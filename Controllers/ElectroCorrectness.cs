using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WaterHeaterTest.Models;
using Microsoft.EntityFrameworkCore;
using WaterHeaterTest.Data;
using System.Net.Http;

namespace WaterHeaterTest.Controllers
{
    public class ElectroCorrectness : Controller
    {
        private readonly PZIPPContext _pZIPPContext;
        private readonly ApplicationDbContext _appContext;
        public ElectroCorrectness(PZIPPContext pzippContext, ApplicationDbContext appContext)
        {
            _pZIPPContext = pzippContext;
            _appContext = appContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetElectropultId(string epult)
        {
            SifElektropult elektropult = _pZIPPContext.SifElektropult.SingleOrDefault(ep => ep.Sifra == epult);
            
            if(elektropult == null)
            {
                return Json("Ne postoji pult sa takvom šifrom.");
            } 
            else
            {
                return Json(elektropult.Id);
            }
        }

        public JsonResult CheckCorrectness(string serialNo, string epCode)
        {
            #region old exporting xml files by day
            //DateTime xmlDate = DateTime.Now;
            //string xmlYear = xmlDate.Year.ToString();
            //string xmlMonth = xmlDate.Month < 10 ? "0" + xmlDate.Month : xmlDate.Month.ToString();
            //string xmlDay = xmlDate.Day < 10? "0" + xmlDate.Day : xmlDate.Day.ToString();
            //string xmlFileName = "P" + xmlYear + xmlMonth + xmlDay + ".xml";
            #endregion
            string xmlFileNamePattern = "*" + serialNo + ".xml";

            string xmlFileName = GetFullnameXMLfile(xmlFileNamePattern);

            if (xmlFileName == "nema fajla")
            {
                return Json("Nema podataka sa elektro pulta.");
            }

            string downloadResult = DownloadXMLcontentTiki(xmlFileName);
            if (downloadResult != "OK")
            {
                return Json(downloadResult);
            }

            SPSResults allTests = DeserializeXML<SPSResults>(xmlFileName);
            if(allTests == default)
            {
                return Json("Deserialize XML Error");
            }

            TEST_RESULTS newTest = SaveToDB(allTests);

            int aktElektroispravnostId = SaveAktElektroispravnostToDB(newTest);//Record tested serialNo
            if(aktElektroispravnostId == 0)
            {
                return Json("AktElektroispravnost not saved in DB.");
            }

            int epult = Convert.ToInt32(epCode);
            string saveAgrAktElektroispResult = SaveAgrAktElektroispravnostSifElektropult(aktElektroispravnostId, epult);
            if(saveAgrAktElektroispResult != "OK")
            {
                return Json("AgrAktElektrois is not saved in DB.");
            }

            string fileDeleteResult = FileDelete(xmlFileName);
            if(fileDeleteResult != "OK")
            {
                return Json(fileDeleteResult);
            }

            if (newTest.TOTALRESULT == "2")
            { 
                //call Itelis web servis on SAP,for getting embedded materials:SN,DN,boiler code,..
                return Json("NOT OK");
            }
            return Json("OK");
        }

        #region Download XML file, deserialize to class object, save object to DB, record test in Akt_Elektroispravnost table and delete xml file.

        private string GetFullnameXMLfile(string xmlFileNamePattern)
        {
            try
            {
                DirectoryInfo rootDirectory = new DirectoryInfo(@"\\10.129.8.158\T21300\dataDir\Results");
                var xmlFiles = rootDirectory.GetFiles(xmlFileNamePattern);
                if (xmlFiles.Length == 0)
                {
                    return "nema fajla";
                }
                else
                {
                    //string currDir = Directory.GetCurrentDirectory();
                    //string xmlName = xmlFiles[0].Substring(currDir.Length + 1);
                    return xmlFiles[0].Name;
                }
            }
            catch (ArgumentNullException)
            {
                return "DirectoryInfo->ArgumentNullException";
            }
            catch (System.Security.SecurityException)
            {
                return "DirectoryInfo->SecurityException";
            }
            catch (ArgumentException)
            {
                return "DirectoryInfo->ArgumentException";
            }
            catch (PathTooLongException)
            {
                return "DirectoryInfo->PathTooLongException";
            }
            catch (DirectoryNotFoundException)
            {
                return "rootDirectory.GetFiles->DirectoryNotFoundException";
            }
            catch (Exception)
            {
                return "Default Exception";
            }
        }
        private string DownloadXMLcontentTiki(string xmlFileContainer)
        {
            string localXMLFile = @"C:\test\" + xmlFileContainer; //tiki IIS: 10.129.15.30
            try
            {
                WebClient webClient = new WebClient();
                string xmlStringed = webClient.DownloadString(@"\\10.129.8.158\T21300\dataDir\Results" + xmlFileContainer);
                System.IO.File.WriteAllText(localXMLFile, xmlStringed);
            }
            catch (ArgumentNullException)
            {
                return "webClient.DownloadString-> Argument Null Exception";
            }
            catch (ArgumentException)
            {
                return "File.WriteAllText->Argument Exception";
            }
            catch (System.Net.WebException)
            {
                return "webClient.DownloadString->NoConnection.target server refused it.";
            }
            catch (NotSupportedException)
            {
                return "webClient.DownloadString-> NotSupportedException";
            }
            catch (PathTooLongException)
            {
                return "File.WriteAllText->PathTooLongException";
            }
            catch (DirectoryNotFoundException)
            {
                return "File.WriteAllText->DirectoryNotFoundException";
            }
            catch (IOException)
            {
                return "File.WriteAllText->IOException";
            }
            catch (UnauthorizedAccessException)
            {
                return "File.WriteAllText->UnauthorizedAccessException";
            }
            catch (System.Security.SecurityException)
            {
                return "File.WriteAllText->SecurityException";
            }
            catch (Exception)
            {
                return "Default Exception";
            }
            return "OK";
        }

        #region testing download on zebracon servers
        private string DownloadXMLcontentLocalhostVersion(string xmlFileContainer)
        {
            string localXMLFile = @"\\192.168.88.13\test\" + xmlFileContainer;
            try
            {
                WebClient webClient = new WebClient();
                string xmlStringed = webClient.DownloadString(@"\\192.168.88.11\test\" + xmlFileContainer);
                System.IO.File.WriteAllText(localXMLFile, xmlStringed);
            }
            catch (ArgumentNullException)
            {
                return "Argument Null Exception";
            }
            catch (System.Net.WebException)
            {
                return "No connection could be made because the target machine actively refused it.";
            }
            catch (NotSupportedException)
            {
                return "NotSupportedException";
            }
            return "OK";
        }
        private string DownloadXMLcontentZebraconVersion(string xmlFileContainer)
        {
            try
            {
                string rootDirectory = @"C:\test";
                Directory.SetCurrentDirectory(rootDirectory);
                string appFile = Directory.GetCurrentDirectory() + "\\" + xmlFileContainer;
                Console.WriteLine(rootDirectory);
                WebClient webClient = new WebClient();
                string xmlStringed = webClient.DownloadString(@"\\192.168.88.11\test\" + xmlFileContainer);
                webClient.Credentials = new NetworkCredential("administrator", "Mil@anche9791");
                webClient.Proxy = new WebProxy();
                System.IO.File.WriteAllText(appFile, xmlStringed);
            }
            catch (ArgumentNullException)
            {
                return "Argument Null Exception:" + Directory.GetCurrentDirectory() + "\\" + xmlFileContainer;
            }
            catch (System.Net.WebException)
            {//No connection could be made because the target machine actively refused it.
                return "No connection could be made because the target machine actively refused it.";
            }
            catch (NotSupportedException)
            {
                return "NotSupportedException" + Directory.GetCurrentDirectory() + "\\" + xmlFileContainer;
            }
            catch (Exception)
            {
                return "Exception" + Directory.GetCurrentDirectory() + "\\" + xmlFileContainer;
            }
            return "OK";
        }
        #endregion
        private T DeserializeXML<T>(string appXMLFile) 
        {
            //Otvori xml fajl za stream (tok podataka)
            //Za taj stream koristi XMLSerializer ka tipu nekog objekta
            //Onda serializer neka deserializuje(rasclani) tok podataka
            try
            {
                string tikiServerFile = @"C:\test\" + appXMLFile;
                using (FileStream streamFromXML = System.IO.File.Open(tikiServerFile, FileMode.Open))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    T classT = (T)xmlSerializer.Deserialize(streamFromXML);
                    return classT;
                }
            }
            catch (ArgumentNullException)
            {
                return default;
            }
            catch (ArgumentException)
            {
                return default;
            }
            catch (System.Net.WebException)
            {
                return default;
            }
            catch (NotSupportedException)
            {
                return default;
            }
            catch (PathTooLongException)
            {
                return default;
            }
            catch (DirectoryNotFoundException)
            {
                return default;
            }
            catch (IOException)
            {
                return default;
            }
            catch (UnauthorizedAccessException)
            {
                return default;
            }
            catch (System.Security.SecurityException)
            {
                return default;
            }
            catch (Exception)
            {
                return default;
            }
            //return "OK";
        }
        private TEST_RESULTS SaveToDB(SPSResults allTests)
        {
            var oneTest = allTests.TEST_RESULTS_List[0];
            
            TEST_RESULTS test_results = new TEST_RESULTS
            {
                COMMENT1 = oneTest.COMMENT1,
                COMMENT2 = oneTest.COMMENT2,
                DEVICENAME = oneTest.DEVICENAME,
                DEVICEVALUE = oneTest.DEVICEVALUE,
                DEVNR = oneTest.DEVNR,
                ENDDATE = oneTest.ENDDATE,
                ERRCODE = oneTest.ERRCODE,
                MTXMINUS = oneTest.MTXMINUS,
                MTXMINUSSTR = oneTest.MTXMINUSSTR,
                MTXPLUS = oneTest.MTXPLUS,
                MTXPLUSSTR = oneTest.MTXPLUSSTR,
                MYRESULT = oneTest.MYRESULT,
                NRFAILED = oneTest.NRFAILED,
                NRPASSED = oneTest.NRPASSED,
                PRODUCTID = oneTest.PRODUCTID,
                PROGRAMFILE = oneTest.PROGRAMFILE,
                REMARKNAME = oneTest.REMARKNAME,
                REMARKVALUE = oneTest.REMARKVALUE,
                //SERIALNR = oneTest.SERIALNR,
                IdSerijskiBrojBojler = GetSerijskiBroj_Id(oneTest.SERIALNR),
                STARTDATE = oneTest.STARTDATE,
                STATIONID = oneTest.STATIONID,
                STEPNR = oneTest.STEPNR,
                STEPTITLE = oneTest.STEPTITLE,
                TOTALRESULT = oneTest.TOTALRESULT,
                USERTESTER = oneTest.USERTESTER,
                WEEKNR = oneTest.WEEKNR,
                //......STEPS FIELDS........
                IdSTEP_CR = GetSTEP_CR_Id(oneTest.STEP_CR),
                IdSTEP_CT = GetSTEP_CT_Id(oneTest.STEP_CT),
                IdSTEP_FK = GetSTEP_FK_Id(oneTest.STEP_FK),
                IdSTEP_H5 = GetSTEP_H5_Id(oneTest.STEP_H5),
                IdSTEP_I5 = GetSTEP_I5_Id(oneTest.STEP_I5),
                IdSTEP_L1 = GetSTEP_L1_Id(oneTest.STEP_L1),
                IdSTEP_PW = GetSTEP_PW_Id(oneTest.STEP_PW),
                IdAgr_ExtaStepStack = null,
                CreatedDate = DateTime.Now
            };
            _pZIPPContext.TEST_RESULTS.Add(test_results);
            try
            {
                _pZIPPContext.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new Exception("Nije sačuvan TEST_RESULTS");
            }

            return test_results;
        }
        private int SaveAktElektroispravnostToDB(TEST_RESULTS newTest)
        {
            AktElektroispravnostBojler elektroTest = new AktElektroispravnostBojler
            {
                IdTestResults = newTest.Id,
                IdKorisnikKontrolisao = _appContext.LoginUsers.SingleOrDefault(user => user.UserName == User.Identity.Name).Id,
                DatumKontrolisanja = DateTime.Now
            };

            _pZIPPContext.AktElektroispravnostBojler.Add(elektroTest);

            try
            {
                _pZIPPContext.SaveChanges();
            }
            catch (ArgumentNullException)
            {
                return 0;
            }
            catch (InvalidOperationException)
            {
                return 0;
            }
            catch (DbUpdateException)
            {
                return 0;
            }

            return elektroTest.Id;
        }

        private string SaveAgrAktElektroispravnostSifElektropult(int aktElektroispravnostId, int epult)
        {
            Agr_AktElektroispravnostBojler_SifElektropult novElektrotestPult = new Agr_AktElektroispravnostBojler_SifElektropult
            {
                IdAktElektroispravnostBojler = aktElektroispravnostId,
                IdSifElektropult = epult,
                DatumKreiranja = DateTime.Now
            };

            _pZIPPContext.Agr_AktElektroispravnostBojler_SifElektropult.Add(novElektrotestPult);

            try
            {
                _pZIPPContext.SaveChanges();
                return "OK";
            }
            catch (DbUpdateException)
            {
                return "SaveAgrAktElektroispravnostSifElektropult -ERROR";
            }
        }
        private string FileDelete(string fileName)
        {
            //string rootDirectory = @"\\192.168.88.13\test\";
            //Directory.SetCurrentDirectory(rootDirectory);
            //string fullPath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            try
            {
                //System.IO.File.Delete(@"\\192.168.88.13\test\" + fileName);  from localhost
                System.IO.File.Delete(@"C:\test\" + fileName);

                return "OK";
            }
            catch (ArgumentException)
            {
                return "File.Delete-> ArgumentException";
            }
            catch (PathTooLongException)
            {
                return "File.Delete->PathTooLongException";
            }
            catch (DirectoryNotFoundException)
            {
                return "File.Delete->DirectoryNotFoundException";
            }
            catch (IOException)
            {
                return "File.Delete->File is in use..";
            }
            catch (UnauthorizedAccessException)
            {
                return "File.Delete->UnauthorizedAccessException";
            }
            catch (NotSupportedException)
            {
                return "File.Delete->NotSupportedException";
            }
            catch (Exception)
            {
                return "File.Delete->EXCEPTION";
            }
        }
        #endregion

        #region Get Ids for Sif_SerijskiBroj and all STEPs

        private int GetSerijskiBroj_Id(string serialNumber)
        {
            Sif_Serijski_Broj_Bojler serBrojBojler = _pZIPPContext.Sif_Serijski_Broj_Bojler.SingleOrDefault(serB => serB.Naziv == serialNumber);
            if (serBrojBojler == null)
            {
                Sif_Serijski_Broj_Bojler noviSerijskiBroj = new Sif_Serijski_Broj_Bojler
                {
                    Naziv = serialNumber,
                    DatumKreiranja = DateTime.Now
                };
                _pZIPPContext.Sif_Serijski_Broj_Bojler.Add(noviSerijskiBroj);

                try
                {
                    _pZIPPContext.SaveChanges();
                    return noviSerijskiBroj.Id;
                }
                catch (DbUpdateException)
                {
                    ViewBag.Exception = "Nije sačuvan serijski broj.";

                    throw ViewBag.Exception;
                }
            }
            else
            {
                return serBrojBojler.Id;
            }
        }
        private int? GetSTEP_CR_Id(STEP_CR newSTEP_CR)
        {
            if(newSTEP_CR == null)
            {
                return null;
            }

            STEP_CR stepCR = new STEP_CR
            {
                CHECKRMIN = newSTEP_CR.CHECKRMIN,
                CreatedDate = DateTime.Now,
                DEVNR = newSTEP_CR.DEVNR,
                ERRCODE = newSTEP_CR.ERRCODE,
                MTXMINUS = newSTEP_CR.MTXMINUS,
                MTXMINUSSTR = newSTEP_CR.MTXMINUSSTR,
                MTXPLUS = newSTEP_CR.MTXPLUS,
                MTXPLUSSTR = newSTEP_CR.MTXPLUSSTR,
                MYRESULT = newSTEP_CR.MYRESULT,
                NRFAILED = newSTEP_CR.NRFAILED,
                NRPASSED = newSTEP_CR.NRPASSED,
                RMAX = newSTEP_CR.RMAX,
                RMIN = newSTEP_CR.RMIN,
                RREAL = newSTEP_CR.RREAL,
                RREALOL = newSTEP_CR.RREALOL,
                STEPNR = newSTEP_CR.STEPNR,
                STEPTITLE = newSTEP_CR.STEPTITLE,
                TESTTIME = newSTEP_CR.TESTTIME
            };
            _pZIPPContext.STEP_CR.Add(stepCR);

            try
            {
                _pZIPPContext.SaveChanges();

                return stepCR.Id;
            }
            catch (DbUpdateException)
            {
                ViewBag.Exception = "Nije sačuvan STEP_CR";

                throw ViewBag.Exception;
            }
        }
        private int? GetSTEP_CT_Id(STEP_CT newSTEP_CT)
        {
            if (newSTEP_CT == null)
            {
                return null;
            }

            STEP_CT stepCT = new STEP_CT
            {
                CHECKIMAX = newSTEP_CT.CHECKIMAX,
                TESTTIME = newSTEP_CT.TESTTIME,
                STEPTITLE = newSTEP_CT.STEPTITLE,
                STEPNR = newSTEP_CT.STEPNR,
                CreatedDate = DateTime.Now,
                DEVNR = newSTEP_CT.DEVNR,
                ERRCODE = newSTEP_CT.ERRCODE,
                IMAX = newSTEP_CT.ERRCODE,
                IMIN = newSTEP_CT.IMIN,
                IREAL = newSTEP_CT.IREAL,
                MTXMINUS = newSTEP_CT.MTXMINUS,
                MTXMINUSSTR = newSTEP_CT.MTXMINUSSTR,
                MTXPLUS = newSTEP_CT.MTXPLUS,
                MTXPLUSSTR = newSTEP_CT.MTXPLUSSTR,
                MYRESULT = newSTEP_CT.MYRESULT,
                NRFAILED = newSTEP_CT.NRFAILED,
                NRPASSED = newSTEP_CT.NRPASSED
            };
            _pZIPPContext.STEP_CT.Add(stepCT);

            try
            {
                _pZIPPContext.SaveChanges();

                return stepCT.Id;
            }
            catch (DbUpdateException)
            {
                ViewBag.Exception = "Nije sačuvan STEP_CT";

                throw ViewBag.Exception;
            }
        }
        private int? GetSTEP_FK_Id(STEP_FK newSTEP_FK)
        {
            if (newSTEP_FK == null)
            {
                return null;
            }

            STEP_FK stepFK = new STEP_FK
            {
                CHECKU1 = newSTEP_FK.CHECKU1,
                CHECKU2 = newSTEP_FK.CHECKU2,
                CHECKU3 = newSTEP_FK.CHECKU3,
                NRPASSED = newSTEP_FK.NRPASSED,
                COSPHIMAX1 = newSTEP_FK.COSPHIMAX1,
                COSPHIMAX2 = newSTEP_FK.COSPHIMAX2,
                COSPHIMAX3 = newSTEP_FK.COSPHIMAX3,
                COSPHIMIN1 = newSTEP_FK.COSPHIMIN1,
                COSPHIMIN2 = newSTEP_FK.COSPHIMIN2,
                COSPHIMIN3 = newSTEP_FK.COSPHIMIN3,
                COSPHIREAL1 = newSTEP_FK.COSPHIREAL1,
                COSPHIREAL2 = newSTEP_FK.COSPHIREAL2,
                COSPHIREAL3 = newSTEP_FK.COSPHIREAL3,
                CreatedDate = DateTime.Now,
                DEVNR = newSTEP_FK.DEVNR,
                ERRCODE = newSTEP_FK.ERRCODE,
                GOODTIME = newSTEP_FK.GOODTIME,
                GOODTIMEOK = newSTEP_FK.GOODTIMEOK,
                IMAX1 = newSTEP_FK.IMAX1,
                IMAX2 = newSTEP_FK.IMAX2,
                IMAX3 = newSTEP_FK.IMAX3,
                IMIN1 = newSTEP_FK.IMIN1,
                IMIN2 = newSTEP_FK.IMIN2,
                IMIN3 = newSTEP_FK.IMIN3,
                IREAL1 = newSTEP_FK.IREAL1,
                IREAL2 = newSTEP_FK.IREAL2,
                IREAL3 = newSTEP_FK.IREAL3,
                KEEPPOWER = newSTEP_FK.KEEPPOWER,
                MTXMINUS = newSTEP_FK.MTXMINUS,
                MTXMINUSSTR = newSTEP_FK.MTXMINUSSTR,
                MTXPLUS = newSTEP_FK.MTXPLUS,
                MTXPLUSSTR = newSTEP_FK.MTXPLUSSTR,
                MYRESULT = newSTEP_FK.MYRESULT,
                NRFAILED = newSTEP_FK.NRFAILED,
                PHASE = newSTEP_FK.PHASE,
                PMAX = newSTEP_FK.PMAX,
                PMIN = newSTEP_FK.PMIN,
                POWERREAL = newSTEP_FK.POWERREAL,
                STEPNR = newSTEP_FK.STEPNR,
                STEPTITLE = newSTEP_FK.STEPTITLE,
                TESTTIME = newSTEP_FK.TESTTIME,
                UMAX1 = newSTEP_FK.UMAX1,
                UMAX2 = newSTEP_FK.UMAX2,
                UMAX3 = newSTEP_FK.UMAX3,
                UMIN1 = newSTEP_FK.UMIN1,
                UMIN2 = newSTEP_FK.UMIN2,
                UMIN3 = newSTEP_FK.UMIN3,
                UREAL1 = newSTEP_FK.UREAL1,
                UREAL2 = newSTEP_FK.UREAL2,
                UREAL3 = newSTEP_FK.UREAL3,
                USECOSPHI1 = newSTEP_FK.USECOSPHI1,
                USECOSPHI2 = newSTEP_FK.USECOSPHI2,
                USECOSPHI3 = newSTEP_FK.USECOSPHI3,
                USEPHASE1 = newSTEP_FK.USEPHASE1,
                USEPHASE2 = newSTEP_FK.USEPHASE2,
                USEPHASE3 = newSTEP_FK.USEPHASE3
            };
            
            _pZIPPContext.STEP_FK.Add(stepFK);

            try
            {
                _pZIPPContext.SaveChanges();

                return stepFK.Id;
            }
            catch (DbUpdateException)
            {
                ViewBag.Exception = "Nije sačuvan STEP_FK";

                throw ViewBag.Exception;
            }
        }
        private int? GetSTEP_H5_Id(STEP_H5 newSTEP_H5)
        {
            if (newSTEP_H5 == null)
            {
                return null;
            }
            STEP_H5 stepH5 = new STEP_H5
            {
                ARCMAX = newSTEP_H5.ARCMAX,
                ARCREAL = newSTEP_H5.ARCREAL,
                CONNECT = newSTEP_H5.CONNECT,
                CreatedDate = DateTime.Now,
                DEVNR = newSTEP_H5.DEVNR,
                ERRCODE = newSTEP_H5.ERRCODE,
                IMAX = newSTEP_H5.IMAX,
                IMIN = newSTEP_H5.IMIN,
                IREAL = newSTEP_H5.IREAL,
                IRMAX = newSTEP_H5.IRMAX,
                IRMIN = newSTEP_H5.IRMIN,
                ITYPE = newSTEP_H5.ITYPE,
                METHOD = newSTEP_H5.METHOD,
                MTXMINUS = newSTEP_H5.MTXMINUS,
                MTXMINUSSTR = newSTEP_H5.MTXMINUSSTR,
                MTXPLUS = newSTEP_H5.MTXPLUS,
                MTXPLUSSTR = newSTEP_H5.MTXPLUSSTR,
                MYRESULT = newSTEP_H5.MYRESULT,
                NRFAILED = newSTEP_H5.NRFAILED,
                NRPASSED = newSTEP_H5.NRPASSED,
                RAMPDOWN = newSTEP_H5.RAMPDOWN,
                RAMPERR = newSTEP_H5.RAMPERR,
                RAMPTIME = newSTEP_H5.RAMPTIME,
                STEPNR = newSTEP_H5.STEPNR,
                STEPTITLE = newSTEP_H5.STEPTITLE,
                TESTTIME = newSTEP_H5.TESTTIME,
                UNOM = newSTEP_H5.UNOM,
                UREAL = newSTEP_H5.UREAL,
                USEDARC = newSTEP_H5.USEDARC,
                USTART = newSTEP_H5.USTART,
                UTYPE = newSTEP_H5.UTYPE
            };

            _pZIPPContext.STEP_H5.Add(stepH5);
            try
            {
                _pZIPPContext.SaveChanges();

                return stepH5.Id;
            }
            catch (DbUpdateException)
            {
                ViewBag.Exception = "Nije sačuvan STEP_H5";

                throw ViewBag.Exception;
            }
        }
        private int? GetSTEP_I5_Id(STEP_I5 newSTEP_I5)
        {
            if (newSTEP_I5 == null)
            {
                return null;
            }
            STEP_I5 stepI5 = new STEP_I5
            {
                CONNECT = newSTEP_I5.CONNECT,
                USTART = newSTEP_I5.USTART,
                CreatedDate = DateTime.Now,
                DEVNR = newSTEP_I5.DEVNR,
                ERRCODE = newSTEP_I5.ERRCODE,
                IRMAX = newSTEP_I5.IRMAX,
                IRMIN = newSTEP_I5.IRMIN,
                MTXMINUS = newSTEP_I5.MTXMINUS,
                MTXMINUSSTR = newSTEP_I5.MTXMINUSSTR,
                MTXPLUS = newSTEP_I5.MTXPLUS,
                MTXPLUSSTR = newSTEP_I5.MTXPLUSSTR,
                MYRESULT = newSTEP_I5.MYRESULT,
                NRFAILED = newSTEP_I5.NRFAILED,
                NRPASSED = newSTEP_I5.NRPASSED,
                RAMPDOWN = newSTEP_I5.RAMPDOWN,
                RAMPERR = newSTEP_I5.RAMPERR,
                RAMPTIME = newSTEP_I5.RAMPTIME,
                RMIN = newSTEP_I5.RMIN,
                RREAL = newSTEP_I5.RREAL,
                RREALOL = newSTEP_I5.RREALOL,
                SKMODE = newSTEP_I5.SKMODE,
                STEPNR = newSTEP_I5.STEPNR,
                STEPTITLE = newSTEP_I5.STEPTITLE,
                TESTTIME = newSTEP_I5.TESTTIME,
                UNOM = newSTEP_I5.UNOM,
                UREAL = newSTEP_I5.UREAL
            };
            _pZIPPContext.STEP_I5.Add(stepI5);
            try
            {
                _pZIPPContext.SaveChanges();

                return stepI5.Id;
            }
            catch (DbUpdateException)
            {
                ViewBag.Exception = "Nije sačuvan STEP_I5";

                throw ViewBag.Exception;
            }
        }
        private int? GetSTEP_L1_Id(STEP_L1 newSTEP_L1)
        {
            if (newSTEP_L1 == null)
            {
                return null;
            }
            STEP_L1 stepL1 = new STEP_L1
            {
                UTYPE = newSTEP_L1.UTYPE,
                UREAL = newSTEP_L1.UREAL,
                UNOM = newSTEP_L1.UNOM,
                TESTTIME = newSTEP_L1.TESTTIME,
                STEPTITLE = newSTEP_L1.STEPTITLE,
                DEVNR = newSTEP_L1.DEVNR,
                ERRCODE = newSTEP_L1.ERRCODE,
                CreatedDate = DateTime.Now,
                IMAX = newSTEP_L1.IMAX,
                IMIN = newSTEP_L1.IMIN,
                IREAL = newSTEP_L1.IREAL,
                IREALMAX = newSTEP_L1.IREALMAX,
                MTXMINUS = newSTEP_L1.MTXMINUS,
                MTXMINUSSTR = newSTEP_L1.MTXMINUSSTR,
                MTXPLUS = newSTEP_L1.MTXPLUS,
                MTXPLUSSTR = newSTEP_L1.MTXPLUSSTR,
                MYRESULT = newSTEP_L1.MYRESULT,
                NRFAILED = newSTEP_L1.NRFAILED,
                NRPASSED = newSTEP_L1.NRPASSED,
                PHASE = newSTEP_L1.PHASE,
                STEPNR = newSTEP_L1.STEPNR,
                UMODE = newSTEP_L1.UMODE
            };
            _pZIPPContext.STEP_L1.Add(stepL1);
            try
            {
                _pZIPPContext.SaveChanges();

                return stepL1.Id;
            }
            catch (DbUpdateException)
            {
                ViewBag.Exception = "Nije sačuvan STEP_L1";

                throw ViewBag.Exception;
            }
        }
        private int? GetSTEP_PW_Id(STEP_PW newSTEP_PW)
        {
            if (newSTEP_PW == null)
            {
                return null;
            }
            STEP_PW stepPW = new STEP_PW
            {
                IMIN = newSTEP_PW.IMIN,
                CreatedDate = DateTime.Now,
                DEVNR = newSTEP_PW.DEVNR,
                ERRCODE = newSTEP_PW.ERRCODE,
                FASTPW = newSTEP_PW.FASTPW,
                IREAL = newSTEP_PW.IREAL,
                METHOD = newSTEP_PW.METHOD,
                MTXMINUS = newSTEP_PW.MTXMINUS,
                MTXMINUSSTR = newSTEP_PW.MTXMINUSSTR,
                MTXPLUS = newSTEP_PW.MTXPLUS,
                MTXPLUSSTR = newSTEP_PW.MTXPLUSSTR,
                MYRESULT = newSTEP_PW.MYRESULT,
                NRFAILED = newSTEP_PW.NRFAILED,
                NRPASSED = newSTEP_PW.NRPASSED,
                PWMSET = newSTEP_PW.PWMSET,
                RMAX = newSTEP_PW.RMAX,
                RMIN = newSTEP_PW.RMIN,
                RREAL = newSTEP_PW.RREAL,
                STARTMODE = newSTEP_PW.STARTMODE,
                STEPNR = newSTEP_PW.STEPNR,
                STEPTITLE = newSTEP_PW.STEPTITLE,
                TESTTIME = newSTEP_PW.TESTTIME,
                UDROPMAX = newSTEP_PW.UDROPMAX,
                UDROPREAL = newSTEP_PW.UDROPREAL,
                UMAX = newSTEP_PW.UMAX
            };

            _pZIPPContext.STEP_PW.Add(stepPW);

            try
            {
                _pZIPPContext.SaveChanges();

                return stepPW.Id;
            }
            catch (DbUpdateException)
            {
                ViewBag.Exception = "Nije sačuvan STEP_PW";

                throw ViewBag.Exception;
            }
        }
        #endregion
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using WaterHeaterTest.Data;
using WaterHeaterTest.Models;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.CSharp.RuntimeBinder;

namespace WaterHeaterTest.Controllers
{
    public class WaterResistance : Controller
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly PZIPPContext _pzippContext;
        public WaterResistance(ApplicationDbContext appDbContext, PZIPPContext pzippContext)
        {
            _appDbContext = appDbContext;
            _pzippContext = pzippContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public JsonResult TestSubmitOK(string barcodeForwarded)
        {
            var idKorisnikKontrolisao = _appDbContext.LoginUsers.SingleOrDefault(user => user.UserName == User.Identity.Name).Id;

            #region staro-provera formata barkoda
            //if (barcodeForwarded == null || barcodeForwarded.Length == 0)
            //{
            //    return Json("Barkod je prazan");
            //} else
            //{
            //    //provera za slova B,Z,X u barkodu
            //    if (barcodeForwarded.IndexOf("B") == -1 ||
            //        barcodeForwarded.IndexOf("Z") == -1 || barcodeForwarded.IndexOf("X") == -1)
            //    {
            //        return Json("Nema slova B,X ili Z u barkodu.");
            //    }
            //    else
            //    {
            //        if(!((barcodeForwarded.IndexOf("B") < barcodeForwarded.IndexOf("Z")) && (barcodeForwarded.IndexOf("Z") < barcodeForwarded.IndexOf("X"))))
            //        {
            //            return Json("B,X,Z nisu u dobrom poretku u barkodu.");
            //        }
            //        int brojB = 0, brojZ = 0, brojX = 0;
            //        foreach (char slovo in barcodeForwarded)
            //        {
            //            if (slovo == 'B')
            //            {
            //                brojB++;
            //            }
            //            if (slovo == 'Z')
            //            {
            //                brojZ++;
            //            }
            //            if (slovo == 'X')
            //            {
            //                brojX++;
            //            }
            //        }
            //        if (brojB != 1 || brojZ != 1 || brojX != 1)
            //        {
            //            return Json("Previse slova B,X ili Z u barkodu.");
            //        }

            //        string sifraBC = barcodeForwarded.Split("B")[0];
            //        string sarzaBC = barcodeForwarded.Split("B")[1].Split("Z")[0];
            //        string redniBrojBC = barcodeForwarded.Split("B")[1].Split("Z")[1].Split("X")[0];
            //        string ukupnoBC = barcodeForwarded.Split("B")[1].Split("Z")[1].Split("X")[1];

            //        if (sifraBC.Length == 0 || sarzaBC.Length == 0 || redniBrojBC.Length == 0 || ukupnoBC.Length == 0)
            //        {
            //            return Json("Barkod nema šifru/šaržu/redniBr/ukupno.");
            //        }

            //        int rbCeo;
            //        int ceoUkupno;
            //        bool convertedToInt1 = Int32.TryParse(redniBrojBC, out rbCeo);
            //        if (!convertedToInt1)
            //        {
            //            return Json("Redni broj nema dobar format.");
            //        }

            //        bool convertedToInt = Int32.TryParse(ukupnoBC, out ceoUkupno);
            //        if (!convertedToInt)
            //        {
            //            return Json("Broj ukupno nema dobar format.");
            //        }

            //    }
            //}
            #endregion

            string[] delovi = barcodeForwarded.Split("B");
            string nazivSifre = delovi[0];
            string nazivSarze = delovi[1].Split("Z")[0];
            string redniBroj = delovi[1].Split("Z")[1].Split("X")[0];
            int narucenaKolicina = int.Parse(delovi[1].Split("Z")[1].Split("X")[1]);

            #region sifra
            SifKotao sifra = _pzippContext.SifKotao.FirstOrDefault(sifra => sifra.Naziv == nazivSifre);
            int idSifre;

            if(sifra == null)
            {
                SifKotao novaSifra = new SifKotao
                {
                    Naziv = nazivSifre,
                    DatumKreiranja = DateTime.Now
                };
                _pzippContext.SifKotao.Add(novaSifra);

                try
                {
                    _pzippContext.SaveChanges();
                    idSifre = novaSifra.Id;
                }
                catch (ArgumentNullException)
                {
                    return Json("Nije kreirana nova sifra");
                }
                catch (Exception)
                {
                    return Json("Nije kreirana nova sifra");
                }
            }
            else
            {
                idSifre = sifra.Id;
            }
            #endregion sifra

            #region sarza
            SifSarzaKotao sarza = _pzippContext.SifSarzaKotao.FirstOrDefault(sarza => sarza.Naziv == nazivSarze && sarza.NarucenaKolicina == narucenaKolicina);
            int idSarze;

            if(sarza == null)
            {
                SifSarzaKotao novaSarza = new SifSarzaKotao
                {
                    Naziv = nazivSarze,
                    NarucenaKolicina = narucenaKolicina,
                    DatumKreiranja = DateTime.Now
                };
                _pzippContext.SifSarzaKotao.Add(novaSarza);

                try
                {
                    _pzippContext.SaveChanges();
                    idSarze = novaSarza.Id;
                }
                catch (ArgumentNullException)
                {
                    return Json("Šarža nije sačuvana uspešno.");
                }
                catch (Exception)
                {
                    return Json("Šarža nije sačuvana uspešno.");
                }
            }
            else
            {
                idSarze = sarza.Id;
            }

            #endregion

            #region agr_sifraSarza
            AgrSifraKotaoSarzaKotao agrSifraSarza = _pzippContext.AgrSifraKotaoSarzaKotao.FirstOrDefault(sifraSarza => sifraSarza.IdKotao == idSifre && sifraSarza.IdSarza == idSarze);
            int idSifraSarza;

            if(agrSifraSarza == null)
            {
                AgrSifraKotaoSarzaKotao novaSifraSarza = new AgrSifraKotaoSarzaKotao
                {
                    IdKotao = idSifre,
                    IdSarza = idSarze,
                    DatumKreiranja = DateTime.Now
                };
                _pzippContext.AgrSifraKotaoSarzaKotao.Add(novaSifraSarza);

                try
                {
                    _pzippContext.SaveChanges();
                    idSifraSarza = novaSifraSarza.Id;
                }
                catch (ArgumentNullException)
                {
                    return Json("Šifra-Šarža nije sačuvana uspešno.");
                }
                catch (Exception)
                {
                    return Json("Šifra-Šarža nije sačuvana uspešno.");
                }
            }
            else
            {
                idSifraSarza = agrSifraSarza.Id;
            }
            #endregion

            #region serijskiBroj
            SifSerijskiBrojKotao serijskiBroj = _pzippContext.SifSerijskiBrojKotao.FirstOrDefault(sb => sb.IdAgrSifraKotaoSarzaKotao == idSifraSarza && sb.RedniBroj == redniBroj);
            int idSerijskiBroj;
            
            if(serijskiBroj == null)
            {
                SifSerijskiBrojKotao noviSerijskiBroj = new SifSerijskiBrojKotao
                {
                    IdAgrSifraKotaoSarzaKotao = idSifraSarza,
                    RedniBroj = redniBroj,
                    DatumKreiranja = DateTime.Now
                };
                _pzippContext.SifSerijskiBrojKotao.Add(noviSerijskiBroj);

                try
                {
                    _pzippContext.SaveChanges();
                    idSerijskiBroj = noviSerijskiBroj.Id;
                }
                catch (ArgumentNullException)
                {
                    return Json("SerijskiBroj nije sačuvan uspešno.");
                }
                catch (Exception)
                {
                    return Json("SerijskiBroj nije sačuvan uspešno.");
                }
            }
            else
            {
                idSerijskiBroj = serijskiBroj.Id;
            }
            #endregion

            #region akt_vodootpornostKotla
            AktVodootpornostKotao noviVodotest = new AktVodootpornostKotao
            {
                IdSerijskiBrojKotao = idSerijskiBroj,
                Ispravan = true,
                IdVrstaProblemaOpis = null,
                DatumKontrolisanja = DateTime.Now,
                IdKorisnikKontrolisao = idKorisnikKontrolisao
            };

            List<AktVodootpornostKotao> kotaoTestovi = _pzippContext.AktVodootpornostKotao.Where(k =>
                    k.IdSerijskiBrojKotao == noviVodotest.IdSerijskiBrojKotao && k.Ispravan == true).ToList();
            
            if(kotaoTestovi.Count > 0)
            {
                return Json("Serijski broj je već OK");
            }
            _pzippContext.AktVodootpornostKotao.Add(noviVodotest);

            try
            {
                _pzippContext.SaveChanges();
            }
            catch (ArgumentNullException)
            {
                return Json("Test nije sačuvan uspešno.");
            }
            catch (Exception)
            {
                return Json("Test nije sačuvan uspešno.");
            }

            #endregion
            return Json("Uspešno sačuvan test.");
        }

        [HttpPost]
        public IActionResult TestSubmitNotOK(string barCode)
        {
            dynamic podaci = new ExpandoObject();

            var idKorisnikKontrolisao = _appDbContext.LoginUsers.SingleOrDefault(user => user.UserName == User.Identity.Name).Id;

            #region staro -provera formata barkoda
                //if (barCode == null || barCode.Length == 0)
                //{
                //    return Json("Barkod je prazan");
                //}
                //else
                //{
                //    //provera za slova B,Z,X u barkodu
                //    if (barCode.IndexOf("B") == -1 ||
                //        barCode.IndexOf("Z") == -1 || barCode.IndexOf("X") == -1)
                //    {
                //        return Json("Nema slova B,X ili Z u barkodu.");
                //    }
                //    else
                //    {
                //        if (!((barCode.IndexOf("B") < barCode.IndexOf("Z")) && (barCode.IndexOf("Z") < barCode.IndexOf("X"))))
                //        {
                //            return Json("B,X,Z nisu u dobrom poretku u barkodu.");
                //        }
                //        int brojB = 0, brojZ = 0, brojX = 0;
                //        foreach (char slovo in barCode)
                //        {
                //            if (slovo == 'B')
                //            {
                //                brojB++;
                //            }
                //            if (slovo == 'Z')
                //            {
                //                brojZ++;
                //            }
                //            if (slovo == 'X')
                //            {
                //                brojX++;
                //            }
                //        }
                //        if (brojB != 1 || brojZ != 1 || brojX != 1)
                //        {
                //            return Json("Previse slova B,X ili Z u barkodu.");
                //        }

                //        string sifraBC = barCode.Split("B")[0];
                //        string sarzaBC = barCode.Split("B")[1].Split("Z")[0];
                //        string redniBrojBC = barCode.Split("B")[1].Split("Z")[1].Split("X")[0];
                //        string ukupnoBC = barCode.Split("B")[1].Split("Z")[1].Split("X")[1];

                //        if (sifraBC.Length == 0 || sarzaBC.Length == 0 || redniBrojBC.Length == 0 || ukupnoBC.Length == 0)
                //        {
                //            return Json("Barkod nema šifru/šaržu/redniBr/ukupno.");
                //        }

                //        int rbCeo;
                //        int ceoUkupno;
                //        bool convertedToInt1 = Int32.TryParse(redniBrojBC, out rbCeo);
                //        if (!convertedToInt1)
                //        {
                //            return Json("Redni broj nema dobar format.");
                //        }

                //        bool convertedToInt = Int32.TryParse(ukupnoBC, out ceoUkupno);
                //        if (!convertedToInt)
                //        {
                //            return Json("Broj ukupno nema dobar format.");
                //        }

                //    }
                //}
            #endregion

            string[] delovi = barCode.Split("B");
            string nazivSifre = delovi[0];
            string nazivSarze = delovi[1].Split("Z")[0];
            string redniBroj = delovi[1].Split("Z")[1].Split("X")[0];
            int narucenaKolicina = int.Parse(delovi[1].Split("Z")[1].Split("X")[1]);

            #region sifra
            SifKotao sifra = _pzippContext.SifKotao.FirstOrDefault(sifra => sifra.Naziv == nazivSifre);
            int idSifre;

            if (sifra == null)
            {
                SifKotao novaSifra = new SifKotao
                {
                    Naziv = nazivSifre,
                    DatumKreiranja = DateTime.Now
                };
                _pzippContext.SifKotao.Add(novaSifra);

                try
                {
                    _pzippContext.SaveChanges();
                    idSifre = novaSifra.Id;
                }
                catch (ArgumentNullException)
                {
                    return Json("Nije kreirana nova sifra");
                }
                catch (Exception)
                {
                    return Json("Nije kreirana nova sifra");
                }
            }
            else
            {
                idSifre = sifra.Id;
            }
            #endregion sifra

            #region sarza
            SifSarzaKotao sarza = _pzippContext.SifSarzaKotao.FirstOrDefault(sarza => sarza.Naziv == nazivSarze && sarza.NarucenaKolicina == narucenaKolicina);
            int idSarze;

            if (sarza == null)
            {
                SifSarzaKotao novaSarza = new SifSarzaKotao
                {
                    Naziv = nazivSarze,
                    NarucenaKolicina = narucenaKolicina,
                    DatumKreiranja = DateTime.Now
                };
                _pzippContext.SifSarzaKotao.Add(novaSarza);

                try
                {
                    _pzippContext.SaveChanges();
                    idSarze = novaSarza.Id;
                }
                catch (ArgumentNullException)
                {
                    return Json("Šarža nije sačuvana uspešno.");
                }
                catch (Exception)
                {
                    return Json("Šarža nije sačuvana uspešno.");
                }
            }
            else
            {
                idSarze = sarza.Id;
            }

            #endregion

            #region agr_sifraSarza
            AgrSifraKotaoSarzaKotao agrSifraSarza = _pzippContext.AgrSifraKotaoSarzaKotao.FirstOrDefault(sifraSarza => sifraSarza.IdKotao == idSifre && sifraSarza.IdSarza == idSarze);
            int idSifraSarza;

            if (agrSifraSarza == null)
            {
                AgrSifraKotaoSarzaKotao novaSifraSarza = new AgrSifraKotaoSarzaKotao
                {
                    IdKotao = idSifre,
                    IdSarza = idSarze,
                    DatumKreiranja = DateTime.Now
                };
                _pzippContext.AgrSifraKotaoSarzaKotao.Add(novaSifraSarza);

                try
                {
                    _pzippContext.SaveChanges();
                    idSifraSarza = novaSifraSarza.Id;
                }
                catch (ArgumentNullException)
                {
                    return Json("Šifra-Šarža nije sačuvana uspešno.");
                }
                catch (Exception)
                {
                    return Json("Šifra-Šarža nije sačuvana uspešno.");
                }
            }
            else
            {
                idSifraSarza = agrSifraSarza.Id;
            }
            #endregion

            #region serijskiBroj
            SifSerijskiBrojKotao serijskiBroj = _pzippContext.SifSerijskiBrojKotao.FirstOrDefault(sb => sb.IdAgrSifraKotaoSarzaKotao == idSifraSarza && sb.RedniBroj == redniBroj);
            int idSerijskiBroj;

            if (serijskiBroj == null)
            {
                SifSerijskiBrojKotao noviSerijskiBroj = new SifSerijskiBrojKotao
                {
                    IdAgrSifraKotaoSarzaKotao = idSifraSarza,
                    RedniBroj = redniBroj,
                    DatumKreiranja = DateTime.Now
                };
                _pzippContext.SifSerijskiBrojKotao.Add(noviSerijskiBroj);

                try
                {
                    _pzippContext.SaveChanges();
                    idSerijskiBroj = noviSerijskiBroj.Id;
                }
                catch (ArgumentNullException)
                {
                    return Json("SerijskiBroj nije sačuvan uspešno.");
                }
                catch (Exception)
                {
                    return Json("SerijskiBroj nije sačuvan uspešno.");
                }
            }
            else
            {
                idSerijskiBroj = serijskiBroj.Id;
            }
            #endregion

            var problemi = (from prob in _pzippContext.SifVrstaProblema.ToList()
                            select new
                            {
                                Id = prob.Id,
                                Value = prob.Naziv
                            }).ToList();

            podaci.idSerijskiBroj = idSerijskiBroj;
            podaci.vrsteProblema = problemi;

            return View("WebCamFinal", podaci);
        }

        #region Save image in local folder/database
        [HttpPost]
        public JsonResult CaptureImage()
        {
            string imageUrl = "";
            try
            {
                var files = HttpContext.Request.Form.Files;

                if (files != null)
                {
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            var fileName = file.FileName;
                            var fileNameToStore = string.Concat(Convert.ToString(Guid.NewGuid()), Path.GetExtension(fileName));
                            //  Path to store the snapshot in local folder
                            var filepath = Path.Combine(Directory.GetCurrentDirectory(), $@"wwwroot\photos") + $@"\{fileNameToStore}";

                            // Save image file in local folder
                            if (!string.IsNullOrEmpty(filepath))
                            {
                                using (FileStream fileStream = System.IO.File.Create(filepath))
                                {
                                    file.CopyTo(fileStream);
                                    fileStream.Flush();
                                }
                            }

                            // Save image file in database
                            byte[] imgBytes = System.IO.File.ReadAllBytes(filepath);
                            if (imgBytes != null)
                            {
                                if (imgBytes != null)
                                {
                                    string slikaKotla = Convert.ToBase64String(imgBytes, 0, imgBytes.Length);
                                    imageUrl = string.Concat("data:image/jpg;base64,", slikaKotla);
                                    // Code to store into database
                                    // save filename and image url(base 64 string) to the database
                                }
                            }
                        }
                    }

                }
                else
                {
                    return Json("Slika nije kreirana.");
                }
            }
            catch (Exception)
            {
                return Json("Slika nije kreirana.");
            }

            return Json(imageUrl);
        }
        #endregion

        [HttpPost]
        public IActionResult ProblemDetailsSubmit(int serijskiBroj, int idVrstaProblema, string opis, string sveSlike)
        {
            dynamic data = new ExpandoObject();
            data.idSerijskiBroj = serijskiBroj;

            try
            {
                var problemi = (from prob in _pzippContext.SifVrstaProblema.ToList()
                            select new
                            {
                                Id = prob.Id,
                                Value = prob.Naziv
                            }).ToList();

                data.vrsteProblema = problemi;

                if (idVrstaProblema == 0)
                {
                    ViewBag.Message = "Problem nije definisan.";
                    return View("AgrVrstaProblemaOpis", data);
                }

                AgrVrstaProblemaOpis problemOpis = new AgrVrstaProblemaOpis
                {
                    IdVrstaProblema = idVrstaProblema,
                    Opis = opis,
                    Slika = sveSlike,
                    DatumKreiranja = DateTime.Now
                };
                _pzippContext.AgrVrstaProblemaOpis.Add(problemOpis);
                _pzippContext.SaveChanges();

                int idProblemOpis = problemOpis.Id;

                AktVodootpornostKotao noviTest = new AktVodootpornostKotao
                {
                    IdSerijskiBrojKotao = serijskiBroj,
                    Ispravan = false,
                    IdVrstaProblemaOpis = idProblemOpis,
                    DatumKontrolisanja = DateTime.Now,
                    IdKorisnikKontrolisao = _appDbContext.LoginUsers.SingleOrDefault(user => user.UserName == User.Identity.Name).Id
                };

                List<AktVodootpornostKotao> kotaoTestovi = _pzippContext.AktVodootpornostKotao.Where(k =>
                    k.IdSerijskiBrojKotao == noviTest.IdSerijskiBrojKotao && k.Ispravan == true).ToList();

                if (kotaoTestovi.Count > 0)
                {
                    return RedirectToAction("Index");
                }

                _pzippContext.AktVodootpornostKotao.Add(noviTest);

                _pzippContext.SaveChanges();
            }
            catch (ArgumentException)
            {
                ViewBag.Message = "Lista problema je prazna.";
                return View("AgrVrstaProblemaOpis", data);
            }
            catch (SqlException)
            {
                ViewBag.Message = "Veza ka bazi nije uspostavljena.";
                return View("AgrVrstaProblemaOpis", data);
            }
            catch (DbUpdateException)
            {
                ViewBag.Message = "Problem/opis/slika nije sačuvan.";
                return View("AgrVrstaProblemaOpis", data);
            }
            catch (RuntimeBinderException)
            {
                ViewBag.Message = "Nema interneta.";
                return View("AgrVrstaProblemaOpis", data);
            }
            catch (Exception)
            {
                ViewBag.Message = "Problem/opis/slika nije sačuvan.";
                return View("AgrVrstaProblemaOpis", data);
            }

            return RedirectToAction("Index");
        }

        public JsonResult CheckBarcodeFormat(string bCode)
        {
            if (bCode == null || bCode.Length == 0)
            {
                return Json("Barkod je prazan");
            }
            else //nije prazan barkod
            {
                //provera za slova B,Z,X u barkodu
                if (bCode.IndexOf("B") == -1 || bCode.IndexOf("Z") == -1 || bCode.IndexOf("X") == -1)
                {
                    return Json("Nema slova B,Z ili X u barkodu.");
                }
                else //ima slova B,Z,X
                {
                    if (!((bCode.IndexOf("B") < bCode.IndexOf("Z")) && (bCode.IndexOf("Z") < bCode.IndexOf("X"))))
                    {
                        return Json("B,Z,X nisu u dobrom poretku u barkodu.");
                    }
                    else //B,Z,X su u dobrom poretku
                    {
                        int brojB = 0, brojZ = 0, brojX = 0;
                        foreach (char slovo in bCode)
                        {
                            if (slovo == 'B')
                            {
                                brojB++;
                            }
                            if (slovo == 'Z')
                            {
                                brojZ++;
                            }
                            if (slovo == 'X')
                            {
                                brojX++;
                            }
                        }

                        if (brojB != 1 || brojZ != 1 || brojX != 1)
                        {
                            return Json("Previse slova B,Z ili X u barkodu.");
                        }
                        else // B,Z,X ima taman po jedan
                        {
                            string sifraBC = bCode.Split("B")[0];
                            string sarzaBC = bCode.Split("B")[1].Split("Z")[0];
                            string redniBrojBC = bCode.Split("B")[1].Split("Z")[1].Split("X")[0];
                            string ukupnoBC = bCode.Split("B")[1].Split("Z")[1].Split("X")[1];

                            if (sifraBC.Length == 0 || sarzaBC.Length == 0 || redniBrojBC.Length == 0 || ukupnoBC.Length == 0)
                            {
                                return Json("Barkod nema šifru/šaržu/redniBr/ukupno.");
                            }
                            else //ima sifru/sarzu/rb/ukupno
                            {
                                //Provera sifre u Sif_Artikal
                                SifArtikal artikal = _pzippContext.SifArtikal.SingleOrDefault(sa => sa.Sifra == sifraBC);
                                if(artikal == null)
                                {
                                    return Json("Sifra kotla ne postoji u bazi");
                                }

                                int rbCeo;
                                int ceoUkupno;
                                bool convertedToInt1 = Int32.TryParse(redniBrojBC, out rbCeo);
                                if (!convertedToInt1)
                                {
                                    return Json("Redni broj nema dobar format.");
                                } 
                                else //redni broj OK
                                {
                                    bool convertedToInt = Int32.TryParse(ukupnoBC, out ceoUkupno);
                                    if (!convertedToInt)
                                    {
                                        return Json("Broj ukupno nema dobar format.");
                                    }
                                    else
                                    {
                                        return Json("Format OK");
                                    }
                                }

                            }
                           
                        }
                        
                    }
                }
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using WaterHeaterTest.Data;
using WaterHeaterTest.Models;

namespace WaterHeaterTest.Controllers
{
    public class VisualCorrectness : Controller
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly PZIPPContext _pzippContext;

        public VisualCorrectness(ApplicationDbContext appDbContext, PZIPPContext pzippContext)
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
            int idSerijskiBroj = Get_IdSerijskiBrojBojler(barcodeForwarded);

            if (idSerijskiBroj == 0)
            {
                return Json("Greška u kominikaciji sa bazom.");
            }

            #region Akt_Vizuelna_Ispravnost

                Akt_Vizuelna_Ispravnost_Bojler vizuelnaIspravnost = new Akt_Vizuelna_Ispravnost_Bojler
                {
                    IdSerijskiBrojBojler = idSerijskiBroj,
                    Ispravan = true,
                    IdAgrVizuelnaGreskaOpis = null,
                    IdKorisnikKontrolisao = _appDbContext.LoginUsers.SingleOrDefault(user => user.UserName == User.Identity.Name).Id,
                    DatumKontrolisanja = DateTime.Now
                };

                _pzippContext.Akt_Vizuelna_Ispravnost_Bojler.Add(vizuelnaIspravnost);

                try
                {
                    _pzippContext.SaveChanges();
                }
                catch (ArgumentException)
                {
                    return Json("Greška u kominikaciji sa bazom.");
                }
                catch (Exception)
                {
                    return Json("Podaci nisu uneti u bazu.");
                }
            #endregion

            return Json("Uspešno sačuvan test.");
        }

        [HttpPost]
        public IActionResult TestSubmitNotOK(string barCode)
        {
            dynamic podaci = new ExpandoObject();
            try
            {
                podaci.barCode = barCode;
                podaci.visualErrors = (from vg in _pzippContext.Sif_Vizuelna_Greska
                                       select new
                                       {
                                           Id = vg.Id,
                                           Tip = vg.Tip,
                                           Naziv = vg.Naziv
                                       }).ToList();

                if(podaci.visualErrors.Count == 0)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (ArgumentException)
            {
                return RedirectToAction("Index");
            }

            return View("VisualError", podaci);
        }

        [HttpPost]
        public IActionResult VisualErrorSubmit(string serialNo, string errId, string desc, string comm, string sveSlike)
        {
            int idSerijskiBroj = Get_IdSerijskiBrojBojler(serialNo);
            if (idSerijskiBroj == 0)
            {
                return RedirectToAction("Index");
            }

            #region Agr_VizuelnaGreskaOpis

                int idAgrVizuelnaGreska;
                Agr_Vizuelna_Greska_Opis vizuelnaGreskaOpis = new Agr_Vizuelna_Greska_Opis
                {
                    IdVizuelnaGreska = Convert.ToInt32(errId),
                    Opis = desc,
                    Komentar = comm,
                    Slika = sveSlike,
                    DatumKreiranja = DateTime.Now
                };
                _pzippContext.Agr_Vizuelna_Greska_Opis.Add(vizuelnaGreskaOpis);

                try
                {
                    _pzippContext.SaveChanges();
                    idAgrVizuelnaGreska = vizuelnaGreskaOpis.Id;
                }
                catch (ArgumentNullException)
                {
                    return Json("Agr_VizuelnaGreska nije sačuvan uspešno.");
                }
                catch (Exception)
                {
                    return Json("Agr_VizuelnaGreska nije sačuvan uspešno.");
                }
            #endregion

            #region Akt_VizuelnaIspravnost

                int errorType = _pzippContext.Sif_Vizuelna_Greska.Single(g => g.Id == Convert.ToInt32(errId)).Tip;
                bool ispravan = Ispravan(errorType, desc);

                try
                {
                    Akt_Vizuelna_Ispravnost_Bojler aktVizuelnaIspravnost = new Akt_Vizuelna_Ispravnost_Bojler
                    {
                        IdSerijskiBrojBojler = idSerijskiBroj,
                        Ispravan = ispravan,
                        IdAgrVizuelnaGreskaOpis = idAgrVizuelnaGreska,
                        IdKorisnikKontrolisao = _appDbContext.LoginUsers.SingleOrDefault(user => user.UserName == User.Identity.Name).Id,
                        DatumKontrolisanja = DateTime.Now
                    };

                    _pzippContext.Akt_Vizuelna_Ispravnost_Bojler.Add(aktVizuelnaIspravnost);
                    _pzippContext.SaveChanges();
                }
                catch (ArgumentException)
                {
                    ViewBag.Message = "Lista problema je prazna.";
                    return View("AgrVrstaProblemaOpis");
                }
                catch (DbUpdateException)
                {
                    ViewBag.Message = "Podaci nisu sačuvani u bazi.";
                    return View("AgrVrstaProblemaOpis");
                }
                catch (RuntimeBinderException)
                {
                    ViewBag.Message = "Nema interneta.";
                    return View("AgrVrstaProblemaOpis");
                }
                catch (InvalidOperationException)
                {
                    ViewBag.Message = "Nije unet ulogovan korisnik.";
                    return View("AgrVrstaProblemaOpis");
                }
                catch (Exception)
                {
                    ViewBag.Message = "Došlo je do greške!";
                    return View("AgrVrstaProblemaOpis");
                }
            #endregion

            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult SerijskiBrojExists(string serialNumber)
        {
            Sif_Serijski_Broj_Bojler serijskiBrojBojler = _pzippContext.Sif_Serijski_Broj_Bojler.SingleOrDefault(serBr => serBr.Naziv == serialNumber);

            if (serijskiBrojBojler == null)
            {
                return Json("false");
            }
            else
            {
                Akt_Vizuelna_Ispravnost_Bojler vizuelniTest = _pzippContext.Akt_Vizuelna_Ispravnost_Bojler.SingleOrDefault(avib => avib.IdSerijskiBrojBojler == serijskiBrojBojler.Id && avib.Ispravan == true);
                if (vizuelniTest == null)
                    return Json("true");
                return Json("false");
            }
        }

        #region Custom methods
        private bool Ispravan(int type, string desc)
        {
            if (type == 1 || type == 2 & desc == null)
                return true;
            return false;
        }
        private int Get_IdSerijskiBrojBojler(string serialNumber)
        {
            int idserijskiBrojBojler;
            try
            {
                idserijskiBrojBojler = _pzippContext.Sif_Serijski_Broj_Bojler.Single(serBr => serBr.Naziv == serialNumber).Id;
            }
            catch (ArgumentException)
            {
                return 0;
            }
            catch (InvalidOperationException)
            {
                return 0;
            }
            
            return idserijskiBrojBojler;
        }
        #endregion
    }
}

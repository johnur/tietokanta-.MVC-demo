using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asiakkaiden_Tilaukset.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asiakkaiden_Tilaukset.Controllers
{
    public class AsiakasController : Controller
    {
        MyyntiDBContext db = new MyyntiDBContext();
        protected override void Dispose(bool disposing) // Sulkee tietokannan
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AsiakasA(string id = "A")
        {
            var q = from a in db.Asiakas
                    where a.Sukunimi.StartsWith("A")
                    select a;

            var q2 = from t in db.Tilaus
                     select t;

            List<AsiakasLkm> asiakkaidenTilauslkm = new List<AsiakasLkm>();

            foreach (Asiakas asiakas in q)
            {
                int lkm = 0;
                foreach (Tilaus tilaus in q2)
                {
                    if (tilaus.AsiakasId == asiakas.AsiakasId)
                    {
                        lkm++;
                    }


                    //Tapa1
                    //AsiakasLkm alkm = new AsiakasLkm();
                    //alkm.Sukunimi = asiakas.Sukunimi;
                    //alkm.Etunimi = asiakas.Etunimi;
                    //alkm.Lkm = lkm;
                    //asiakkaidenTilauslkm.Add(alkm);

                    //Tapa2
                    //AsiakasLkm alkm2 = new AsiakasLkm() { Sukunimi = asiakas.Sukunimi, Etunimi = asiakas.Etunimi, Lkm = lkm };
                    //asiakkaidenTilauslkm.Add(alkm2);
                }
                    //Tapa3
                    asiakkaidenTilauslkm.Add(new AsiakasLkm() { Sukunimi = asiakas.Sukunimi, Etunimi = asiakas.Etunimi, Lkm = lkm });


                }
                 //TAPA 4
                var q3 = (from a in db.Asiakas
                          where a.Sukunimi.StartsWith("A")
                          select a).Include("Tilaus");
                //TAPA 5
                var q4 = db.Asiakas.Where(a => a.Sukunimi.StartsWith(id)).Include("Tilaus");
                List<AsiakasLkm> asiakkaidenTilauslkm2 = new List<AsiakasLkm>();
                foreach (Asiakas asiakas1 in q4)
                {
                    asiakkaidenTilauslkm2.Add(new AsiakasLkm() { Sukunimi = asiakas1.Sukunimi, Etunimi = asiakas1.Etunimi, Lkm = asiakas1.Tilaus.Count });
                }

            return View(asiakkaidenTilauslkm2);
        }
        [HttpGet]
        public IActionResult Haku(string id = "a")
        {
            var q4 = db.Tuote.Where(a => a.Nimi.ToLower().Contains(id.ToLower()));
            List<Tuote> tuoteHaku = new List<Tuote>();
            foreach (Tuote t in q4)
            {
                tuoteHaku.Add(new Tuote() { TuoteId = t.TuoteId, Nimi = t.Nimi, Tyyppi = t.Tyyppi, Hinta = t.Hinta, Tuoteryhmä = t.Tuoteryhmä });
            }
            ViewBag.th = id;
            return View(tuoteHaku);
        }
        [HttpPost]
        public IActionResult HakuKone(string nimi)
        {

            var tuotteet = db.Tuote.Where(c => c.Nimi.Contains(nimi));
            ViewBag.haku = nimi;


            return View(tuotteet);
        }
        public const string SessionKeyName = "Kori";

        public IActionResult Ostoskori(int? id)
        {
            List<Tuote> ostoskori = new List<Tuote>();
            string tuotteet = default;

            tuotteet = HttpContext.Session.GetString("kori") ?? default;
            tuotteet += id.ToString() + ";";
            HttpContext.Session.SetString("kori", tuotteet);

            string[] splitatut = tuotteet.Split(';');

            for (int i = 0; i < splitatut.Length; i++)
            {
                if (splitatut[i] != "")
                {
                    var valituttuotteet = (from t in db.Tuote
                                           where t.TuoteId == int.Parse(splitatut[i])
                                           select t).FirstOrDefault();

                    ostoskori.Add(valituttuotteet);
                }
            }
           
            return View(ostoskori);
        }

        public IActionResult Tyhjennä()
        {
            HttpContext.Session.Remove("kori");
            return RedirectToAction("Haku", "Asiakas");

        }

    }
}


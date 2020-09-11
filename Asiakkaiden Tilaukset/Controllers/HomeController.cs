using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Asiakkaiden_Tilaukset.Models;
using Microsoft.AspNetCore.Http;

namespace Asiakkaiden_Tilaukset.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        public IActionResult Index()
        {
            
            int laskuri = HttpContext.Session.GetInt32("lkm") ?? 0; //tää kysyy löytyykö integeria jolla on arvo lkm (lkm on avain ja sillä voi käydä lukemassa muuallakin)
            laskuri++;
            HttpContext.Session.SetInt32("lkm", laskuri);

            ViewBag.Lkm = laskuri; 
            return View();
        }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
           
        }
        public IActionResult Privacy(int? id)
        {

            Nullable<int> i = 123;
            i = null;
            int k = i.HasValue ? i.Value : 0;
            k = i ?? 0; // kysärikysäri tarkoittaa että i voi olla null mutta sitten sen tilalle tulee se 0
            ViewBag.ID = id;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

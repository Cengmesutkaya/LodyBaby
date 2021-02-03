using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LodyBaby.Controllers
{
    public class CorporateController : Controller
    {
        public ActionResult About()
        {
            ViewBag.Title = "Lody Baby | Hakkımızda";
            return View();
        }

        public ActionResult Docs()
        {
            ViewBag.Title = "Lody Baby | Belgeler";
            return View();
        }
        public ActionResult Vision()
        {
            ViewBag.Title = "Lody Baby | Misyon-Vizyon";
            return View();
        }
        public ActionResult Management()
        {
            ViewBag.Title = "Lody Baby | Yönetim Felsefesi";
            return View();
        }
        public ActionResult Quality()
        {
            ViewBag.Title = "Lody Baby | Kalite politkası";
            return View();
        }

        public ActionResult InfoSecurity()
        {
            ViewBag.Title = "Lody Baby | Bilgi Güvenliği Politikası";
            return View();
        }
    }
}
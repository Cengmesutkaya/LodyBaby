using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LodyBaby.Controllers
{
    public class GalleryController : Controller
    {
        public ActionResult Photo()
        {
            ViewBag.Title = "Lody Baby | Fotoğraflarımız";
            return View();
        }

        public ActionResult Video()
        {
            ViewBag.Title = "Lody Baby | Videolarımız";
            return View();
        }

        public ActionResult Ecatalog()
        {
            ViewBag.Title = "Lody Baby | E-Katalog";
            ViewBag.Keywords = "lody baby, bebek bezi, lody fabrikası, bebek bezi fabrikası, mardin bebek bezi, lody bebek bezi";
            ViewBag.Description = "2012 yılından beri Mardin'de kurulan Lody Baby, üretim ve dağıtım alanında  pek çok ilke imza atmıştır.";
            ViewBag.Images = Directory.EnumerateFiles(Server.MapPath("~/Content/images/catalog"))
                           .Select(fn => "/Content/images/catalog/" + Path.GetFileName(fn));
            return View();
        }
    }
}
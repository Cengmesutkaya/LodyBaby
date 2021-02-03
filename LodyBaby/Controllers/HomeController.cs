using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LodyBaby.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Lody Baby | Anasayfa";
            return View();
        }

        public ActionResult _Slider()
        {
            return View();
        }
        public ActionResult _Section()
        {
            return View();
        }

        public ActionResult _WhoWeAre()
        {
            return View();
        }
        public ActionResult _About()
        {
            return View();
        }

        public ActionResult _Team()
        {
            return View();
        }

        public ActionResult _Gallery()
        {
            return View();
        }

        public ActionResult _Result()
        {
            return View();
        }
    }
}
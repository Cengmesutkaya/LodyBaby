using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LodyBaby.Controllers
{
    public class BlogController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Lody Baby | Blog";
            return View();
        }

        public ActionResult Detail()
        {
            ViewBag.Title = "Lody Baby | Blog Detail";
            return View();
        }
    }
}
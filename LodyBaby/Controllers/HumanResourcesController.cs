using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LodyBaby.Controllers
{
    public class HumanResourcesController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Lody Baby | İnsan Kaynakları";
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LodyBaby.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Lody Baby | Ürünlerimiz";
            return View();
        }
    }
}
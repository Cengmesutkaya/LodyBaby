using Package_Ecommerce.Areas.Admin.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Package_Ecommerce.Areas.Admin.Controllers
{
    public class DashBoardController : BaseAdminController
    {
        public ActionResult Index()
        {
            var productModel = manager.repo_product.List();
            ViewBag.AllArticle = productModel.Count();
            ViewBag.ActiveArticle = productModel.Where(m => m.IsActive = true).Count();
            ViewBag.PasiveArticle = productModel.Where(m => m.IsActive = false).Count();

            ViewBag.AllCategory = manager.repo_category.List().Count();

            var authorModel = manager.repo_team.List();
            ViewBag.AllAuthor = authorModel.Count();
            ViewBag.ActiveAuthor = authorModel.Where(m => m.IsActive = true).Count();
            ViewBag.PasiveAuthor = authorModel.Where(m => m.IsActive = false).Count();
            return View();
        }
    }
}
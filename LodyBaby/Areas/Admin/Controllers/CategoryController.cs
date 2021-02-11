using Package_Ecommerce.Areas.Admin.Utils;
using Package_Ecommerce.DataEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Package_Ecommerce.Areas.Admin.Controllers
{
    public class CategoryController : BaseAdminController
    {
        public ActionResult Index()
        {
            return View(manager.repo_category.List());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category, string checkbox)
        {
            category.Guid = Guid.NewGuid().ToString();
            category.Createby = AdminName;
            category.CreateDate = DateTime.Now;
            if (checkbox != null)
            {
                category.IsVitrin = true;
            }
            manager.repo_category.Insert(category);
            return RedirectToAction("Index");

        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = manager.repo_category.GetById(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(Category category, string checkbox)
        {
            var editCategory = manager.repo_category.GetById(category.Id);
            editCategory.Name = category.Name;
            editCategory.Tags = category.Tags;
            editCategory.Createby = AdminName;
            editCategory.CreateDate = DateTime.Now;
            if (checkbox != null)
            {
                editCategory.IsVitrin = true;
            }
            else
            {
                editCategory.IsVitrin = false;
            }
            manager.repo_category.Update(editCategory);
            return RedirectToAction("Index");
        }
    }
}
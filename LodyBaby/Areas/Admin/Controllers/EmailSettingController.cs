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
    public class EmailSettingController : BaseAdminController
    {
        public ActionResult Index()
        {
            return View(manager.repo_email.List().FirstOrDefault());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Email email, string checkbox)
        {
            email.Createby = AdminName;
            email.CreateDate = DateTime.Now;
            if (checkbox != null)
            {
                email.EnableSsl = true;
            }
            manager.repo_email.Insert(email);
            return RedirectToAction("Index", "EmailSetting");
        }

        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Email article = manager.repo_email.GetById(Id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(Email email, string checkbox)
        {
            Email emailEdit = manager.repo_email.GetById(email.Id);
            if (checkbox != null)
            {
                email.EnableSsl = true;
            }
            else
            {
                emailEdit.EnableSsl = email.EnableSsl;
            }
            emailEdit.Host = email.Host;
            emailEdit.Port = email.Port;
            emailEdit.Password = email.Password;
            emailEdit.SmtpClient = email.SmtpClient;
            emailEdit.CCEmailOne = email.CCEmailOne;
            emailEdit.CCEmailTwo = email.CCEmailTwo;
            emailEdit.CCEmailThree = email.CCEmailThree;
            emailEdit.Updateby = AdminName;
            emailEdit.UpdateDate = DateTime.Now;
            if (checkbox != null)
            {
                emailEdit.EnableSsl = true;
            }
            manager.repo_email.Update(emailEdit);
            return RedirectToAction("Index", "EmailSetting");
        }

    }

}
using Package_Ecommerce.Areas.Admin.Utils;
using Package_Ecommerce.DataEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Package_Ecommerce.Areas.Admin.Controllers
{
    public class MemberController : BaseAdminController
    {
        public ActionResult Index()
        {
            return View(manager.repo_member.List());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Member member)
        {
            var md5password = Crypto.Hash(member.Password, "sha256");
            member.Password = md5password;
            member.Createby = AdminName;
            member.CreateDate = DateTime.Now;
            member.IsActive = true;
            manager.repo_member.Insert(member);
            return RedirectToAction("Index", "Member");
        }

           public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var member = manager.repo_member.GetById(Id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        [HttpPost]
        public ActionResult Edit(Member member)
        {
            var memberEdit = manager.repo_member.GetById(member.Id);        
            memberEdit.Name = member.Name;
            memberEdit.Surname = member.Surname;
            memberEdit.Password = member.Password;
            memberEdit.Phone = member.Phone;
            if (member.Password.Length != 64)
            {
                var Sha256password = Crypto.Hash(member.Password, "sha256");
                memberEdit.Password = Sha256password;
            }
            memberEdit.Email = member.Email;
            memberEdit.Adress = member.Adress;
            memberEdit.Updateby = AdminName;
            memberEdit.UpdateDate = DateTime.Now;        
            manager.repo_member.Update(memberEdit);
            return RedirectToAction("Index", "Member");
        }
        
        public ActionResult Passive()
        {
            return View(manager.repo_member.Where(m => m.IsActive == false));
        }

        public ActionResult Menu()
        {
            return PartialView(manager.repo_member.List());
        }

        public ActionResult ActiveOrPassive(string id)
        {
            Member member = manager.repo_member.Find(m => m.Guid == id);
            if (member.IsActive == true)
            {
                member.IsActive = false;
            }
            else
            {
                member.IsActive = true;
            }
            manager.repo_member.Save();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Member member = manager.repo_member.GetById(id);
            manager.repo_member.Delete(member);
            return Redirect("/member-passive");
        }
    }
}
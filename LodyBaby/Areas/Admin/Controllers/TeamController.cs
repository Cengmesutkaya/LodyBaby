using Package_Ecommerce.Areas.Admin.Utils;
using Package_Ecommerce.DataEntities.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Package_Ecommerce.Areas.Admin.Controllers
{
    public class TeamController : BaseAdminController
    {
        public ActionResult Index()
        {
            return View(manager.repo_team.List());
        }

        public ActionResult Create(Team team, HttpPostedFileBase File, string checkbox)
        {
            if (team.Email == null || team.About == null)
            {
                return View();
            }
            var IsExist = manager.repo_team.Find(m => m.Email == team.Email);
            if (IsExist == null)
            {
                if (checkbox != null)
                {
                    team.MemberType = Constants.MemberType.Admin;
                }
                else
                {
                    team.MemberType = Constants.MemberType.User;
                }
                team.IsActive = true;
                team.Createby = AdminName;
                team.CreateDate = DateTime.Now;
                var Sha256password = Crypto.Hash(team.Password, "sha256");
                team.Password = Sha256password;
                team.Guid = Guid.NewGuid().ToString();
                try
                {
                    if (File != null)
                    {
                        FileInfo fileinfo = new FileInfo(File.FileName);
                        WebImage image = new WebImage(File.InputStream);
                        string uzanti = (Guid.NewGuid().ToString() + fileinfo.Extension).ToLower();
                        image.Resize(100, 100, false, true);//770 boy ,440 en
                        string tamYol = "~/Content/images/team/" + uzanti;
                        image.Save(Server.MapPath(tamYol));
                        team.Image = "/Content/images/team/" + uzanti;
                    }
                    manager.repo_team.Insert(team);
                }
                catch (Exception)
                {
                    ViewBag.MemberExist = "Resim boyutu çok fazla. Lütfen başka bir resim yükleyin";
                    return View();
                }
              
            }
            else
            {
                ViewBag.MemberExist = "Aynı Email adresine ait bir kullanıcı sistemde mevcuttur.";
                return View();
            }
            return RedirectToAction("Index");

        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team author = manager.repo_team.Find(m => m.Guid == id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        [HttpPost]
        public ActionResult Edit(Team team, HttpPostedFileBase File, string checkbox)
        {
            var getTeam = manager.repo_team.Find(m => m.Guid == team.Guid);
            if (team.Password.Length != 64)
            {
                var Sha256password = Crypto.Hash(team.Password, "sha256");
                getTeam.Password = Sha256password;
            }
            if (File != null)
            {
                var filePath = Server.MapPath("~" + getTeam.Image);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
                FileInfo fileinfo = new FileInfo(File.FileName);
                WebImage image = new WebImage(File.InputStream);
                string uzanti = (Guid.NewGuid().ToString() + fileinfo.Extension).ToLower();
                image.Resize(100, 100, false, true);//770 boy ,440 en
                string tamYol = "~/Content/images/team/" + uzanti;
                image.Save(Server.MapPath(tamYol));
                getTeam.Image = "/Content/images/team/" + uzanti;
            }
            if (checkbox != null)
            {
                getTeam.MemberType = Constants.MemberType.Admin;
            }
            else
            {
                getTeam.MemberType = Constants.MemberType.User;
            }
            getTeam.About = team.About;
            getTeam.Email = team.Email;
            getTeam.Job = team.Job;
            getTeam.NameSurname = team.NameSurname;
            getTeam.UpdateDate = DateTime.Now;
            getTeam.Updateby = AdminName;
            manager.repo_team.Update(getTeam);
            return RedirectToAction("Index");
        }

        public ActionResult ActiveOrPassive(string id)
        {
            Team author = manager.repo_team.Find(m => m.Guid == id);
            if (author.IsActive == true)
            {
                author.IsActive = false;
            }
            else
            {
                author.IsActive = true;
            }
            manager.repo_team.Save();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Team author = manager.repo_team.GetById(id);
            manager.repo_team.Delete(author);
            var filePath = Server.MapPath("~" + author.Image);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            return RedirectToAction("Index","Team");
        }
    }

}
using Package_Ecommerce.Areas.Admin.Utils;
using Package_Ecommerce.BussinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Package_Ecommerce.Areas.Admin.Controllers
{
    public class LoginController :Controller
    {
        public Manager manager = new Manager();
        public ActionResult SignIn(string email, string password)
        {
            if (email == null || password == null)
            {
                return View();
            }
            else
            {
                var md5password = Crypto.Hash(password, "sha256");
                var admin = manager.repo_team.Find(m => m.Email == email && m.Password == md5password && m.IsActive == true);
                if (admin != null)
                {
                    Session["AdminName"] = admin.NameSurname;
                    Session["AdminId"] = admin.Id;
                    Session["AdminImage"] = admin.Image;
                    Session["AdminEmail"] = admin.Email;
                    return RedirectToAction("Index", "Team");
                }
                else
                {
                    ViewBag.Error = "Kullanıcı adı veya şifre hatalı!";
                    return View();
                }
            }
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("SignIn", "Login");
        }
    }
}
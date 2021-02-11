using Package_Ecommerce.BussinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Package_Ecommerce.Areas.Admin.Utils
{
    public class BaseAdminController : Controller
    {
        public Manager manager = new Manager();
        public bool IsLogin { get; private set; }
        public string AdminName { get; private set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["AdminName"] == null)
            {
                filterContext.Result = new RedirectResult("/admin/system-login");
            }
            else
            {
                IsLogin = true;
                AdminName = Session["AdminName"].ToString();
            }           
            base.OnActionExecuting(filterContext);
        }
    }
}
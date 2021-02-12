using Areas.Admin.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Areas.Admin.Controllers
{
    public class OrderCustomerController : BaseAdminController
    {
        public ActionResult Index()
        {
            return View(manager.repo_order.List());
        }

        public ActionResult Detail(int? id)
        {
            var orderdetail = manager.repo_orderdetail.Where(m=>m.OrderId == id).ToList();
            return View(orderdetail);
        }
    }
}
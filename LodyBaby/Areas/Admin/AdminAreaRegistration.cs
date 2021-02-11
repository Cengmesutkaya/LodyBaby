using System.Web.Mvc;

namespace Package_Ecommerce.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
            name: "Email",
            url: "email-list",
            defaults: new { controller = "EmailSetting", action = "Index" });

            context.MapRoute(
            name: "OrderCustomer",
            url: "ordercustomer-list",
            defaults: new { controller = "OrderCustomer", action = "Index" });

          //  context.MapRoute(
          //name: "MemberPassive",
          //url: "member-list/{parameter}",
          //defaults: new { controller = "Member", action = "Index", id = "" });

            context.MapRoute(
            name: "OrderCustomerdetail",
            url: "Order-customer-detail/{id}",
            defaults: new { controller = "OrderCustomer", action = "Detail", id = "" });

            context.MapRoute(
            name: "EmailEdit",
            url: "email-edit/{id}",
            defaults: new { controller = "EmailSetting", action = "Edit", id = "" });
            context.MapRoute(
            name: "Logout",
            url: "system-out",
            defaults: new { controller = "Login", action = "LogOut" });
            context.MapRoute(
            name: "Login",
            url: "admin/system-login",
            defaults: new { controller = "Login", action = "SignIn" });
            context.MapRoute(
            name: "TeamCreate",
            url: "team-create",
            defaults: new { controller = "Team", action = "Create" });
            context.MapRoute(
             name: "Team",
             url: "team-list",
             defaults: new { controller = "Team", action = "Index" });
            context.MapRoute(
            name: "TeamEdit",
            url: "team-edit/{id}",
            defaults: new { controller = "Team", action = "Edit", id = "" });

            context.MapRoute(
            name: "OfferCreate",
            url: "offerform-create",
            defaults: new { controller = "OfferForm", action = "Create" });
            context.MapRoute(
            name: "Offer",
            url: "offerform-list",
            defaults: new { controller = "OfferForm", action = "Index" });
            context.MapRoute(
            name: "OfferEdit",
            url: "offerform-edit/{id}",
            defaults: new { controller = "OfferForm", action = "Edit", id = "" });
            context.MapRoute(
           name: "OfferDetail",
            url: "offerform-detail/{id}",
            defaults: new { controller = "OfferForm", action = "Detail", id = "" });
            context.MapRoute(
           name: "OfferSendEmail",
            url: "offerform-sendemail/{id}",
            defaults: new { controller = "OfferForm", action = "SendEmail", id = "" });


            context.MapRoute(
            name: "WorkerCreate",
            url: "workorder-create",
            defaults: new { controller = "WorkOrder", action = "Create" });
            context.MapRoute(
            name: "worker",
            url: "workorder-list",
            defaults: new { controller = "WorkOrder", action = "Index" });
            context.MapRoute(
            name: "workerEdit",
            url: "workorder-edit/{id}",
            defaults: new { controller = "WorkOrder", action = "Edit", id = "" });
  
   

            context.MapRoute(
            name: "Category",
            url: "category-list",
            defaults: new { controller = "Category", action = "Index" });
            context.MapRoute(
            name: "categoryEdit",
            url: "category-edit/{id}",
            defaults: new { controller = "Category", action = "Edit", id = "" });
            context.MapRoute(
            name: "CategoryCreate",
            url: "category-create",
            defaults: new { controller = "Category", action = "Create" });

            context.MapRoute(
            name: "ProductCreate",
            url: "product-create",
            defaults: new { controller = "Product", action = "Create" });

            context.MapRoute(
            name: "ProductEdit",
            url: "Product-edit/{id}",
            defaults: new { controller = "Product", action = "Edit", id = "" });

            context.MapRoute(
            name: "Product",
            url: "product-list",
            defaults: new { controller = "Product", action = "Index" });
          
            context.MapRoute(
            name: "ProductDelete",
            url: "Product-delete/{id}",
            defaults: new { controller = "Product", action = "Delete", id = "" });


            context.MapRoute(
           name: "MemberCreate",
           url: "member-create",
           defaults: new { controller = "Member", action = "Create" });
            context.MapRoute(
            name: "Member",
            url: "member-list",
            defaults: new { controller = "Member", action = "Index" , id = "" });
            context.MapRoute(
            name: "MemberEdit",
            url: "member-edit/{id}",
            defaults: new { controller = "Member", action = "Edit", id = "" });

            context.MapRoute(
         name: "MemberPassive",
         url: "member-passive",
         defaults: new { controller = "Member", action = "Passive"});
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
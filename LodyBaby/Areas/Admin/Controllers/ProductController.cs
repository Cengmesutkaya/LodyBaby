using Package_Ecommerce.Areas.Admin.Utils;
using Package_Ecommerce.DataEntities.Models;
using Package_Ecommerce.Models;
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
    public class ProductController : BaseAdminController
    {
        public ActionResult Index()
        {
            List<ProductModel> productModel = null;
            productModel = (from product in manager.repo_product.List()
                            join category in manager.repo_category.List() on product.CategoryId equals category.Id
                            select new ProductModel
                            {
                                Id = product.Id,
                                Date = product.CreateDate,
                                Name = product.Name,
                                CreatedBy = product.Createby,
                                Price = product.Price,
                                CategoryName = category.Name,
                                Discount = product.Discount,
                                IsActive = product.IsActive,
                                ImageOne = product.ImageOne,
                                Guid = product.Guid,
                            }).OrderByDescending(m => m.Id).ToList();
            return View(productModel);
        }

        public void DropdownCategory()
        {
            List<SelectListItem> category = (from b in manager.repo_category.List()
                                             select new SelectListItem
                                             {
                                                 Text = b.Name,
                                                 Value = b.Id.ToString()
                                             }).ToList();
            ViewBag.Category = new SelectList(category, "Value", "Text");
        }

        public ActionResult Create()
        {
            DropdownCategory();
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase imageOne, HttpPostedFileBase imageTwo, string checkbox)
        {
            try
            {
                var model = manager.repo_product.List().Last();
                product.IsActive = true;
                product.CreateDate = DateTime.Now;
                product.Createby = AdminName;
                product.ReadingCount = 10;
                product.Guid = Guid.NewGuid().ToString();
                if (checkbox != null)
                {
                    product.Vitrin = true;
                }
                if (imageOne != null)
                {
                    FileInfo fileinfo = new FileInfo(imageOne.FileName);
                    WebImage image = new WebImage(imageOne.InputStream);
                    string uzanti = (Extensions.UrlDuzenleme.UrlCevir(product.Name) + fileinfo.Extension).ToLower();
                    image.Resize(450, 500, false, true);
                    string tamYol = "~/Content/images/product/" + uzanti;
                    image.Save(Server.MapPath(tamYol));
                    product.ImageOne = "/Content/images/product/" + uzanti;
                }
                if (imageTwo != null)
                {
                    FileInfo fileinfo = new FileInfo(imageTwo.FileName);
                    WebImage image = new WebImage(imageTwo.InputStream);
                    string uzanti = (Extensions.UrlDuzenleme.UrlCevir(product.Name) +"-" + fileinfo.Extension).ToLower();
                    image.Resize(450, 500, false, true);
                    string tamYol = "~/Content/images/product/" + uzanti;
                    image.Save(Server.MapPath(tamYol));
                    product.ImageTwo = "/Content/images/product/" + uzanti;
                }
                manager.repo_product.Insert(product);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ViewBag.Error = "Resim boyutu çok fazla. Lütfen daha küçük boyutlu bir resim ekleyin";
                return View();
            }
        }

        public ActionResult Edit(string Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = manager.repo_product.Find(m => m.Guid == Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            DropdownCategory();
            return View(product);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase imageOne, HttpPostedFileBase imageTwo, string checkbox)
        {
            Product productEdit = manager.repo_product.Find(m => m.Guid == product.Guid);
            try
            {
                if (imageOne != null)
                {
                    var filePath = Server.MapPath("~" + productEdit.ImageOne);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    FileInfo fileinfo = new FileInfo(imageOne.FileName);
                    WebImage image = new WebImage(imageOne.InputStream);
                    string uzanti = (Extensions.UrlDuzenleme.UrlCevir(product.Name).ToLower() + fileinfo.Extension).ToLower();
                    image.Resize(450, 500, false, true);
                    string tamYol = "~/Content/images/product/" + uzanti;
                    image.Save(Server.MapPath(tamYol));
                    productEdit.ImageOne = "/Content/images/product/" + uzanti;
                }
                if (imageTwo != null)
                {
                    var filePath = Server.MapPath("~" + productEdit.ImageTwo);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    FileInfo fileinfo = new FileInfo(imageTwo.FileName);
                    WebImage image = new WebImage(imageTwo.InputStream);
                    string uzanti = (Extensions.UrlDuzenleme.UrlCevir(product.Name) + "-" + fileinfo.Extension).ToLower();
                    image.Resize(450, 500, false, true);
                    string tamYol = "~/Content/images/product/" + uzanti;
                    image.Save(Server.MapPath(tamYol));
                    productEdit.ImageTwo = "/Content/images/product/" + uzanti;
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Resim boyutu çok fazla. Lütfen daha küçük boyutlu bir resim ekleyin";
                return View();
            }

            productEdit.Name = product.Name;
            productEdit.ProductCode = product.ProductCode;
            productEdit.Summary = product.Summary;
            productEdit.Discount = product.Discount;
            productEdit.Price = product.Price;
            productEdit.Description = product.Description;
            productEdit.CategoryId = product.CategoryId;
            productEdit.Tags = product.Tags;
            productEdit.MinValue = product.MinValue;
            productEdit.PropertyProduct = product.PropertyProduct;
            productEdit.Updateby = AdminName;
            productEdit.UpdateDate = DateTime.Now;
            if (checkbox != null)
            {
                productEdit.Vitrin = true;
            }
            else
            {
                productEdit.Vitrin = false;
            }
            manager.repo_product.Update(productEdit);
            return RedirectToAction("Index");
        }

        public ActionResult ActiveOrPassive(string Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = manager.repo_product.Find(m=>m.Guid == Id);
            if (product != null)
            {
                if (product.IsActive == true)
                {
                    product.IsActive = false;
                }
                else
                {
                    product.IsActive = true;
                }
                manager.repo_product.Save();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = manager.repo_product.GetById(Id);
            manager.repo_product.Delete(product);
            var imageOne = Server.MapPath("~" + product.ImageOne);
            if (System.IO.File.Exists(imageOne))
            {
                System.IO.File.Delete(imageOne);
            }
            var imageTwo = Server.MapPath("~" + product.ImageTwo);
            if (System.IO.File.Exists(imageTwo))
            {
                System.IO.File.Delete(imageTwo);
            }
            return Redirect("/product-list");
        }
    }
}
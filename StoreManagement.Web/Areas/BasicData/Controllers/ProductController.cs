using StoreManagement.Web.Areas.BasicData.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using StoreManagement.Common.Models;
using StoreManagement.Framework.Common;
using StoreManagement.Common.EntityServices;

namespace StoreManagement.Web.Areas.BasicData.Controllers
{
    [RouteArea("BasicData")]
    public partial class ProductController : Controller
    {
        #region List
        public virtual ActionResult List()
        {


            var service = ServiceFactory.Create<IProductService>();
            var list = service.FetchViewModels();

            ViewBag.List = list;
            ViewBag.Type = typeof(Product);
            return View("EntityList");
            //}
        }
        #endregion

        #region Create
        public virtual ActionResult Create()
        {
            AddProductViewModel product = new AddProductViewModel();
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(AddProductViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "فیلدهای مورد نظر را وارد کنید.");
                return View(viewModel);
            }
            using (var db = new ApplicationDbContext())
            {
                var product = new Product
                {
                    Code = viewModel.Code,
                    CategoryId = viewModel.CategoryId,
                    Category = db.Categories.Find(viewModel.CategoryId),
                    Price = viewModel.Price,
                    Name = viewModel.Name,
                    Description = viewModel.Description
                };
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("List");
            }
        }
        #endregion

        #region Edit

        [HttpGet]
        public virtual ActionResult Edit(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            using (var db = new ApplicationDbContext())
            {
                var viewModel = db.Products
                    .Select(p => new EditProductViewModel
                    {
                        Name = p.Name,
                        Code = p.Code,
                        Price = p.Price,
                        Description = p.Description,
                        Id = p.Id,
                        CategoryId = p.CategoryId,
                        Version = p.Version
                    }).FirstOrDefault(p => p.Id == id);
                if (viewModel == null)
                    return HttpNotFound();
                return View(viewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(EditProductViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "فیلدهای مورد نظر را وارد کنید.");
                return View(viewModel);
            }
            var db = new ApplicationDbContext();
            try
            {
                var product = new Product
                {
                    Id = viewModel.Id,
                    Name = viewModel.Name,
                    Code = viewModel.Code,
                    Description = viewModel.Description,
                    CategoryId = viewModel.CategoryId,
                    Version = viewModel.Version,
                    Price = viewModel.Price
                };
                db.Entry<Product>(product).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("List");
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError("", "کالا مورد نظر توسط کاربر دیگری در شبکه، تغییر یافته است. برای ادامه صفحه را رفرش کنید.");
                return View(viewModel);
            }
            finally
            {
                db.Dispose();
            }
        }
        #endregion

        #region Details

        public virtual ActionResult Details(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var viewModel = ServiceFactory.Create<IProductService>().FetchViewModels()
                .FirstOrDefault(p => p.Id == id);
            if (viewModel == null)
                return HttpNotFound();
            return View(viewModel);
        }
        #endregion

        #region Delete

        public virtual ActionResult Delete(long id)         // this is not the real implemetation
        {
            using (var db = new ApplicationDbContext())
            {
                var product = new Product { Id = id };

                //db.Entry<Product>(product).State = System.Data.Entity.EntityState.Deleted;      // jeddan chera :(

                var temp = db.Products.Find(id);
                if (temp != null)
                {
                    db.Products.Remove(temp);
                    db.SaveChanges();
                }

            }
            return RedirectToAction("List");

        }
        #endregion

        #region RemoteValidation
        [HttpPost]
        public virtual JsonResult CodeExist(string code, long? Id)
        {
            var exist = ServiceFactory.Create<IProductService>().CheckCodeExist(code, Id);
            return Json(!exist);
        }

        [HttpPost]
        public virtual JsonResult NameExist(string name, long? Id)
        {
            var exist = ServiceFactory.Create<IProductService>().CheckNameExist(name, Id);
            return Json(!exist);
        }
        #endregion
    }
}
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
            var list = service.FetchAllAndProject(p => new ProductViewModel
            {
                Code = p.Code,
                Name = p.Name,
                Price = p.Price,
                Category = p.Category.Title,
                Id = p.Id,
                Description = p.Description
            });

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
            ServiceFactory.Create<IProductService>()
                .Create(new Product
                {
                    Code = viewModel.Code,
                    CategoryId = viewModel.CategoryId,
                    Price = viewModel.Price,
                    Name = viewModel.Name,
                    Description = viewModel.Description
                });

            return RedirectToAction("List");
        }
        #endregion

        #region Edit

        [HttpGet]
        public virtual ActionResult Edit(long? id)      // returns the viewmodel doesn't change anything
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            EditProductViewModel viewModel = ServiceFactory.Create<IProductService>()
                .FetchByIdAndProject(id.Value, a => new EditProductViewModel
                {
                    Id = a.Id,
                    CategoryId = a.CategoryId,
                    Code = a.Code,
                    Description = a.Description,
                    Name = a.Name,
                    Price = a.Price,
                    Version = a.Version
                });

            if (viewModel == null)
                return HttpNotFound();
            return View(viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(EditProductViewModel viewModel)    // changes an entity redirects to list
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "فیلدهای مورد نظر را وارد کنید.");
                return View(viewModel);
            }
            try
            {
                ServiceFactory.Create<IProductService>()
                    .Save(new Product
                    {
                        Id = viewModel.Id,
                        Name = viewModel.Name,
                        Code = viewModel.Code,
                        Description = viewModel.Description,
                        CategoryId = viewModel.CategoryId,
                        Version = viewModel.Version,
                        Price = viewModel.Price
                    });
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError("", "کالا مورد نظر توسط کاربر دیگری در شبکه، تغییر یافته است. برای ادامه صفحه را رفرش کنید.");
                return View(viewModel);
            }
            return RedirectToAction("List");
        }
        #endregion


        #region Details

        public virtual ActionResult Details(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var viewModel = ServiceFactory.Create<IProductService>().FetchByIdAndProject(id.Value, p => new ProductViewModel
            {
                Category = p.Category.Title,
                Code = p.Code,
                Description = p.Description,
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            });
            if (viewModel == null)
                return HttpNotFound();
            return View(viewModel);
        }
        #endregion

        #region Delete

        public virtual ActionResult Delete(long id)         // maybe this must only check is_deleted
        {
            ServiceFactory.Create<IProductService>().DeleteById(id);
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
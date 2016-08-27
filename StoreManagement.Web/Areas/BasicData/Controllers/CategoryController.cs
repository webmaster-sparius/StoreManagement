using StoreManagement.Common.EntityServices;
using StoreManagement.Web.Areas.BasicData.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreManagement.Common.Models;
using StoreManagement.Framework.Common;

namespace StoreManagement.Web.Areas.BasicData.Controllers
{
    [RouteArea("BasicData")]
    public partial class CategoryController : Controller
    {
        #region List

        public virtual ActionResult List()
        {
            var list = ServiceFactory.Create<ICategoryService>().FetchAll().
                Select(c => CategoryViewModel.FromModel(c));
            ViewBag.Type = typeof(Category);
            ViewBag.List = list;

            return View("EntityList");
        }
        #endregion

        #region Create

        [HttpGet]
        public virtual ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public virtual ActionResult Create(AddCategoryViewModel viewModel)
        {
            var catService = ServiceFactory.Create<ICategoryService>();

            if (catService.CheckTitleExist(viewModel.Title, null))
                ModelState.AddModelError("Title", "یک گروه با این عنوان قبلا در سیستم ثبت شده است.");

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "بار آخرتون باشه.");
                return View(viewModel);
            }

            using (var db = new ApplicationDbContext())
            {

                var category = new Category { Title = viewModel.Title };
                db.Categories.Add(category);
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }

        #endregion

        #region Edit
        [HttpGet]
        public virtual ActionResult Edit(long id)
        {
            using (var db = new ApplicationDbContext())
            {
                var viewModel = db.Categories
                    .Select(a => new EditCategoryViewModel
                    {
                        Id = a.Id,
                        Title = a.Title,
                        Version = a.Version
                    }).FirstOrDefault(a => a.Id == id);

                if (viewModel == null)
                    return HttpNotFound();

                return View(viewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(EditCategoryViewModel viewModel)
        {
            if (ServiceFactory.Create<ICategoryService>().CheckTitleExist(viewModel.Title, viewModel.Id))
                ModelState.AddModelError("Title", "یک گروه با این عنوان قبلا در سیستم ثبت شده است.");
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "آخرین بارت باشه.");
                return View(viewModel);
            }

            var db = new ApplicationDbContext();
            try
            {
                var category = new Category
                {
                    Id = viewModel.Id,
                    Title = viewModel.Title,
                    Version = viewModel.Version
                };

                db.Entry<Category>(category).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("List");
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError("", "گروه مورد نظر توسط کاربر دیگری در شبکه، تغییر یافته است. برای ادامه صفحه را رفرش کنید.");
                return View(viewModel);
            }
            finally
            {
                db.Dispose();
            }
        }
        #endregion

        #region Delete
        [HttpPost]
        public virtual ActionResult Delete(long id, byte[] version)
        {
            using (var db = new ApplicationDbContext())
            {
                var category = new Category { Id = id, Version = version };
                db.Entry<Category>(category).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }
        #endregion

        #region RemoteValidations
        [HttpPost]
        public virtual JsonResult TitleExist(string title, long? id)
        {
            var exist = ServiceFactory.Create<ICategoryService>().CheckTitleExist(title, id);
            return Json(!exist);
        }
        #endregion
    }
}
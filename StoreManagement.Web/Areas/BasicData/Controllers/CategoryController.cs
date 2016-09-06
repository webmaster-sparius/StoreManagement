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
using System.Net;

namespace StoreManagement.Web.Areas.BasicData.Controllers
{
    [RouteArea("BasicData")]
    public partial class CategoryController : Controller
    {
        #region List

        public ActionResult List()
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

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "بار آخرتون باشه.");
                return View(viewModel);
            }

            ServiceFactory.Create<ICategoryService>()
                .Create(new Category { Title = viewModel.Title });
            
            return RedirectToAction("List");
        }

        #endregion

        #region Edit
        [HttpGet]
        public virtual ActionResult Edit(long id)
        {
            EditCategoryViewModel viewModel = ServiceFactory.Create<ICategoryService>().FetchByIdAndProject(id, a => new EditCategoryViewModel
            {
                Id = a.Id,
                Title = a.Title,
                Version = a.Version
            });

                if (viewModel == null)
                    return HttpNotFound();
                return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(EditCategoryViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "آخرین بارت باشه.");
                return View(viewModel);
            }
            try
            {
                ServiceFactory.Create<ICategoryService>()
                    .Save(new Category
                    {
                        Id = viewModel.Id,
                        Title = viewModel.Title,
                        Version = viewModel.Version
                    });
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError("", "گروه مورد نظر توسط کاربر دیگری در شبکه، تغییر یافته است. برای ادامه صفحه را رفرش کنید.");
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
            var viewModel = ServiceFactory.Create<ICategoryService>().FetchAll()
                .Select(v => new CategoryViewModel
                {
                    Id = v.Id,
                    Title = v.Title
                }).FirstOrDefault(v => v.Id == id);
            if (viewModel == null)
                return HttpNotFound();
            return View(viewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        public virtual ActionResult Delete(long id)
        {

            ServiceFactory.Create<ICategoryService>().DeleteById(id);
            return RedirectToAction("List");
        }
        #endregion

        #region Search
        [HttpGet]
        public ActionResult Search(string title)
        {
            var list = ServiceFactory.Create<ICategoryService>().FetchByTitle(title).Select(c => CategoryViewModel.FromModel(c));
            ViewBag.Type = typeof(Category);
            ViewBag.List = list;
            ViewBag.RowsAffected = list.Count();
            return View("EntityList");
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
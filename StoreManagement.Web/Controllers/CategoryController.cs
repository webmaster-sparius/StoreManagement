using StoreManagement.Web.Models;
using StoreManagement.Web.Services;
using StoreManagement.Web.ViewModels.Category;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreManagement.Web.Controllers
{
    [RoutePrefix("Category")]
    [Route("{action}")]
    public partial class CategoryController : Controller
    {
        #region Fields
        /// <summary>
        /// todo: use dependency injection
        /// </summary>
        private readonly ICategoryService _categoryService;
        #endregion

        #region Constructor
        /// <summary>
        /// todo:use dependency injection
        /// </summary>
        /// <param name="categoryService"></param>
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public CategoryController()
        {

        }
        #endregion

        #region List
        public virtual ActionResult List()
        {
            using (var db = new ApplicationDbContext())
            {
                var categories = db.Categories
                    .Select(category => new CategoryViewModel { Title = category.Title, Id = category.Id })
                    .ToList();

                return View(categories);
            }

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
            // best practice 
            if (new CategoryService().CheckTitleExist(viewModel.Title, null))
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
            if (new CategoryService().CheckTitleExist(viewModel.Title, viewModel.Id))
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

                return RedirectToAction("Edit", new { id = viewModel.Id });
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

        #region RemoteValidations
        [Route(Name = "UniqueCategoryTitle")]
        [HttpPost]
        public virtual JsonResult TitleExist(string title, long? id)
        {
            var exist = new CategoryService().CheckTitleExist(title, id);
            return Json(!exist);
        }
        #endregion
    }
}
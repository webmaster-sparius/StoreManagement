using StoreManagement.Web.Models;
using StoreManagement.Web.Services;
using StoreManagement.Web.ViewModels.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreManagement.Web.Controllers
{
    [RoutePrefix("Category")]
    [Route("{action}")]
    public class CategoryController : Controller
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
        public ActionResult List()
        {
            using (var db = new ApplicationDbContext())
            {
                var categories = db.Categories
                    .Select(category => new CategoryViewModel { Title = category.Title })
                    .ToList();

                return View(categories);
            }

        }
        #endregion

        #region Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(AddCategoryViewModel viewModel)
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

        #endregion

        #region RemoteValidations
        [Route(Name = "UniqueCategoryTitle")]
        [HttpPost]
        public JsonResult TitleExist(string title, long? id)
        {
            var exist = new CategoryService().CheckTitleExist(title, id);
            return Json(!exist);
        }
        #endregion
    }
}
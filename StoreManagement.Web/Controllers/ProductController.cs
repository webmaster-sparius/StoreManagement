﻿using StoreManagement.Web.Models;
using StoreManagement.Web.Services;
using StoreManagement.Web.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;

namespace StoreManagement.Web.Controllers
{
    public partial class ProductController : Controller
    {
        #region List
        public virtual ActionResult List()
        {
            using (var db = new ApplicationDbContext())
            {
                var products = db.Products.Include(a => a.Category)
                    .Where(p => p.IsDeleted == false)
                    .Select(p => new ProductViewModel
                    { Code = p.Code, Name = p.Name, Price = p.Price, Category = p.Category.Title, Id = p.Id })
                    .ToList();
                return View(products);
            }
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
            var productService = new ProductService();
            if (productService.CheckCodeExist(viewModel.Code, null))
                ModelState.AddModelError("Code", "یک کالا با این کد قبلا در سیستم ثبت شده است.");
            if (productService.CheckNameExist(viewModel.Name, null))
                ModelState.AddModelError("Name", "یک کالا با این نام قبلا در سیستم ثبت شده است.");
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
            var productService = new ProductService();
            if (productService.CheckCodeExist(viewModel.Code, viewModel.Id))
                ModelState.AddModelError("Code", "یک کالا با این کد قبلا در سیستم ثبت شده است.");
            if (productService.CheckNameExist(viewModel.Name, viewModel.Id))
                ModelState.AddModelError("Name", "یک کالا با این نام قبلا در سیستم ثبت شده است.");
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

            using (var db = new ApplicationDbContext())
            {
                var viewModel = db.Products
                    .Select(p => new ProductViewModel
                    {
                        Id = p.Id,
                        Code = p.Code,
                        Name = p.Name,
                        Price = p.Price,
                        Category = p.Category.Title,
                        Description = p.Description
                    })
                    .FirstOrDefault(p => p.Id == id);
                if (viewModel == null)
                    return HttpNotFound();
                return View(viewModel);
            }
        }
        #endregion

        #region Delete
        
        public virtual ActionResult Delete(long id)         // this is not the real implemetation
        {
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
        #endregion
    }
}
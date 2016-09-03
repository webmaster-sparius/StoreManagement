﻿using StoreManagement.Common.EntityServices;
using StoreManagement.Common.Models;
using StoreManagement.Web.Areas.BasicData.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreManagement.Business.EntityServices
{
    public class CategoryService : ICategoryService
    {
        #region ICategoryService
        
        public bool CheckTitleExist(string title, long? id)
        {
            using (var db = new ApplicationDbContext())
            {
                var categories = db.Categories;

                var exist = id.HasValue ?
                   categories.Any(a => a.Id != id.Value && a.Title == title) :
                    categories.Any(a => a.Title == title);

                return exist;
            }
        }

        public IEnumerable<Category> FetchAll()
        {
            using (var db = new ApplicationDbContext())
                return db.Categories.ToList();
        }


		public EditCategoryViewModel FetchEditViewModel(long? id)
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
                return viewModel;
            }
        }

        public IEnumerable<Category> FetchByTitle(string title)
		{
            using (var db = new ApplicationDbContext())
            {
                return db.Categories.Where(a => a.Title == title).ToList();
            }
		}

        public void EditByViewModel(EditCategoryViewModel viewModel)
        {
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
            }
            finally
            {
                db.Dispose();
            }
        }

        public void DeleteById(long id)
        {
            using (var db = new ApplicationDbContext())
            {
                var category = new Category { Id = id };

                //db.Entry<Product>(product).State = System.Data.Entity.EntityState.Deleted;      // jeddan chera :(

                var temp = db.Categories.Find(id);
                if (temp != null)
                {
                    db.Categories.Remove(temp);
                    db.SaveChanges();
                }

            }
        }

        public void CreateByViewModel(AddCategoryViewModel viewModel)
        {
            using (var db = new ApplicationDbContext())
            {

                var category = new Category { Title = viewModel.Title };
                db.Categories.Add(category);
                db.SaveChanges();
            }
        }

        #endregion
    }
}
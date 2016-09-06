using StoreManagement.Common.EntityServices;
using StoreManagement.Common.Models;
using StoreManagement.Framework.Common;
using StoreManagement.Web.Areas.BasicData.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreManagement.Business.EntityServices
{
    public class CategoryService : EntityService<Category>, ICategoryService
    {
        #region ICategoryService

        public bool CheckTitleExist(string title, long? id)
        {
            var db = Repository.Current;
            var categories = db.Set<Category>();

            var exist = id.HasValue ?
               categories.Any(a => a.Id != id.Value && a.Title == title) :
                categories.Any(a => a.Title == title);

            return exist;
        }

        public IEnumerable<Category> FetchByTitle(string title)
        {
            var db = Repository.Current;
            return db.Set<Category>().Where(a => a.Title == title).ToList();
        }

        public void CreateByViewModel(AddCategoryViewModel viewModel)
        {
            var db = Repository.Current;
            var category = new Category { Title = viewModel.Title };
            db.Set<Category>().Add(category);
            db.SaveChanges();
        }

        #endregion
    }
}
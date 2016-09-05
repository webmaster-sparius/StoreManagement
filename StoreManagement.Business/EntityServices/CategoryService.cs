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
    public class CategoryService : ICategoryService
    {
        #region ICategoryService
        
        public bool CheckTitleExist(string title, long? id)
        {
            using (var db = new Repository())
            {
                var categories = db.Set<Category>();

                var exist = id.HasValue ?
                   categories.Any(a => a.Id != id.Value && a.Title == title) :
                    categories.Any(a => a.Title == title);

                return exist;
            }
        }

        public IEnumerable<Category> FetchAll()
        {
            using (var db = new Repository())
                return db.Set<Category>().ToList();
        }

        public IEnumerable<Category> FetchByTitle(string title)
		{
            using (var db = new Repository())
            {
                return db.Set<Category>().Where(a => a.Title == title).ToList();
            }
		}

        public void DeleteById(long id)
        {
            using (var db = new Repository())
            {
                var category = new Category { Id = id };

                //db.Entry<Product>(product).State = System.Data.Entity.EntityState.Deleted;      // jeddan chera :(

                var temp = db.Set<Category>().Find(id);
                if (temp != null)
                {
                    db.Set<Category>().Remove(temp);
                    db.SaveChanges();
                }

            }
        }

        public void CreateByViewModel(AddCategoryViewModel viewModel)
        {
            using (var db = new Repository())
            {

                var category = new Category { Title = viewModel.Title };
                db.Set<Category>().Add(category);
                db.SaveChanges();
            }
        }

        #endregion
    }
}
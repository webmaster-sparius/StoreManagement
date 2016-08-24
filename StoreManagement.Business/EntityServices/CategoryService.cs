using StoreManagement.Common.EntityServices;
using StoreManagement.Common.Models;
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
        #endregion
    }
}
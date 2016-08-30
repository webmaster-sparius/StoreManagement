using StoreManagement.Common.EntityServices;
using StoreManagement.Common.Models;
using StoreManagement.Framework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using StoreManagement.Web.Areas.BasicData.ViewModels;
using System.Data.Entity;
namespace StoreManagement.Business.EntityServices
{
    public class ProductService : IProductService
    {
        #region IProductService

        public bool CheckNameExist(string name, long? id)
        {
            var products = new ApplicationDbContext().Products;
            var exist = id.HasValue ?
                products.Any(p => p.Id != id && p.Name == name) :
                products.Any(p => p.Name == name);
            return exist;
        }

        public bool CheckCodeExist(string code, long? id)
        {
            var products = new ApplicationDbContext().Products;
            var exist = id.HasValue ?
                products.Any(p => p.Id != id && p.Code == code) :
                products.Any(p => p.Code == code);
            return exist;
        }


        public IEnumerable<Product> FetchAll()
        {
            List<Product> products;
            using (var db = new ApplicationDbContext())
                products = db.Products.ToList();
            return products;
        }

        #endregion

        public IEnumerable<ProductViewModel> FetchViewModels()
        {
            using (var db = new ApplicationDbContext())
            {

                var list = db.Products.Include(a => a.Category).Where(p => !p.IsDeleted)
                    .Select(p => new ProductViewModel
                    { Code = p.Code, Name = p.Name, Price = p.Price, Category = p.Category.Title, Id = p.Id, Description = p.Description })
                            .ToList();
                return list;
            }
        }

        public EditProductViewModel FetchEditViewModel(long? id)
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
        //        if (viewModel == null)
                    return viewModel;
            }
        //     return null;
          //  return viewModel;
        }


        public void EditByViewModel(EditProductViewModel viewModel)
        {
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
            }
            finally
            {
                db.Dispose();
            }
        }

        public void DeleteById(long? id)
        {
            using (var db = new ApplicationDbContext())
            {
                var product = new Product { Id = id.Value };

                //db.Entry<Product>(product).State = System.Data.Entity.EntityState.Deleted;      // jeddan chera :(

                var temp = db.Products.Find(id.Value);
                if (temp != null)
                {
                    db.Products.Remove(temp);
                    //db.Entry(temp).CurrentValues.SetValues( ... < isDeleted = true > ... );
                    db.SaveChanges();
                }

            }   
        }
    }
}
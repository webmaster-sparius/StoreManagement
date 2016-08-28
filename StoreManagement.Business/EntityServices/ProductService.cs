using StoreManagement.Common.EntityServices;
using StoreManagement.Common.Models;
using StoreManagement.Framework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                products =  db.Products.ToList();
            return products;
        }

            #endregion

        public IEnumerable<ProductViewModel> FetchViewModels()
        {
            using (var db = new ApplicationDbContext()) {

                var list = db.Products.Include(a=>a.Category).Where(p =>! p.IsDeleted)
                    .Select(p => new ProductViewModel
                    { Code = p.Code, Name = p.Name, Price = p.Price, Category = p.Category.Title, Id = p.Id, Description= p.Description})
                            .ToList();
                return list;
            }
        }
    }
}
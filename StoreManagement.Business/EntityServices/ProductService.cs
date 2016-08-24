using StoreManagement.Common.EntityServices;
using StoreManagement.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        #endregion
    }
}
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
    public class ProductService : EntityService<Product>, IProductService
    {
        #region IProductService

        public bool CheckNameExist(string name, long? id)
        {
            var db = Repository.Current;
            var products = db.Set<Product>();
            var exist = id.HasValue ?
            products.Any(p => p.Id != id && p.Name == name) :
            products.Any(p => p.Name == name);
            return exist;
        }

        public bool CheckCodeExist(string code, long? id)
        {
            var db = Repository.Current;
            var products = db.Set<Product>();
            var exist = id.HasValue ?
                products.Any(p => p.Id != id && p.Code == code) :
                products.Any(p => p.Code == code);
            return exist;
        }

        #endregion

        public void DeleteById(long? id)
        {
            var db = Repository.Current;
            var product = new Product { Id = id.Value };

            //db.Entry<Product>(product).State = System.Data.Entity.EntityState.Deleted;      // jeddan chera :(

            var temp = db.Set<Product>().Find(id.Value);
            if (temp != null)
            {
                db.Set<Product>().Remove(temp);
                //db.Entry(temp).CurrentValues.SetValues( ... < isDeleted = true > ... );
                db.SaveChanges();
            }

        }
    }
}
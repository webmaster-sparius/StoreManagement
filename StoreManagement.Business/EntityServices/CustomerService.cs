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
    public class CustomerService : EntityService<Customer>, ICustomerService
    {
        public bool CheckCustomerExist(string firstName, string lastName, long? Id)
        {
            using (var db = new Repository())
            {
                var customers = db.Set<Customer>();

                var exist = Id.HasValue ?
                    customers.Any(a => a.Id != Id && a.LastName == lastName && a.FirstName == firstName) :
                    customers.Any(a => a.LastName == lastName && a.FirstName == firstName);
                return exist;
            }
        }

        public IEnumerable<Customer> FetchAll()
        {
            List<Customer> customers = null;
            using (var db = new Repository())
            {
                customers = db.Set<Customer>().ToList();
            }
            return customers;
        }

        public void DeleteById(long? id)
        {
            using (var db = new Repository())
            {
                var customer = new Customer { Id = id.Value };
                var temp = db.Set<Customer>().Find(id);
                if (temp != null)
                {
                    db.Set<Customer>().Remove(temp);
                    db.SaveChanges();
                }

            }
        }



    }
}
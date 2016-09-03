using StoreManagement.Common.EntityServices;
using StoreManagement.Common.Models;
using StoreManagement.Web.Areas.BasicData.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreManagement.Business.EntityServices
{
    public class CustomerService : ICustomerService
    {
        public bool CheckCustomerExist(string firstName, string lastName, long? Id)
        {
            using (var db = new ApplicationDbContext())
            {
                var customers = db.Customers;

                var exist = Id.HasValue ?
                    customers.Any(a => a.Id != Id && a.LastName == lastName && a.FirstName == firstName) :
                    customers.Any(a => a.LastName == lastName && a.FirstName == firstName);
                return exist;
            }
        }

        public IEnumerable<Customer> FetchAll()
        {
            List<Customer> customers = null;
            using (var db = new ApplicationDbContext())
            {
                customers = db.Customers.ToList();
            }
            return customers;
        }

        public EditCustomerViewModel FetchEditViewModel(long? id)
        {
            EditCustomerViewModel viewModel;
            using (var db = new ApplicationDbContext())
            {
                viewModel = db.Customers.Select(
                    a => new EditCustomerViewModel
                    {
                        Id = a.Id,
                        FirstName = a.FirstName,
                        LastName = a.LastName,
                        PhoneNumber = a.PhoneNumber,
                        Version = a.Version
                    }).FirstOrDefault(a => a.Id == id);
            }
            return viewModel;
        }

        public void EditByViewModel(EditCustomerViewModel viewModel)
        {
            var db = new ApplicationDbContext();
            try
            {
                var customer = new Customer
                {
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    PhoneNumber = viewModel.PhoneNumber,
                    Id = viewModel.Id,
                    Version = viewModel.Version
                };
                db.Entry<Customer>(customer).State = System.Data.Entity.EntityState.Modified;
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
                var customer = new Customer { Id = id.Value };
                var temp = db.Customers.Find(id);
                if (temp != null)
                {
                    db.Customers.Remove(temp);
                    db.SaveChanges();
                }

            }
        }

        public void CreateByViewModel(AddCustomerViewModel viewModel)
        {
            using (var db = new ApplicationDbContext())
            {
                var customer = new Customer { FirstName = viewModel.FirstName, LastName = viewModel.LastName, PhoneNumber = viewModel.PhoneNumber };
                db.Customers.Add(customer);
                db.SaveChanges();
            }
        }

    }
}
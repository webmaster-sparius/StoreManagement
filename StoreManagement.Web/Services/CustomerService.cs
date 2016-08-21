﻿using StoreManagement.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreManagement.Web.Services
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
    }
}
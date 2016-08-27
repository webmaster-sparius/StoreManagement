using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoreManagement.Common.Models;

namespace StoreManagement.Common.EntityServices
{
    public interface ICustomerService
    {
        bool CheckCustomerExist(string firstName, string lastName, long? id);
        IEnumerable<Customer> FetchAll();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreManagement.Web.Services
{
    public interface ICustomerService
    {
        bool CheckCustomerExist(string firstName, string lastName, long? id);
    }
}
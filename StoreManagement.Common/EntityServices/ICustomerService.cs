using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreManagement.Common.EntityServices
{
    public interface ICustomerService
    {
        bool CheckCustomerExist(string firstName, string lastName, long? id);
    }
}
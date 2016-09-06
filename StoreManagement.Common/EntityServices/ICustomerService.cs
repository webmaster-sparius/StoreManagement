using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoreManagement.Common.Models;
using StoreManagement.Framework.Common;

namespace StoreManagement.Common.EntityServices
{
    public interface ICustomerService : IEntityService<Customer>
    {
        bool CheckCustomerExist(string firstName, string lastName, long? id);
    }
}
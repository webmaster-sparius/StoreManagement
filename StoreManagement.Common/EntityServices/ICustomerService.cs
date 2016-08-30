﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoreManagement.Common.Models;
using StoreManagement.Framework.Common;
using StoreManagement.Web.Areas.BasicData.ViewModels;

namespace StoreManagement.Common.EntityServices
{
    public interface ICustomerService : IEntityService<Customer>
    {
        bool CheckCustomerExist(string firstName, string lastName, long? id);
        IEnumerable<Customer> FetchAll();
        EditCustomerViewModel FetchEditViewModel(long? id);
        void EditByViewModel(EditCustomerViewModel viewModel);
        void DeleteById(long? id);
    }
}
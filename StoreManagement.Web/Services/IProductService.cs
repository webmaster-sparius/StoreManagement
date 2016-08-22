﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreManagement.Web.Services
{
    public interface IProductService
    {
        bool CheckNameExist(string name, long? id);
        bool CheckCodeExist(string code, long? id);
    }
}
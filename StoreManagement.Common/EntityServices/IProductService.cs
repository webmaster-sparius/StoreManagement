using StoreManagement.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoreManagement.Web.Areas.BasicData.ViewModels;

namespace StoreManagement.Common.EntityServices
{
    public interface IProductService
    {
        bool CheckNameExist(string name, long? id);
        bool CheckCodeExist(string code, long? id);
        IEnumerable<Product> FetchAll();
        IEnumerable<ProductViewModel> FetchViewModels();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreManagement.Web.ViewModels.Product
{
    public class ProductViewModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        
    }
}
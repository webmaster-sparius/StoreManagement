using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace StoreManagement.Web.Areas.BasicData.ViewModels
{
    public class InvoiceItemViewModel
    {
        
        public decimal Quantity { get; set; }

        
        public decimal Price { get; set; }

        public decimal FinalPrice { get; set; }

        public string ProductName { get; set; }
    }
}
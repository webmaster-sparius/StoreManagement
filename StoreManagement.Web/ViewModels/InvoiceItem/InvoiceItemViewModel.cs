using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace StoreManagement.Web.ViewModels.InvoiceItem
{
    public class InvoiceItemViewModel
    {
        [Required]
        public decimal Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoreManagement.Web.Models
{
    public class InvoiceItem
    {
        #region Properties
        public long Id { get; set; }
        [Required]
        public decimal Quantity { get; set; }
        [Required]
        public decimal Price { get; set; }

        #endregion

        #region NavigationProperties
        public Product Product { get; set; }
        public long ProductId { get; set; }
        public Invoice Invoice { get; set; }
        public long InvoiceId { get; set; }
        #endregion

    }
}
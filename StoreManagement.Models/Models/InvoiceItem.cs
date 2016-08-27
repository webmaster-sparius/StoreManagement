using StoreManagement.Framework.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoreManagement.Common.Models
{
    public class InvoiceItem: BaseEntity
    {
        #region Properties
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
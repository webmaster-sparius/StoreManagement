using StoreManagement.Framework.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoreManagement.Common.Models
{
    public class Invoice: BaseEntity
    {
        #region Properties
        [Required]
        [MaxLength(50)]
        public string Number { get; set; }
        public DateTime CreatedOn { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }
        #endregion

        #region NavigationProperties
        public Customer Customer { get; set; }
        public long CustomerId { get; set; }
        public ICollection<InvoiceItem> Items { get; set; }
        #endregion
    }
}
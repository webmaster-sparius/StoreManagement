using StoreManagement.Framework.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoreManagement.Common.Models
{
    public class Customer: BaseEntity
    {
        #region Properties
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }
        [Timestamp]
        public byte[] Version { get; set; }
        #endregion

        #region NavigationProperties
        public ICollection<Invoice> Invoices { get; set; }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace StoreManagement.Web.Areas.BasicData.ViewModels.Customer
{
    public class EditCustomerViewModel
    {
        public long Id { get; set; }

        [DisplayName("نام")]
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [DisplayName("نام خانوادگی")]
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [DisplayName("تلفن")]
        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }
    }
}
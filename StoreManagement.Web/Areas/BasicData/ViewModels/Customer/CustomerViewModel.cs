using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoreManagement.Web.Areas.BasicData.ViewModels.Customer
{
    public class CustomerViewModel
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
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace StoreManagement.Web.Areas.BasicData.ViewModels
{
    public class AddCustomerViewModel
    {
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
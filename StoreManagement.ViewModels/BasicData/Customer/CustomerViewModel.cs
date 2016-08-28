using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoreManagement.Web.Areas.BasicData.ViewModels
{
    [DisplayName("مشتری")]
    public class CustomerViewModel
    {
        public long Id { get; set; }

        [DisplayName("نام")]
        public string FirstName { get; set; }

        [DisplayName("نام خانوادگی")]
        public string LastName { get; set; }

        [DisplayName("تلفن")]
        public string PhoneNumber { get; set; }
    }
}
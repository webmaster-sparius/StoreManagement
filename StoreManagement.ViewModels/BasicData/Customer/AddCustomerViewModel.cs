using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;


namespace StoreManagement.Web.Areas.BasicData.ViewModels
{
    public class AddCustomerViewModel
    {
        [DisplayName("نام")]
        [Required(ErrorMessage ="نام مشتری را وارد کنید.")]
        [MaxLength(50,ErrorMessage ="نام مشتری نباید بیشتر از 50 کارکتر باشد.")]
        public string FirstName { get; set; }

        [DisplayName("نام خانوادگی")]
        [Required(ErrorMessage = "نام خانوادگی مشتری را وارد کنید.")]
        [MaxLength(50, ErrorMessage = "نام خانوادگی مشتری نباید بیشتر از 50 کارکتر باشد.")]
        [Remote("CustomerExist", "Customer",AdditionalFields ="FirstName", ErrorMessage ="مشتری با این نام و نام خانوادگی قبلا در سیستم ثبت شده است.",HttpMethod ="POST")]
        public string LastName { get; set; }

        [DisplayName("تلفن")]
        [Required(ErrorMessage = "شماره تلفن مشتری را وارد کنید.")]
        [MaxLength(20, ErrorMessage = "شماره تلفن مشتری نباید بیشتر از 20 کارکتر باشد.")]
        public string PhoneNumber { get; set; }
    }
}
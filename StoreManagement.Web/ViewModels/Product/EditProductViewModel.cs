using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace StoreManagement.Web.ViewModels.Product
{
    public class EditProductViewModel
    {
        [Required(ErrorMessage = "نام کالا را وارد کنید.")]
        [StringLength(50, ErrorMessage = "نام کالا نباید بیشتر از 100 کاراکتر باشد.")]
        [Remote("UniqueCategoryTitle", ErrorMessage = "یک کالا با این نام قبلا در سیستم ثبت شده است.", HttpMethod = "POST")]
        public string Name { get; set; }
        [Required(ErrorMessage = "قیمت کالا را وارد کنید.")]
        public decimal Price { get; set; }
    }
}
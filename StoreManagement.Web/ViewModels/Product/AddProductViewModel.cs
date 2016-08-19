using StoreManagement.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreManagement.Web.ViewModels.Product
{
    public class AddProductViewModel
    {
        [Required(ErrorMessage = "نام کالا را وارد کنید.")]
        [StringLength(100, ErrorMessage = "نام کالا نباید بیشتر از 100 کاراکتر باشد.")]
        [Remote("UniqueCategoryTitle", ErrorMessage = "یک کالا با این نام قبلا در سیستم ثبت شده است.", HttpMethod = "POST")]
        public string Name { get; set; }
        [Required(ErrorMessage = "کد کالا را وارد کنید.")]
        [StringLength(50, ErrorMessage = "تعداد کاراکترهای کد کالا نباید بیشتر از 50کاراکتر باشد.")]
        [Remote("UniqueCategoryTitle", ErrorMessage = "یک کالا با این کد قبلا در سیستم ثبت شده است.", HttpMethod = "POST")]
        public string Code { get; set; }
        [Required(ErrorMessage = "قیمت کالا را وارد کنید.")]
        public decimal Price { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage ="گروه کالا را انتخاب کنید.")]
        public long CategoryId { get; set; }

        public DbSet<Models.Category> Categories = new ApplicationDbContext().Categories;
        }
}
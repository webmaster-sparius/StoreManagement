using StoreManagement.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace StoreManagement.Web.Areas.BasicData.ViewModels
{
    public class EditProductViewModel
    {
        [Required]
        public long Id { get; set; }

        [Required(ErrorMessage = "نام کالا را وارد کنید.")]
        [StringLength(50, ErrorMessage = "نام کالا نباید بیشتر از 100 کاراکتر باشد.")]
        [Remote("TitleExist", "Category", AdditionalFields ="Id",ErrorMessage = "یک کالا با این نام قبلا در سیستم ثبت شده است.", HttpMethod = "POST")]
        [DisplayName("نام")]
        public string Name { get; set; }

        [Required(ErrorMessage = "قیمت کالا را وارد کنید.")]
        [DisplayName("قیمت")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "کد کالا را وارد کنید.")]
        [StringLength(50, ErrorMessage = "تعداد کاراکترهای کد کالا نباید بیشتر از 50کاراکتر باشد.")]
        [Remote("TitleExist", "Category", AdditionalFields ="Id" ,ErrorMessage = "یک کالا با این کد قبلا در سیستم ثبت شده است.", HttpMethod = "POST")]
        [DisplayName("کد")]
        public string Code { get; set; }

        [DisplayName("توضیحات")]
        public string Description { get; set; }

        [Required]
        public byte[] Version { get; set; }

        [Required(ErrorMessage = "گروه کالا را انتخاب کنید.")]
        [DisplayName("گروه")]
        public long CategoryId { get; set; }

        public IQueryable<Category> Categories
        {
            get
            {
                return new ApplicationDbContext().Categories;
            }
        }
    }
}
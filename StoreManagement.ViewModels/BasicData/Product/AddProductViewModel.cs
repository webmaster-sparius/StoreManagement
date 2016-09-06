using StoreManagement.Common.EntityServices;
using StoreManagement.Common.Models;
using StoreManagement.Framework.Common;
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
    public class AddProductViewModel
    {
        [Required(ErrorMessage = "نام کالا را وارد کنید.")]
        [StringLength(100, ErrorMessage = "نام کالا نباید بیشتر از 100 کاراکتر باشد.")]
        [Remote("NameExist", "Product", ErrorMessage = " کالا با این نام قبلا در سیستم ثبت شده است.", HttpMethod = "POST")]
        [DisplayName("نام")]
        public string Name { get; set; }
        [Required(ErrorMessage = "کد کالا را وارد کنید.")]
        [StringLength(50, ErrorMessage = "تعداد کاراکترهای کد کالا نباید بیشتر از 50کاراکتر باشد.")]
        [Remote("CodeExist", "Product", ErrorMessage = "کالا با این کد قبلا در سیستم ثبت شده است.", HttpMethod = "POST")]
        [DisplayName("کد")]
        public string Code { get; set; }
        [Required(ErrorMessage = "قیمت کالا را وارد کنید.")]
        [DataType(DataType.Currency, ErrorMessage = "عدد وارد کنید.")]
        [DisplayName("قیمت")]
        public decimal Price { get; set; }
        [DisplayName("توضیحات")]
        public string Description { get; set; }

        [DisplayName("گروه")]
        [Required(ErrorMessage = "گروه کالا را انتخاب کنید.")]
        public long CategoryId { get; set; }

        public IList<Category> Categories
        {
            get
            {
                return ServiceFactory.Create<ICategoryService>().FetchAll();
            }
        }
    }
}
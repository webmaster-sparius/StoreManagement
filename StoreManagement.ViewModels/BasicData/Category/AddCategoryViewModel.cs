﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreManagement.Web.Areas.BasicData.ViewModels
{
    public class AddCategoryViewModel
    {
        [Required(ErrorMessage ="عنوان را وارد کنید.")]
        [StringLength(50,ErrorMessage ="عنوان نباید بیشتر از 50 کاراکتر باشد.")]
        [Remote("TitleExist", "Category", ErrorMessage = "یک گروه با این عنوان قبلا در سیستم ثبت شده است.",HttpMethod ="POST")]
        [DisplayName("عنوان")]
        public string Title { get; set; }
    }
}
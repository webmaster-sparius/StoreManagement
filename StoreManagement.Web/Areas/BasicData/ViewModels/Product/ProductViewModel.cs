using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace StoreManagement.Web.Areas.BasicData.ViewModels
{
    [DisplayName("کالا")]
    public class ProductViewModel
    {
        public long Id { get; set; }

        [DisplayName("نام کالا")]
        public string Name { get; set; }

        [DisplayName("کد کالا")]
        public string Code { get; set; }

        [DisplayName("قیمت")]
        public decimal Price { get; set; }

        [DisplayName("توضیحات")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DisplayName("گروه کالا")]
        public string Category { get; set; }
    }
}
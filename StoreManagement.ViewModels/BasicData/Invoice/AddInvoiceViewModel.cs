using StoreManagement.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Threading;
using StoreManagement.Framework.Common;
using StoreManagement.Common.EntityServices;
using System.Web.Mvc;

namespace StoreManagement.Web.Areas.BasicData.ViewModels
{
    public class AddInvoiceViewModel
    {
        [DisplayName("شماره فاکتور")]
        [Required(ErrorMessage ="شماره فاکتور را وارد کنید.")]
        [MaxLength(50)]
        [Remote("ExistNumber","Invoice",ErrorMessage ="فاکتور با این شماره قبلا در سیستم ثبت شده است.",HttpMethod ="POST")]
        public string Number { get; set; }

        [DisplayName("مشتری")]
        [Required(ErrorMessage = "  مشتری را انتخاب کنید.")]
        public long CustomerId { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedOn { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "تاریخ را وارد کنید.")]
        [DisplayName("تاریخ")]
        public string CreatedOnString { get; set; }

        [DisplayName("قیمت کل")]
        public decimal Price { get; set; }

     
        public ICollection<InvoiceItem> Items { get; set; }

        public IEnumerable<Customer> Customers
        {
            get
            {
                return ServiceFactory.Create<ICustomerService>().FetchAll();
            }
        }

        public IEnumerable<Product> Products
        {
            get
            {
                return ServiceFactory.Create<IProductService>().FetchAll();
            }
        }
    }
}
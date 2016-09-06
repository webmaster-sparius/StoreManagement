using StoreManagement.Common.Models;
using StoreManagement.Framework.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace StoreManagement.Web.Areas.BasicData.ViewModels
{
    public class EditInvoiceViewModel
    {
        [Required]
        public long Id { get; set; }

        [DisplayName("شماره")]
        [Required(ErrorMessage = "شماره فاکتور را وارد کنید.")]
        [Remote("ExistNumber","Invoice",AdditionalFields="Id" ,ErrorMessage ="فاکتور با این شماره قبلا در سیستم ثبت شده است.", HttpMethod ="POST")]
        public string Number { get; set; }

        [DisplayName("مشتری")]
        [Required(ErrorMessage = "مشتری را وارد کنید")]
        public long CustomerId { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedOn { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("تاریخ")]
        [Required(ErrorMessage = "تاریخ را وارد کنید")]
        public string CreatedOnString { get; set; }

        public byte[] Version { get; set; }

        public ICollection<EditInvoiceItemViewModel> Items { get; set; }

        public IQueryable<Customer> Customers
        {
            get
            {
                return Repository.Current.Set<Customer>();
            }
        }

        public IQueryable<Product> Products
        {
            get
            {
                return Repository.Current.Set<Product>();
            }
        }
    }
}


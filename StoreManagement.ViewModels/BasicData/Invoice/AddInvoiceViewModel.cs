using StoreManagement.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StoreManagement.Web.Areas.BasicData.ViewModels
{
    public class AddInvoiceViewModel
    {
        [DisplayName("شماره فاکتور")]
        [Required]
        [MaxLength(50)]
        public string Number { get; set; }

        [DisplayName("مشتری")]
        //[Required(ErrorMessage = "  مشتری را انتخاب کنید.")]
        public long CustomerId { get; set; }

        [DisplayName("تاریخ")]
        public DateTime CreatedOn { get; set; }

        [DisplayName("قیمت کل")]
        public decimal Price { get; set; }

     
        public ICollection<InvoiceItem> Items { get; set; }

        public IQueryable<Customer> Customers
        {
            get
            {
                return new ApplicationDbContext().Customers;
            }
        }

        public IQueryable<Product> Products
        {
            get
            {
                return new ApplicationDbContext().Products;
            }
        }
    }
}
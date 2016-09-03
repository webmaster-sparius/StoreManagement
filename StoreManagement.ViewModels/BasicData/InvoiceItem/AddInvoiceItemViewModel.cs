using StoreManagement.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Web.Areas.BasicData.ViewModels
{
    public class AddInvoiceItemViewModel
    {
        [DisplayName("کالا")]
        [Required(ErrorMessage = " کالا را انتخاب کنید.")]
        public long ProductId { get; set; }

        [DisplayName("تعداد")]
        [Required(ErrorMessage ="تعداد کالا را وارد کنید")]
        public decimal Quantity { get; set; }

        [DisplayName("قیمت")]
        public long Price { get; set; }

        public long InvoiceId { get; set; }
        
        
    }
}

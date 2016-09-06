using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Web.Areas.BasicData.ViewModels
{
    public class EditInvoiceItemViewModel
    {
        public long Id { get; set; }
        public decimal Quantity { get; set; }

        public decimal Price { get; set; }

        public long ProductId { get; set; }

        public long InvoiceId { get; set; }
    }
}

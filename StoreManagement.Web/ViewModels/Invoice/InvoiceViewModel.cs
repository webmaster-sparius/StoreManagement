using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreManagement.Web.ViewModels.Invoice
{
    public class InvoiceViewModel
    {
        public string Number { get; set; }

        public byte[] Version { get; set; }

        public Customer.CustomerViewModel Customer { get; set; }   // needs check

        ICollection<InvoiceItem.InvoiceItemViewModel> Items { get; set; }
    }
}
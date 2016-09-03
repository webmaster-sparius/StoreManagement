using StoreManagement.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoreManagement.Web.Areas.BasicData.ViewModels;

namespace StoreManagement.Common.EntityServices
{
    public interface IInvoiceService
    {
        IEnumerable<Invoice> FetchAll();
        void Create(List<string> inputs, List<InvoiceItem> items);
        IEnumerable<InvoiceViewModel> FetchViewModels();
    }
}

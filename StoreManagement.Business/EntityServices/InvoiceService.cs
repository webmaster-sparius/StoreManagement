using StoreManagement.Common.EntityServices;
using StoreManagement.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Business.EntityServices
{
     public  class InvoiceService : IInvoiceService 
    {
        #region
        public IEnumerable<Invoice> FetchAll()
        {
            List<Invoice> Invoices;
            using (var db = new ApplicationDbContext())
                Invoices = db.Invoices.ToList();
            return Invoices;
        }
        #endregion
    }
}
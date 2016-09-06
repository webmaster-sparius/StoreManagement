
using StoreManagement.Common.EntityServices;
using StoreManagement.Common.Models;
using StoreManagement.Framework.Common;
using StoreManagement.Web.Areas.BasicData.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;

namespace StoreManagement.Business.EntityServices
{
    public class InvoiceService : EntityService<Invoice>, IInvoiceService
    {

        #region IInvoiceService
        public bool CheckNumberExist(string number, long? id)
        {
            var db = Repository.Current;
            var invoices = db.Set<Invoice>();
            var exist = id.HasValue ?
                invoices.Any(i => i.Id != id && i.Number == number) :
                invoices.Any(i => i.Number == number);
            return exist;
        }

        public void SaveInvoice(Invoice invoice)
        {
            var db = Repository.Current;
            db.Set<Invoice>().Add(invoice);
            db.SaveChanges();
        }

        public void UpdateInvoice(Invoice invoice)
        {
            var db = Repository.Current;
                db.Entry(invoice).State = System.Data.Entity.EntityState.Modified;
            foreach (var item in invoice.Items)
                db.Entry(item).State = item.Id == 0 ?
                    System.Data.Entity.EntityState.Added :
                    System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        #endregion
    }
}
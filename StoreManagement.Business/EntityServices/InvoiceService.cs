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

        public void Create(List<string> inputs, List<InvoiceItem> items)
        {
            using (var db = new ApplicationDbContext())
            {
                var invoice = new Invoice
                {
                    Number = inputs[0],
                    CustomerId = Int32.Parse(inputs[1]),
                    CreatedOn = DateTime.Parse(inputs[2]),
                    Customer = db.Customers.Find(Int32.Parse(inputs[1]))
                };
                foreach (var item in items)
                {
                    var invoiceItem = new InvoiceItem
                    {
                        ProductId = item.ProductId,
                        Price = item.Price,
                        Quantity = item.Quantity,
                        Product = db.Products.Find(item.ProductId),
                        Invoice = invoice
                    };
                    db.InvoiceItems.Add(invoiceItem);
                    invoice.Items.Add(invoiceItem);
                }
                db.Invoices.Add(invoice);
                db.SaveChanges();
            }
        }
        #endregion
    }
}
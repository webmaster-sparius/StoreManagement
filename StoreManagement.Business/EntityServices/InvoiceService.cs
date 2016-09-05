using StoreManagement.Common.EntityServices;
using StoreManagement.Common.Models;
using StoreManagement.Web.Areas.BasicData.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Business.EntityServices
{
    public class InvoiceService : IInvoiceService
    {
        #region IInvoiceService
        public IEnumerable<Invoice> FetchAll()
        {
            List<Invoice> Invoices;
            using (var db = new ApplicationDbContext())
            {
                Invoices = db.Invoices.ToList();
                List<InvoiceItem> InvoiceItems = db.InvoiceItems.ToList();
                foreach (var invoice in Invoices)
                {
                    invoice.Customer = db.Customers.Find(invoice.CustomerId);
                }
            }
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
                    CreatedOn = PersianDateTime.Parse(inputs[2]).ToDateTime(),
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

        public IEnumerable<InvoiceViewModel> FetchViewModels()
        {
            List<InvoiceViewModel> invoices;
            using (var db = new ApplicationDbContext())
            {
                List<Invoice> list = db.Invoices.ToList();
                var items = db.InvoiceItems.ToList();
                foreach (var invoice in list)
                {
                    foreach (var item in items)
                    {
                        if (item.InvoiceId == invoice.Id)
                        {
                            invoice.Items.Add(item);

                        }
                    }
                    invoice.Customer = db.Customers.Find(invoice.CustomerId);
                }
                invoices = list.Select(invoice => new InvoiceViewModel
                {
                    Id = invoice.Id,
                    Customer = invoice.Customer.FirstName + " " + invoice.Customer.LastName,
                    Number = invoice.Number,
                    Items = invoice.Items.Select(i => new InvoiceItemViewModel
                    {
                        ProductName = db.Products.Find(i.ProductId).Name, 
                        Price = i.Price,
                        Quantity = i.Quantity,
                        FinalPrice = i.Quantity * i.Price
                    }).ToList(),
                    CreatedOn = invoice.CreatedOn,
                    FinalPrice = invoice.Items.Sum(item => (item.Quantity * item.Price))
                }).ToList();
            }
            foreach (var invoice in invoices)
                invoice.CreatedOnString = new PersianDateTime(invoice.CreatedOn).ToString(PersianDateTimeFormat.Date);
            return invoices;
        }

        public void DeleteById(long id)
        {
            ////////////////////////
            using (var db = new ApplicationDbContext())
            {
                var product = new Invoice { Id = id };

                //db.Entry<Product>(product).State = System.Data.Entity.EntityState.Deleted;      // jeddan chera :(

                var temp = db.Invoices.Find(id);
                if (temp != null)
                {
                    db.Invoices.Remove(temp);
                    //db.Entry(temp).CurrentValues.SetValues( ... < isDeleted = true > ... );
                    db.SaveChanges();
                }
            }
            /////////////////////////
        }
        #endregion
    }
}
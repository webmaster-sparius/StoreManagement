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

namespace StoreManagement.Business.EntityServices
{
    public class InvoiceService : EntityService<Invoice>, IInvoiceService
    {
        #region IInvoiceService
        public void Create(List<string> inputs, List<InvoiceItem> items)
        {
            var db = Repository.Current;
            var invoice = new Invoice
            {
                Number = inputs[0],
                CustomerId = Int32.Parse(inputs[1]),
                CreatedOn = DateTime.Parse(inputs[2]),
                Customer = db.Set<Customer>().Find(Int32.Parse(inputs[1]))
            };
            foreach (var item in items)
            {
                var invoiceItem = new InvoiceItem
                {
                    ProductId = item.ProductId,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    Product = db.Set<Product>().Find(item.ProductId),
                    Invoice = invoice
                };
                db.Set<InvoiceItem>().Add(invoiceItem);
                invoice.Items.Add(invoiceItem);
            }
            db.Set<Invoice>().Add(invoice);
            db.SaveChanges();
        }

        public IList<InvoiceViewModel> FetchViewModels()
        {
            return FetchAllAndProject(i =>
                new InvoiceViewModel
                {
                    Id = i.Id,
                    Number = i.Number,
                    Customer = i.Customer.FirstName + " " + i.Customer.LastName,
                    Items = i.Items.Select(ii => new InvoiceItemViewModel
                    {
                        ProductName = ii.Product.Name,
                        Price = ii.Price,
                        Quantity = ii.Quantity,
                        FinalPrice = ii.Quantity * ii.Price
                    }).ToList(),
                    CreatedOn = i.CreatedOn,
                    FinalPrice = i.Items.Sum(ii => ii.Quantity * ii.Price)
                });
        }

        #endregion
    }
}
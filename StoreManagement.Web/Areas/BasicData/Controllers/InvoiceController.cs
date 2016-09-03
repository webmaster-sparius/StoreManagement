using StoreManagement.Common.EntityServices;
using StoreManagement.Common.Models;
using StoreManagement.Framework.Common;
using StoreManagement.Web.Areas.BasicData.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreManagement.Web.Areas.BasicData.Controllers
{
    public class InvoiceController : Controller
    {
        #region
        public virtual ActionResult List()
        {
            var list = ServiceFactory.Create<IInvoiceService>().FetchAll();
            ViewBag.Type = typeof(Invoice);
            ViewBag.List = list;

            return View("EntityList");

        }
        #endregion

        #region
        [HttpGet]
        public ActionResult Create()
        {
            AddInvoiceViewModel viewModel = new AddInvoiceViewModel();
            viewModel.CreatedOn = DateTime.Now;
            return View(viewModel);
        }
        
        [HttpPost]
        public JsonResult Create(List<string> inputs, List<InvoiceItem> items)
        {
            if (inputs != null & items != null)
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
                return Json(new { msg = "success" });
            }
            else
            {
                return Json(new { msg = "fail" });
            }
        }
        #endregion
    }
}
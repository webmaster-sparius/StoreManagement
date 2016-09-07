using StoreManagement.Common.EntityServices;
using StoreManagement.Common.Models;
using StoreManagement.Framework.Common;
using StoreManagement.Web.Areas.BasicData.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace StoreManagement.Web.Areas.BasicData.Controllers
{
    public class InvoiceController : Controller
    {
        #region List
        public virtual ActionResult List()
        {
            var list = ServiceFactory.Create<IInvoiceService>()
                .FetchAllAndProject(GetInvoiceToInvoiceViewModelExpression()).ToList();

            foreach (var invoice in list)
                invoice.CreatedOnString = new PersianDateTime(invoice.CreatedOn).ToString(PersianDateTimeFormat.Date);


            ViewBag.Type = typeof(Invoice);
            ViewBag.List = list;

            return View("EntityList");

        }
        #endregion

        #region Create
        [HttpGet]
        public ActionResult Create()
        {
            AddInvoiceViewModel viewModel = new AddInvoiceViewModel();
            // viewModel.CreatedOn = DateTime.Now;
            return View(viewModel);
        }

        [HttpPost]

        public void Create(AddInvoiceViewModel viewModel)
        {
            var invoice = new Invoice
            {
                Number = viewModel.Number,
                CustomerId = viewModel.CustomerId,
                CreatedOn = PersianDateTime.Parse(viewModel.CreatedOnString).ToDateTime(),
                Items = new List<InvoiceItem>()
            };
            foreach (var item in viewModel.Items)
            {
                var invoiceItem = new InvoiceItem
                {
                    ProductId = item.ProductId,
                    Price = item.Price,
                    Quantity = item.Quantity
                };
                invoice.Items.Add(invoiceItem);
            }
            ServiceFactory.Create<IInvoiceService>().SaveInvoice(invoice);
        }
        #endregion

        #region Details
        public ActionResult Details(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var viewModel = ServiceFactory.Create<IInvoiceService>()
                .FetchByIdAndProject(id.Value, GetInvoiceToInvoiceViewModelExpression());
            viewModel.CreatedOnString = new PersianDateTime(viewModel.CreatedOn).ToString(PersianDateTimeFormat.Date);
            if (viewModel == null)
                return HttpNotFound();
            return View(viewModel);
        }
        #endregion

        #region Edit
        [HttpGet]
        public ActionResult Edit(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            EditInvoiceViewModel viewModel = ServiceFactory.Create<IInvoiceService>().
                FetchByIdAndProject(id.Value, GetInvoiceToEditInvoiceViewModelExpression());
            viewModel.CreatedOnString = new PersianDateTime(viewModel.CreatedOn).ToString(PersianDateTimeFormat.Date);

            if (viewModel == null)
                return HttpNotFound();
            return View(viewModel);
        }

        [HttpPost]
        public JsonResult Edit(EditInvoiceViewModel viewModel)
        {
            var invoice = new Invoice
            {
                Id = viewModel.Id,
                Number = viewModel.Number,
                CustomerId = viewModel.CustomerId,
                CreatedOn = PersianDateTime.Parse(viewModel.CreatedOnString).ToDateTime(),
                Version = viewModel.Version
            };
            invoice.Items = new List<InvoiceItem>();
            foreach (var item in viewModel.Items)
            {
                var invoiceItem = new InvoiceItem
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    InvoiceId = item.InvoiceId
                };
                invoice.Items.Add(invoiceItem);
            }
            try
            {
                ServiceFactory.Create<IInvoiceService>().UpdateInvoice(invoice);
                return Json(new { success = true });
            }
            catch (DbUpdateConcurrencyException)
            {
               // ModelState.AddModelError("", "این فاکتور قبلا در سیستم توسط فرد دیگری ویرایش شده است.صفحه را رفرش کنید.");
                return Json(new { success = false});
            }
        }
        #endregion

        #region Delete
        public ActionResult Delete(long id)
        {
            ServiceFactory.Create<IInvoiceService>().DeleteById(id);
            return RedirectToAction("List");
        }
        #endregion

        #region RemoteValidation
        [HttpPost]
        public JsonResult ExistNumber(string number, long? id)
        {
            var exist = ServiceFactory.Create<IInvoiceService>().CheckNumberExist(number, id);
            return Json(!exist);
        }
        #endregion

        public Expression<Func<Invoice, InvoiceViewModel>> GetInvoiceToInvoiceViewModelExpression()
        {
            return i => new InvoiceViewModel
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
            };
        }
        public Expression<Func<Invoice, EditInvoiceViewModel>> GetInvoiceToEditInvoiceViewModelExpression()
        {
            return invoice => new EditInvoiceViewModel
            {
                Id = invoice.Id,
                Version = invoice.Version,
                CreatedOn = invoice.CreatedOn,
                CustomerId = invoice.CustomerId,
                Number = invoice.Number,
                Items = invoice.Items
                    .Select(item => new EditInvoiceItemViewModel
                    {
                        Id = item.Id,
                        ProductId = item.ProductId,
                        Price = item.Price,
                        Quantity = item.Quantity,
                        InvoiceId = invoice.Id
                    }).ToList()
            };
        }

    }
}
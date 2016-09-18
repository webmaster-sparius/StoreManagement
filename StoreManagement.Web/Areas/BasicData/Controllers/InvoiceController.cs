using StoreManagement.Common.EntityServices;
using StoreManagement.Common.Models;
using StoreManagement.Framework.Common;
using StoreManagement.ViewModels;
using StoreManagement.Web.Areas.BasicData.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
                .FetchAllAndProject(i => new InvoiceViewModel
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
                }).ToList();
            //var list = ServiceFactory.Create<IInvoiceService>().FetchViewModels();

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
        
        public void  Create(List<string> inputs, List<InvoiceItem> items)
        {
            if (inputs != null && items != null)
            {
                ServiceFactory.Create<IInvoiceService>().Create(inputs, items);
            }
        }
        #endregion

        #region Details
        public ActionResult Details(long? id)
        {
            if(id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var viewModel = ServiceFactory.Create<IInvoiceService>()
                .FetchByIdAndProject(id.Value, i => new InvoiceViewModel
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
            if (viewModel == null)
                return HttpNotFound();
            return View(viewModel);
        }
        #endregion

        #region Edit
        public ActionResult Edit(long? id)
        {
            return View("Impossible");
        }
        #endregion

        #region Impossible
        public ActionResult Impossible(long? id)
        {
            return View("Impossible");
        }
        #endregion

        #region Delete
        public ActionResult Delete(long id)
        {
            ServiceFactory.Create<IInvoiceService>().DeleteById(id);
            return RedirectToAction("List");
        }
        #endregion

        #region Search
        public ActionResult Search(InvoiceViewModel viewModel)
        {
            var dic = ControllerHelper.QueryStringToDictionary(Request.Url.Query);
            var list = ServiceFactory.Create<IInvoiceService>().SearchByFilter(dic).Select(i => new InvoiceViewModel
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
            ViewBag.Type = typeof(Invoice);
            ViewBag.List = list;

            ViewBag.RowsAffected = list.Count();
            return View("EntityList");
        }
        #endregion
    }
}
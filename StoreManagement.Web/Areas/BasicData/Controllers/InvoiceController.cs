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
        #region List
        public virtual ActionResult List()
        {
            var list = ServiceFactory.Create<IInvoiceService>().FetchAll()
                .Select(invoice => new InvoiceViewModel
                {
                    Id = invoice.Id,
                    Number = invoice.Number,
                    Customer = invoice.Customer.FirstName + " " + invoice.Customer.LastName,
                }).ToList();
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
            viewModel.CreatedOn = DateTime.Now;
            return View(viewModel);
        }
        
        [HttpPost]
        public JsonResult Create(List<string> inputs, List<InvoiceItem> items)
        {
            if (inputs != null & items != null)
            {
                ServiceFactory.Create<IInvoiceService>().Create(inputs, items);
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
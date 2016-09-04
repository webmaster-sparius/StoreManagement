using StoreManagement.Common.EntityServices;
using StoreManagement.Common.Models;
using StoreManagement.Framework.Common;
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
            var list = ServiceFactory.Create<IInvoiceService>().FetchViewModels();

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
        
        public ActionResult Create(List<string> inputs, List<InvoiceItem> items)
        {
            if (inputs != null & items != null)
            {
                ServiceFactory.Create<IInvoiceService>().Create(inputs, items);
            }
            return RedirectToAction("List");
        }
        #endregion

        #region Details
        public ActionResult Details(long? id)
        {
            if(id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var viewModel = ServiceFactory.Create<IInvoiceService>().FetchViewModels()
                .FirstOrDefault(i => i.Id == id);
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
    }
}
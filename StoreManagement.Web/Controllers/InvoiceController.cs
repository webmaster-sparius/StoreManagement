using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreManagement.Web.Controllers
{
    public class InvoiceController : Controller
    {
        // GET: Invoice
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View("Add");
        }

        public PartialViewResult get_item_group(string name)
        {
            return PartialView("_ItemGroup");
        }
    }
}
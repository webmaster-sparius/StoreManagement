using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreManagement.Web.Controllers
{
    public partial class InvoiceController : Controller
    {
        // GET: Invoice
        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult Add()
        {
            return View("Add");
        }

        public virtual PartialViewResult get_item_group(string name)
        {
            return PartialView("_ItemGroup");
        }
    }
}
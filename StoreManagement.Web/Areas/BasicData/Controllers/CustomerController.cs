using StoreManagement.Common.EntityServices;
using StoreManagement.Common.Models;
using StoreManagement.Framework.Common;
using StoreManagement.Web.Areas.BasicData.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace StoreManagement.Web.Areas.BasicData.Controllers
{
    [RouteArea("BasicData")]
    public partial class CustomerController : Controller
    {
        #region Fields
        /// <summary>
        /// todo : use dependecy injection
        /// </summary>
        private readonly ICustomerService _customerService;
        #endregion

        #region Constructor
        public CustomerController(ICustomerService customerSevice)
        {
            _customerService = customerSevice;
        }

        public CustomerController()
        {
        }
        #endregion

        #region List
        public virtual ActionResult List()
        {
            /*
            using (var db = new ApplicationDbContext())
            {
                var customers = db.Customers.Select(customer => new CustomerViewModel { Id = customer.Id, FirstName = customer.FirstName, LastName = customer.LastName, PhoneNumber = customer.PhoneNumber })
                    .ToList();
                ViewBag.List = customers;
                ViewBag.Type = typeof(Customer);
                return View("EntityList");
            }
            */
            var customers = ServiceFactory.Create<ICustomerService>().FetchAll()
                .Select(customer => new CustomerViewModel {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    PhoneNumber = customer.PhoneNumber
                }).ToList();
            ViewBag.List = customers;
            ViewBag.Type = typeof(Customer);
            return View("EntityList");

        }
        #endregion

        #region Create
        [HttpGet]
        public virtual ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(AddCustomerViewModel viewModel)
        {
            /// to do : add checking when CustomerService implement
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "تمام فیلد ها باید وارد شوند.");
                return View(viewModel);
            }

            ServiceFactory.Create<ICustomerService>().CreateByViewModel(viewModel);

            return RedirectToAction("List");
        }
        #endregion

        #region Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(EditCustomerViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "تمام فیلد ها باید وارد شوند.");
                return View(viewModel);
            }
            try
            {
                ServiceFactory.Create<ICustomerService>().EditByViewModel(viewModel);
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError("", "اطلاعات کاریر مورد نظر توسط کاربر دیگری در شبکه، تغییر یافته است. برای ادامه صفحه را رفرش کنید.");
                return View(viewModel);
            }
            return RedirectToAction("List");
        }

        [HttpGet]
        public virtual ActionResult Edit(long id)
        {
            EditCustomerViewModel viewModel = ServiceFactory.Create<ICustomerService>().FetchEditViewModel(id);
                if (viewModel == null)
                    return HttpNotFound();
                return View(viewModel);
        }
        #endregion

        #region Details
        public virtual ActionResult Details(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var viewModel = ServiceFactory.Create<ICustomerService>().FetchAll()
                .Select(v => new CustomerViewModel {
                    Id = v.Id,
                    FirstName = v.FirstName,
                    LastName = v.LastName,
                    PhoneNumber = v.PhoneNumber
                }).FirstOrDefault(v => v.Id == id);
            if (viewModel == null)
                return HttpNotFound();
            return View(viewModel);
        }
        #endregion

        #region Delete

        public virtual ActionResult Delete(long id)         
        {
            ServiceFactory.Create<ICustomerService>().DeleteById(id);
            return RedirectToAction("List");

        }
        #endregion

        #region RemoteValidation
        [HttpPost]
        public virtual JsonResult CustomerExist(string lastName, string firstName, long? Id)
        {
            var exist = ServiceFactory.Create<ICustomerService>().CheckCustomerExist(firstName, lastName, Id);
            return Json(!exist);
        }
        #endregion
    }
}
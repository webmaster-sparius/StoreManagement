using StoreManagement.Common.EntityServices;
using StoreManagement.Common.Models;
using StoreManagement.Framework.Common;
using StoreManagement.Web.Areas.BasicData.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
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
            using (var db = new ApplicationDbContext())
            {
                var customers = db.Customers.Select(customer => new CustomerViewModel { Id = customer.Id, FirstName = customer.FirstName, LastName = customer.LastName, PhoneNumber = customer.PhoneNumber })
                    .ToList();
                return View(customers);
            }

        }
        #endregion

        #region Create
        [HttpGet]
        public virtual ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public virtual ActionResult Create(AddCustomerViewModel viewModel)
        {
            if (ServiceFactory.Create<ICustomerService>().CheckCustomerExist(viewModel.FirstName, viewModel.LastName, null))
                ModelState.AddModelError("", "یک کاربر با این نام و نام خانوادگی ثبت شده است");
            /// to do : add checking when CustomerService implement
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "تمام فیلد ها باید وارد شوند.");
                return View(viewModel);
            }
            using (var db = new ApplicationDbContext())
            {
                var customer = new Customer { FirstName = viewModel.FirstName, LastName = viewModel.LastName, PhoneNumber = viewModel.PhoneNumber };
                db.Customers.Add(customer);
                db.SaveChanges();
            }
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
            var db = new ApplicationDbContext();
            try
            {
                var customer = new Customer
                {
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    PhoneNumber = viewModel.PhoneNumber,
                    Id = viewModel.Id,
                    Version = viewModel.Version
                };
                db.Entry<Customer>(customer).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = viewModel.Id });
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError("", "اطلاعات کاریر مورد نظر توسط کاربر دیگری در شبکه، تغییر یافته است. برای ادامه صفحه را رفرش کنید.");
                return View(viewModel);
            }
            finally
            {
                db.Dispose();
            }
        }
        [HttpGet]
        public virtual ActionResult Edit(long id)
        {
            using (var db = new ApplicationDbContext())
            {
                var viewModel = db.Customers.Select(
                    a => new EditCustomerViewModel
                    {
                        Id = a.Id,
                        FirstName = a.FirstName,
                        LastName = a.LastName,
                        PhoneNumber = a.PhoneNumber,
                        Version = a.Version
                    }).FirstOrDefault(a => a.Id == id);
                if (viewModel == null)
                    return HttpNotFound();
                return View(viewModel);
            }
        }
        #endregion

        #region RemoteValidation
        [HttpPost]
        public virtual JsonResult CustomerExist(string firstName, string lastName, long? Id)
        {
            var exist = ServiceFactory.Create<ICustomerService>().CheckCustomerExist(firstName, lastName, Id);
            return Json(!exist);
        }
        #endregion
    }
}
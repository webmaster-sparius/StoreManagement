using StoreManagement.Web.Models;
using StoreManagement.Web.Services;
using StoreManagement.Web.ViewModels.Customer;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreManagement.Web.Controllers
{
    [RoutePrefix("Customer")]
    [Route("{action}")]
    public class CustomerController : Controller
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
        public ActionResult List()
        {
            using (var db = new ApplicationDbContext())
            {
                var customers = db.Customers.Select(customer => new CustomerViewModel {FirstName = customer.FirstName, LastName = customer.LastName, PhoneNumber = customer.PhoneNumber  })
                    .ToList();
                return View(customers);
            }

        }
        #endregion

        #region Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(AddCustomerViewModel viewModel)
        {
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
        public ActionResult Edit(EditCustomerViewModel viewModel)
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
                ModelState.AddModelError("", "گروه مورد نظر توسط کاربر دیگری در شبکه، تغییر یافته است. برای ادامه صفحه را رفرش کنید.");
                return View(viewModel);
            }
            finally
            {
                db.Dispose();
            }
        }
        #endregion
        
    }
}
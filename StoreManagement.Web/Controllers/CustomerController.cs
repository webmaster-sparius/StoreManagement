using StoreManagement.Web.Models;
using StoreManagement.Web.Services;
using System;
using System.Collections.Generic;
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
                var customers = db.Customers.Select(customer => new CustomerViewModel { })
                    .ToList();
            }

            return View(customers);
        }
        #endregion

        #region Create
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}
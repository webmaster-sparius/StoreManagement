using StoreManagement.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StoreManagement.Web.Controllers
{
    public partial class HomeController : Controller
    {

        public virtual ActionResult Index()
        {
            List<Category> categories;
            using (var db = new ApplicationDbContext())
            {
                categories = db.Categories.ToList();
            }

            ViewData["categories"] = categories;
            return View();
        }



        // loading home page with search already done

        public virtual ActionResult Search(string text)
        {
            List<Category> categories;
            using (var db = new ApplicationDbContext())
            {
                categories = db.Categories.ToList();
            }

            ViewData["categories"] = categories;
            if (string.IsNullOrEmpty(text))
            {
                ViewData["searched"] = "";
            }
            else {
                ViewData["searched"] = text;
            }
            return View();
        }


        public virtual ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public virtual ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public virtual PartialViewResult SearchFor(string title)
        {

            IQueryable<Product> products;

            using (var db = new ApplicationDbContext())
            {
                products = db.Products.Where(p => p.Name == title);

                ViewData["search_result"] = products.ToList();
            }

            return PartialView("_ResultPView");
        }




    }
}
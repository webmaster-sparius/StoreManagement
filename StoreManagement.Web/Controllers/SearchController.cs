using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreManagement.Web.Models;


namespace StoreManagement.Web.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        public string returnRes(string title)
        {
            string res="";

            // a query using EF returns a strongly typed result
            // we have to turn that into an html tag

            // using default EF model for Product

            using (var db = new ApplicationDbContext())
            {
                var products = db.Products.Where(p => p.Name == title);
            }


            return res;
        }

    }
}
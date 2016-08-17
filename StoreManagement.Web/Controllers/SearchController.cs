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

            IQueryable<Product> products;

            using (var db = new ApplicationDbContext())
            {
                products = db.Products.Where(p => p.Name == title);


                // now turn into a tag

                res += "<p>";
                res += "start of results";
                res += "</p>";
                res += " ";

                foreach (var elem in products)
                {
                    res += "<p>";
                    res += elem.Name;
                    res += "</p>";
                    res += " ";
                }

                res += "<p>";
                res += "end of results";
                res += "</p>";
                res += " ";
            }

            //          return res;
            return res;
        }

    }
}
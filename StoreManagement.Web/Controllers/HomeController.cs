﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreManagement.Web.Models;

namespace StoreManagement.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public string SearchFor(string title)
        {
            string res = "";

            // a query using EF returns a strongly typed result
            // we have to turn that into an html tag

            // using default EF model for Product

            IQueryable<Product> products;

            using (var db = new ApplicationDbContext())
            {
                products = db.Products.Where(p => p.Name == title);


                // now turn into a tag

                res += "<p>";
                res += "begin results for "+title;
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

                res += "<hr /> ";
            }

            //          return res;
            return res;
        }

        // loading home page with search already done

        public ActionResult SearchRes(string param)
        {
            ViewData["searched"] = param;
            return View("Search");
        }

    }
}
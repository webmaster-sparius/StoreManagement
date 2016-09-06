using StoreManagement.Common.Models;
using StoreManagement.Framework.Common;
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
            /*
            List<Category> categories;
            using (var db = new ApplicationDbContext())
            {
                categories = db.Categories.ToList();
            }

            ViewData["categories"] = categories;
            return View();
            */

            Response.Redirect("/basicdata/invoice/create");
            return null;
        }



        // loading home page with search already done

        public virtual ActionResult Search(string text)
        {
            List<Category> categories;
            var db = Repository.Current;
            categories = db.Set<Category>().ToList();

            ViewData["categories"] = categories;
            if (string.IsNullOrEmpty(text))
            {
                ViewData["searched"] = "";
            }
            else
            {
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

            var db = Repository.Current;
                products = db.Set<Product>().Where(p => p.Name == title);

                ViewData["search_result"] = products.ToList();
        
            return PartialView("_ResultPView");
        }


        public virtual PartialViewResult GetEditedDiv()
        {
            return PartialView("_EditedDiv");
        }


    }
}
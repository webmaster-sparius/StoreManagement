using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;
using StoreManagement.Common.Models;
using System.Configuration;
using StoreManagement.Framework.App;
using System.IO;
using StoreManagement.Framework.Common;

namespace StoreManagement.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AppLoader.InitializeApp(Server.MapPath("."));

            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<Repository>());

            using (var db = new Repository())
            {
                db.Database.Initialize(force: true);
            }

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }


    }
}

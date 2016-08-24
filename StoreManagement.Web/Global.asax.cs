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

namespace StoreManagement.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ApplicationDbContext>());

            using (var db = new ApplicationDbContext())
            {
                db.Database.Initialize(force: true);
            }

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var binPath = ConfigurationManager.AppSettings["BinPath"];
            if (!Path.IsPathRooted(binPath))
                binPath = Path.Combine(Server.MapPath("."), binPath);
            AppLoader.LoadBinAssemblies(binPath);
        }
    }
}

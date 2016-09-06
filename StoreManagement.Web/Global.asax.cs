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
using System.Linq.Expressions;

namespace StoreManagement.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AppLoader.InitializeApp(Server.MapPath("."));

            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<Repository>());

            using (var db = Repository.Create())
                db.Database.Initialize(force: true);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

        }
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            RequestRepositoryProvider.CreateRequestRepository();
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            RequestRepositoryProvider.GetRequestRepository().Dispose();
        }
    }

    internal class RequestRepositoryProvider : IRequestRepositoryProvider
    {
        private static string _requestRepositoryKey = "HttpContextRequestLevelRepository";

        public Repository Repository
        {
            get
            {
                return GetRequestRepository();
            }
        }

        internal static void CreateRequestRepository()
        {
            HttpContext.Current.Items[_requestRepositoryKey] = Repository.Create();
        }

        internal static Repository GetRequestRepository()
        {
            return (Repository)HttpContext.Current.Items[_requestRepositoryKey];
        }
    }
}

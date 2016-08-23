using System.Web.Mvc;

namespace StoreManagement.Web.Areas.BasicData
{
    public class BasicDataAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "BasicData";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "BasicData_default",
                "BasicData/{controller}/{action}/{id}",
                defaults: new { id = UrlParameter.Optional },
                namespaces: new[] { this.GetType().Namespace + ".Controllers" }
            );
        }
    }
}
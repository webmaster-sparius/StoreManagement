using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StoreManagement.Web.Startup))]
namespace StoreManagement.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}

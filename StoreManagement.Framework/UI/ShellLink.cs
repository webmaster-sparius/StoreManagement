using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Framework.UI
{
    public class ShellLink
    {
        public ShellLink(string title, string controllerName, string actionName)
        {
            this.Title = title;
            this.ControllerName = controllerName;
            this.ActionName = actionName;
        }

        public string Title { get; set; }
        public string AreaName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
    }
}

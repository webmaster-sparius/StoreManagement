using StoreManagement.Framework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoreManagement.Framework.UI;
using StoreManagement.Web.Models;

namespace StoreManagement.Web.Areas.BasicData
{
    public class BasicDataComponentInfo : ComponentInfo
    {
        public override string AreaName
        {
            get
            {
                return "BasicData";
            }
        }

        public override IEnumerable<ShellLink> GetComponentShellLinks()
        {
            return new[]
            {
                new ShellLink("گروه", "Category", "List"),
                new ShellLink("کالا", "Product", "List"),
                new ShellLink("مشتری", "Customer", "List"),
            };
        }
    }
}
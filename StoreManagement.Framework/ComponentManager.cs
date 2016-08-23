using StoreManagement.Framework.Common;
using StoreManagement.Framework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Framework
{
    public class ComponentManager
    {
        public static IEnumerable<ShellLink> GetAllLinks()
        {
            var links = new List<ShellLink>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var asm in assemblies)
            {
                foreach (var t in asm.GetTypes().
                    Where(t => t.IsSubclassOf(typeof(ComponentInfo))))
                {
                    var c = (ComponentInfo)Activator.CreateInstance(t);
                    foreach (var link in c.GetComponentShellLinks())
                    {
                        link.AreaName = c.AreaName;
                        links.Add(link);
                    }
                }
            }

            return links;
        }
    }
}

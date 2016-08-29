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
        private static IEnumerable<ComponentInfo> allComponentInfo;
        static ComponentManager()
        {
            var componentInfos = new List<ComponentInfo>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var asm in assemblies)
            {
                foreach (var t in asm.GetTypes().
                    Where(t => t.IsSubclassOf(typeof(ComponentInfo))))
                {
                    var c = (ComponentInfo)Activator.CreateInstance(t);
                    componentInfos.Add(c);
                }
            }
            allComponentInfo = componentInfos;
        }
        public static IEnumerable<ComponentInfo> GetAllComponentInfos()
        {
            return allComponentInfo;
        }

        public static IEnumerable<ShellLink> GetAllLinks()
        {
            var links = new List<ShellLink>();

            foreach (var ci in GetAllComponentInfos())
            {
                foreach (var link in ci.GetComponentShellLinks())
                {
                    link.AreaName = ci.AreaName;
                    links.Add(link);
                }
            }

            return links;
        }

        public static IEnumerable<EntityInfo> GetAllEntityInfos()
        {
            var entityInfos = new List<EntityInfo>();
            foreach (var ci in GetAllComponentInfos())
                entityInfos.AddRange(ci.GetEntityInfos());
            return entityInfos;
        }
    }
}

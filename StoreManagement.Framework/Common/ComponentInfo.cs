using StoreManagement.Framework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Framework.Common
{
    public abstract class ComponentInfo
    {
        public abstract string AreaName { get; }
        public abstract IEnumerable<ShellLink> GetComponentShellLinks();
        public abstract IEnumerable<EntityInfo> GetEntityInfos();
    }
}

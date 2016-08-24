using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Framework.Common
{
    public class EntityInfo
    {
        public EntityInfo(Type entityType, Type defaultViewModelType, Type serviceType)
        {
            this.EntityType = entityType;
            this.DefaultViewModelType = defaultViewModelType;
            this.ServiceType = serviceType;
        }

        public Type EntityType { get; private set; }
        public Type DefaultViewModelType { get; private set; }
        public Type ServiceType { get; private set; }
    }
}

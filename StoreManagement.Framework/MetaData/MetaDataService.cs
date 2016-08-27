using StoreManagement.Framework.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Framework.MetaData
{
    public class MetaDataService
    {
        private static Dictionary<Type, EntityInfo> _entityInfos = new Dictionary<Type, EntityInfo>();

        public static void AddEntityInfo(EntityInfo ei)
        {
            _entityInfos[ei.EntityType] = ei;
        }

        public static EntityInfo GetEntityInfo(Type entityType)
        {
            if (!_entityInfos.ContainsKey(entityType))
                throw new Exception("EntityInfo for entity '" + entityType.Name + "' not found.");
            return _entityInfos[entityType];
        }

        public static List<Tuple<string, string>> GetTypeDisplayProperties(Type viewModelType)
        {
            List<Tuple<string, string>> list = new List<Tuple<string, string>>();
            foreach (var prop in viewModelType.GetProperties())
            {
                var attribs = prop.GetCustomAttributes(typeof(DisplayNameAttribute), false);
                if (attribs.Length == 0)
                    continue;
                list.Add(Tuple.Create(prop.Name, ((DisplayNameAttribute)attribs[0]).DisplayName));
            }

            return list;
        }
    }
}

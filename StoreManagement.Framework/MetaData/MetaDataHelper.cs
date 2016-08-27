using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Framework.MetaData
{
    public class MetaDataHelper
    {
        public static string GetDisplayName(Type type)
        {
            var attribs = type.GetCustomAttributes(typeof(DisplayNameAttribute), false);
            if (attribs.Length == 0)
                return null;
            return ((DisplayNameAttribute)attribs[0]).DisplayName;
        }
    }
}

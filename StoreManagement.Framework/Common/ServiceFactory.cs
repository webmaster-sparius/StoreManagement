using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Framework.Common
{
    public static class ServiceFactory
    {
        public static T Create<T>()
        {
            var interfaceType = typeof(T);
            if (!interfaceType.IsInterface)
                throw new ArgumentException(string.Format("'{0} is not an interface.", interfaceType.Name));

            foreach(var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach(var type in asm.GetTypes())
                {
                    if(!type.IsInterface &&
                        !type.IsAbstract &&
                        interfaceType.IsAssignableFrom(type))
                    {
                        return (T)Activator.CreateInstance(type);
                    }
                }
            }
            throw new Exception("Implementation for '" + interfaceType.Name + "' not found.");
        }
    }
}

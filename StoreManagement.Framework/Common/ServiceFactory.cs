using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.StartsWith("StoreManagement."));
            //var assemblyNames = assemblies.Select(a => a.GetName().Name).OrderBy(n => n).ToList();
            //Debug.WriteLine(string.Join("\r\n", assemblyNames));
            //Debug.WriteLine("Searching assemblies for implementation of '" + interfaceType.ToString() + "'");
            foreach (var asm in assemblies)
            {
                //Debug.WriteLine($"Assembly: {asm.GetName().Name}, Path: '{asm.CodeBase}'");
                foreach(var type in asm.GetTypes())
                {
                    //Debug.WriteLine($"  Type: {type.FullName}");
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




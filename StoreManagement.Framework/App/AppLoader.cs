using StoreManagement.Framework.MetaData;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Framework.App
{
    public class AppLoader
    {
        public static void InitializeApp(string appRootPath)
        {
            var binPath = ConfigurationManager.AppSettings["BinPath"];
            if (!Path.IsPathRooted(binPath))
                binPath = Path.Combine(appRootPath, binPath);
            LoadBinAssemblies(binPath);
        }
        private static void LoadBinAssemblies(string path)
        {
            var loadedAssemblies = new HashSet<string>(
                AppDomain.CurrentDomain.GetAssemblies().
                Select(a => a.GetName().Name));
            foreach(var asmFileName in Directory.GetFiles(path, "StoreManagement.*.dll"))
            {
                var nameOnly = Path.GetFileNameWithoutExtension(asmFileName);
                if (!loadedAssemblies.Contains(nameOnly))
                    Assembly.LoadFrom(asmFileName);
            }
            foreach (var ei in ComponentManager.GetAllEntityInfos())
                MetaDataService.AddEntityInfo(ei);
        }
    }
}

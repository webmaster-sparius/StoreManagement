using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace StoreManagement.Framework.Common
{
    public class Repository : DbContext
    {
        private Repository() : base("StoreManagementEF")
        {
        }

        public static Repository Create()
        {
            return new Repository();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var entityMethod = typeof(DbModelBuilder).GetMethod("Entity");

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var entityTypes = assembly.GetTypes()
                    .Where(t => t.IsSubclassOf(typeof(BaseEntity)));
                foreach (var type in entityTypes)
                {
                    entityMethod.MakeGenericMethod(type)
                        .Invoke(modelBuilder, new object[] { });
                }
            }
        }

        public static Repository Current
        {
            get
            {
                return ServiceFactory.Create<IRequestRepositoryProvider>().Repository;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Framework.Common
{
    public class EntityService<TEntity> : IEntityService<TEntity>
        where TEntity : BaseEntity
    {

        //public virtual IEnumerable<TEntity> SearchByFilter(TViewModel viewModel)
        //{
        //    Dictionary<string, string> query = new Dictionary<string, string>();
        //    var properties = viewModel.GetType().GetProperties();
        //    using (var db = new ApplicationDbContext())
        //    {
        //        foreach (var prop in properties)
        //        {
        //            prop.PropertyType.IsValueType
        //            if (prop.GetValue(viewModel) != null || !(prop.Name == "Id" && (long)prop.GetValue(viewModel) == 0))
        //            {
        //                query.Add(prop.Name, prop.GetValue(viewModel).ToString());
        //            }
        //        }
        //        var list = db.Categories.Where();
        //    }
        //    return new[] { viewModel };

        //}
        public TEntity FetchById(long id)
        {
            using (var repository = new Repository())
            {
                return repository.Set<TEntity>().
                    Where(e => e.Id == id).
                    FirstOrDefault();
            }
        }

        public TResult FetchByIdAndProject<TResult>(long id, Expression<Func<TEntity, TResult>> projection)
        {
            using (var repository = new Repository())
            {
                return repository.Set<TEntity>().
                    Where(e => e.Id == id).
                    Select(projection).
                    FirstOrDefault();
            }
        }
        public IEnumerable<TResult> FetchAllAndProject<TResult>(Expression<Func<TEntity, TResult>> projection)
        {
            using (var repository = new Repository())
            {
                return repository.Set<TEntity>().
                    Select(projection).
                    ToList();
            }
        }

        public IEnumerable<TEntity> FetchAll()
        {
            using (var db = new Repository()) 
            {
                return db.Set<TEntity>().ToList();
            }
        }

        public void Save(TEntity entity)
        {
            using (var db = new Repository())
            {
                db.Entry<TEntity>(entity).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Create(TEntity entity)
        {
            using (var db = new Repository())
            {
                db.Set<TEntity>().Add(entity);
                db.SaveChanges();
            }
        }
    }
}


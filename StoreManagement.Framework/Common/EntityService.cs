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
            var db = Repository.Current;
            return db.Set<TEntity>().
                Where(e => e.Id == id).
                FirstOrDefault();
        }

        public TResult FetchByIdAndProject<TResult>(long id, Expression<Func<TEntity, TResult>> projection)
        {
            var db = Repository.Current;
            return db.Set<TEntity>().
                Where(e => e.Id == id).
                Select(projection).
                FirstOrDefault();
        }
        public IList<TResult> FetchAllAndProject<TResult>(Expression<Func<TEntity, TResult>> projection)
        {
            var db = Repository.Current;
            return db.Set<TEntity>().
                Select(projection).
                ToList();
        }

        public IList<TEntity> FetchAll()
        {
            var db = Repository.Current;
            return db.Set<TEntity>().ToList();
        }

        public void Save(TEntity entity)
        {
            var db = Repository.Current;
            db.Entry<TEntity>(entity).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
        }

        public void Create(TEntity entity)
        {
            var db = Repository.Current;
            db.Set<TEntity>().Add(entity);
            db.SaveChanges();
        }

        public void DeleteById(long id)
        {
            var db = Repository.Current;
            var entity = db.Set<TEntity>().Find(id);
            if (entity == null)
                throw new InvalidOperationException("ID does not found.");
            db.Set<TEntity>().Remove(entity);
            db.SaveChanges();
        }
    }
}


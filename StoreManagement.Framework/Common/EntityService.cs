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
        private static Expression IndexOfPredicateGenerator(Expression pe, string value)
        {
            Expression left = Expression.Call(pe, typeof(string).GetMethod("IndexOf", new Type[] { typeof(string) }), Expression.Constant(value));
            Expression right = Expression.Constant(0);
            Expression predicate = Expression.GreaterThanOrEqual(left, right);
            return predicate;
        }

        private static Expression CreateFieldSearchExpression(Expression expression, string fieldName, string value)
        {
            return IndexOfPredicateGenerator(
                Expression.Call(
                    Expression.Property(expression, fieldName),
                    typeof(object).GetMethod("ToString")),
                value);
        }

        public IQueryable<TEntity> SearchByFilter(Dictionary<string, string> searchPredicates)
        {
            ParameterExpression param = Expression.Parameter (typeof(TEntity), "e");
            Expression ex = Expression.Constant(true);
            foreach(var keyVal in searchPredicates)
            {
                if (string.IsNullOrWhiteSpace(keyVal.Value))
                    continue;
                ex = Expression.And(ex, CreateFieldSearchExpression(param, keyVal.Key, keyVal.Value));
            }
            return FetchAll().Where(Expression.Lambda<Func<TEntity, bool>>(ex, param));
        }


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

        public IQueryable<TResult> FetchAllAndProject<TResult>(Expression<Func<TEntity, TResult>> projection)
        {
            var db = Repository.Current;
            return db.Set<TEntity>().
                Select(projection);
        }

        public IQueryable<TEntity> FetchAll()
        {
            var db = Repository.Current;
            return db.Set<TEntity>();
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


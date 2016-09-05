using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Framework.Common
{
    public interface IEntityService<TEntity>
        where TEntity : BaseEntity
    {
        TEntity FetchById(long id);
        TResult FetchByIdAndProject<TResult>(long id, Expression<Func<TEntity, TResult>> projection);
        IEnumerable<TEntity> FetchAll();
        void Save(TEntity entity);
        void Create(TEntity entity);
        IEnumerable<TResult> FetchAllAndProject<TResult>(Expression<Func<TEntity, TResult>> projection);
    }
}

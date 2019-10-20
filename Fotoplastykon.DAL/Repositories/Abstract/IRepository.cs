using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Fotoplastykon.DAL.Entities.Abstract;
using Fotoplastykon.Tools.Pager;

namespace Fotoplastykon.DAL.Repositories.Abstract
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        Task<TEntity> Get(long id);
        Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetAll();
        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);
        Task<IPaginationResult<TEntity>> GetPaginatedList(IPager pager);

        Task Add(TEntity entity);
        Task AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        Task Remove(long id);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}

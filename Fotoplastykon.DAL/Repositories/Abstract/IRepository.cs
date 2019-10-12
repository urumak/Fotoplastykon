using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Fotoplastykon.DAL.Entities.Abstract;
using Fotoplastykon.Tools.Pager;

namespace Fotoplastykon.DAL.Repositories.Abstract
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        TEntity Get(long id);
        TEntity Get(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        IPaginationResult<TEntity> GetPaginatedList(IPager pager);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void Remove(long id);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}

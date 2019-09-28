using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Fotoplastykon.DAL.Entities.Abstract;

namespace Fotoplastykon.DAL.Repositories.Abstract
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        TEntity Get(long id);
        TEntity Get(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void Remove(long id);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Fotoplastykon.DAL.Entities.Abstract;
using Fotoplastykon.Tools.Pager;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Fotoplastykon.DAL.Repositories.Abstract
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        protected DbContext Context { get; }

        protected Repository(DbContext context)
        {
            Context = context;
        }

        public virtual TEntity Get(long id)
        {
            return Context.Set<TEntity>().FirstOrDefault(x => x.Id == id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate).ToList();
        }

        public virtual void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public virtual void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public virtual void Remove(long id)
        {
            var entity = Context.Set<TEntity>().FirstOrDefault(x => x.Id == id);
            Context.Set<TEntity>().Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }

        public virtual TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().FirstOrDefault(predicate);
        }

        public virtual IPaginationResult<TEntity> GetPaginatedList(IPager pager)
        {
            return Context.Set<TEntity>().GetPaginationResult<TEntity>(pager);
        }
    }
}

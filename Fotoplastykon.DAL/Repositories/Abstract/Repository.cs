using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
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

        public virtual async Task<TEntity> Get(long id)
        {
            return await Context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public virtual async Task Add(TEntity entity)
        {
            if(entity is IAuditable auditableEntity)
            {
                auditableEntity.DateCreated = DateTime.Now;
                await Context.Set<IAuditable>().AddAsync(auditableEntity);
            }
            else
            {
                await Context.Set<TEntity>().AddAsync(entity);
            }
        }

        public virtual async Task AddRange(IEnumerable<TEntity> entities)
        {
            await Context.Set<TEntity>().AddRangeAsync(entities);
        }

        public virtual void Remove(TEntity entity)
        {
            if(entity is IRecoverable recoverableEntity)
            {
                recoverableEntity.DateDeleted = DateTime.Now;
            }
            else
            {
                Context.Set<TEntity>().Remove(entity);
            }
        }

        public virtual async Task Remove(long id)
        {
            var entity = await Get(id);
            Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }

        public virtual async Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<IPaginationResult<TEntity>> GetPaginatedList(IPager pager)
        {
            return await Context.Set<TEntity>().GetPaginationResult(pager);
        }

        public virtual async Task<IPaginationResult<TEntity>> GetPaginatedList(IPager pager, Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().Where(predicate).GetPaginationResult(pager);
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Fotoplastykon.DAL.Entities.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Fotoplastykon.DAL.Repositories.Abstract
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly DbContext Context;

        protected Repository(DbContext context)
        {
            Context = context;
        }

        public TEntity Get(long id)
        {
            return Context.Set<TEntity>().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate).ToList();
        }

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void Remove(long id)
        {
            var entity = Context.Set<TEntity>().FirstOrDefault(x => x.Id == id);
            Context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().FirstOrDefault(predicate);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Banking.Data.Infrastructure;
using Banking.Model;

namespace Banking.Data.Repositories
{
    public class EntityRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        protected DbSet<TEntity> DbSet { get; set; }
        protected DbContext DbContext { get; set; }

        public EntityRepository(DbContext context)
        {
            DbContext = context;
            DbSet = DbContext.Set<TEntity>();
        }

        public EntityRepository(IDbFactory factory) : this(factory.GetContext())
        {
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }

        public virtual IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return includeProperties.Aggregate(DbSet, (Func<IQueryable<TEntity>, Expression<Func<TEntity, object>>, IQueryable<TEntity>>)((current, includeProperty) => current.Include(includeProperty)));
        }

        public virtual void Update(TEntity entity)
        {
            DbSet.AddOrUpdate(entity);
        }

        public virtual TEntity Add(TEntity entity)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
                dbEntityEntry.State = EntityState.Added;
            else
                DbSet.Add(entity);
            return entity;
        }

        public virtual IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            return DbSet.AddRange(entities);
        }

        public virtual async Task<TEntity> FindAsync(params object[] keyValues)
        {
            return await FindAsync(CancellationToken.None, keyValues);
        }

        public virtual async Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return await DbSet.FindAsync(cancellationToken, keyValues);
        }

        public virtual TEntity Remove(TEntity entity)
        {
            return DbSet.Remove(entity);
        }

        public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }
    }
}

using System;
using System.Collections.Generic;
using CompanyManager.DatabaseAccessLayer.Context;
using CompanyManager.DatabaseAccessLayer.Repositories.Interfaces;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Linq;

namespace CompanyManager.DatabaseAccessLayer.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private CompanyDatabaseContext context;
        private DbSet<TEntity> dbSet;

        public GenericRepository(CompanyDatabaseContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public TEntity GetById(object id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<TEntity> GetAll(
            Expression<Func<TEntity,bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy=null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;
            if(filter!=null)
            {
                query = query.Where(filter);
            }
            foreach(var includeProperty in includeProperties.Split
                (new char[]{','},StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if(orderBy!=null)
            {
                query = orderBy(query);
            }

            return query;
        }

        public void Add(TEntity entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();
        }

        public void Remove(TEntity entity)
        {
            if(context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
            context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);

            }
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
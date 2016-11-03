using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Web;

namespace test.Models
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal DbContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(DbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, List<Expression<Func<TEntity, object>>> includeProperties = null, int? page = null, int? pageSize = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (includeProperties != null)
                includeProperties.ForEach(i => { query = query.Include(i); });

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            if (page != null && pageSize != null)
                query = query
                    .Skip((page.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);

            return query;
        }

        public IQueryable<TEntity> Get(string predicate, params object[] values)
        {
            return dbSet.Where(predicate, values);
        }

        public void Attach(TEntity entity)
        {
            dbSet.Attach(entity);
            context.Configuration.ValidateOnSaveEnabled = false;
        }

        public TEntity GetById(object id)
        {
            return this.dbSet.Find(id);
        }

        public void Create(TEntity entity)
        {
            this.dbSet.Add(entity);
        }

        public void CreateWithViewModel<TViewModel>(TViewModel viewModel, TEntity distination)
        {
            Mapper.Map<TViewModel, TEntity>(viewModel, distination);
            this.dbSet.Add(distination);
        }

        public void Delete(object id)
        {
            var entity = this.dbSet.Find(id);
            if (entity != null)
                this.context.Entry(entity).State = EntityState.Deleted;
        }

        public void Delete(TEntity entity)
        {
            this.dbSet.Attach(entity);
            this.dbSet.Remove(entity);
        }

        public void UpdateAll(TEntity entity)
        {
            this.dbSet.Attach(entity);
            this.context.Entry(entity).State = EntityState.Modified;
        }

        public void UpdateWithViewModel<TViewModel>(TViewModel viewModel, TEntity distination)
        {
            this.dbSet.Attach(distination);
            Mapper.Map<TViewModel, TEntity>(viewModel, distination);
        }

        public void UpdateAllWithViewModel<TViewModel>(TViewModel viewModel, TEntity distination)
        {
            this.UpdateAll(distination);
            Mapper.Map<TViewModel, TEntity>(viewModel, distination);
        }

        public IQueryable<TEntity> SqlQuery(string query, params object[] parameters)
        {
            return dbSet.SqlQuery(query, parameters).AsQueryable();
        }

        public IQueryable<TEntity> CompareWith(string columnName, string key, int flag)
        {
            Func<TEntity, bool> compare;

            switch (flag)
            {
                case -1:
                    compare = x => ((string)x.GetType().GetProperty(columnName).GetValue(x, null))
                        .CompareTo(key) <= flag;
                    break;
                case 0:
                    compare = x => ((string)x.GetType().GetProperty(columnName).GetValue(x, null))
                        .Equals(key);
                    break;
                case 1:
                    compare = x => ((string)x.GetType().GetProperty(columnName).GetValue(x, null))
                        .CompareTo(key) >= flag;
                    break;
                default:
                    compare = x => ((string)x.GetType().GetProperty(columnName).GetValue(x, null))
                        .Equals(key);
                    break;
            }

            IQueryable<TEntity> query = dbSet;
            return query.Where(compare).AsQueryable();
        }

    }
}
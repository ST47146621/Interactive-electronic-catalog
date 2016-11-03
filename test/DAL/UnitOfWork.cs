using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace test.Models
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DbContext context;
        private bool disposed;
        private Hashtable repositories;

        public UnitOfWork()
        {
            this.context = new ARealShopEntities();
        }

        public UnitOfWork(DbContext context)
        {
            this.context = context;
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (repositories == null)
                repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);

                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), context);

                repositories.Add(type, repositoryInstance);
            }

            return (IRepository<TEntity>)repositories[type];

        }

        public IQueryable<T> SqlQuery<T>(string query, params object[] parameters)
        {
            return context.Database.SqlQuery<T>(query, parameters).AsQueryable();
        }

        public void Save()
        {
            try
            {
                context.SaveChanges();
                context.Configuration.ValidateOnSaveEnabled = true;
            }
            catch (DbEntityValidationException dbEx)
            {

            }
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
            context.Configuration.ValidateOnSaveEnabled = true;
        }

        public void LazyLoadConfig(bool enable)
        {
            this.context.Configuration.LazyLoadingEnabled = enable;
        }

        public void ValidateOnSaveEnabled(bool enable)
        {
            this.context.Configuration.ValidateOnSaveEnabled = enable;
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
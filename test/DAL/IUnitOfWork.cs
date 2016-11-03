using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace test.Models
{
    public interface IUnitOfWork : IDisposable
    {
        void Dispose();
        void Dispose(bool disposing);
        void Save();
        Task SaveAsync();
        void LazyLoadConfig(bool enable);
        void ValidateOnSaveEnabled(bool enable);
        IQueryable<T> SqlQuery<T>(string query, params object[] parameters);
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;
    }
}
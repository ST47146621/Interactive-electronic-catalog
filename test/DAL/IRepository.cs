using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace test.Models
{
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Where 查詢
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includeProperties = null,
            int? page = null,
            int? pageSize = null);

        /// <summary>
        /// Where 查詢
        /// </summary>
        /// <param name="predicat"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        IQueryable<TEntity> Get(string predicat, params object[] values);

        /// <summary>
        /// SQL COMMAND 查詢
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IQueryable<TEntity> SqlQuery(string query, params object[] parameters);

        TEntity GetById(object id);
        void Attach(TEntity entity);
        void Create(TEntity entity);
        void CreateWithViewModel<TViewModel>(TViewModel viewModel, TEntity distination);
        void Delete(object id);
        void Delete(TEntity entity);
        void UpdateAll(TEntity entity);
        void UpdateWithViewModel<TViewModel>(TViewModel viewModel, TEntity distination);
        void UpdateAllWithViewModel<TViewModel>(TViewModel viewModel, TEntity distination);

        IQueryable<TEntity> CompareWith(string columnName, string key, int flag);
    }
}
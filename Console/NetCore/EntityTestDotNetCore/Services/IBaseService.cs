using EntityTestDotNetCore.Shared.Models;
using EntityTestDotNetCore.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace EntityTestDotNetCore.Services
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        /// <summary>
        /// Checks if database connection is possible.
        /// </summary>
        /// <returns>Possible TRUE/FALSE, or error</returns>
        Task<ServiceResponse<bool>> CanDbConnectAsync();

        /// <summary>
        /// Loads all data entries by condition.
        /// If condition is empty, all data entries will be loaded
        /// </summary>
        /// <param name="whereExpression">Custom-Filter Expression -> .Where(x => x.Id == 123)</param>
        /// <param name="includeExpression">Include Expression -> .Include(p => p.Products)</param>
        /// <returns>List of data entries, or error</returns>
        Task<ServiceResponse<List<TEntity>>>? GetListAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeExpression = null, Expression<Func<TEntity, bool>>? whereExpression = null);

    }
}

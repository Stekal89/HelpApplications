using EntityTestDotNetCore.Shared;
using EntityTestDotNetCore.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EntityTestDotNetCore.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        private readonly PubsContext _context;

        /// <summary>
        /// Service Context of Pubs database
        /// </summary>
        public PubsContext Context { get { return _context; } }

        /// <summary>
        /// Constructor of the Service
        /// </summary>
        /// <param name="context">Service-Context</param>
        public BaseService(PubsContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task<ServiceResponse<bool>> CanDbConnectAsync()
        {
            ServiceResponse<bool> response = new ServiceResponse<bool>();

            try
            {
                response = new ServiceResponse<bool>
                {
                    Data = await _context.Database.CanConnectAsync()
                };

                if (!response.Data)
                {
                    response.Message = $"Cannot connect to database";
                    response.Success = false;
                }
            }
            catch (Exception ex)
            {
                response.Message = $"CanDbConnectAsync: {ex.Message}";
                response.Success = false;
            }

            return response;
        }

        /// <inheritdoc />
        public async Task<ServiceResponse<List<TEntity>>>? GetListAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeExpression = null, Expression<Func<TEntity, bool>>? whereExpression = null)
        {
            ServiceResponse<List<TEntity>> response = new ServiceResponse<List<TEntity>>();

            var query = _context.Set<TEntity>().AsQueryable();

            if (includeExpression != null)
            {
                query = includeExpression(query);
            }

            if (whereExpression != null)
            {
                query = query.Where(whereExpression);
            }

            try
            {
                response = new ServiceResponse<List<TEntity>>()
                {
                    //Data = await _context.Authors.Include(x => x.Titleauthors).ToListAsync()
                    //Data = await _context.Set<TEntity>().ToListAsync()
                    
                    Data = await query.ToListAsync()
                };

                response.Success = response.Data?.Count > 0 ? true : false;
            }
            catch (Exception ex)
            {
                response.Message = $"GetAuthorsAsync:\n{ex.Message}";
                System.Diagnostics.Debug.WriteLine($"\n{response.Message}");
                response.Success = false;
            }

            return response;
        }
    }
}

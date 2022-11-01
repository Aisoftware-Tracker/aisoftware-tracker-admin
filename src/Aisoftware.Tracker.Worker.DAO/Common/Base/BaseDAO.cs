using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using Aisoftware.Tracker.Borders.Models;
using Aisoftware.Tracker.Worker.DAO.Common.Configurations;

using Microsoft.AspNetCore.Http;

namespace Aisoftware.Tracker.Worker.DAO.Common.Base;

abstract public class BaseDAO<T> : IBaseDAO<T> where T : class
{
    private readonly IAppConfiguration _config;

    #region constructor

    public virtual async Task<IEnumerable<T>> FindAll()
    {
        using (var connection = new NpgsqlConnection(_config.ConnectionString))
        {
            return (await connection.QueryAsync<T>("",
            new 
            {
        
            })).AsEnumerable();
        }
    }

    public virtual async Task<T> FindById(int id)
    {
        
        var response = new HttpResponseMessage();
        
        using (var connection = new NpgsqlConnection(_config.ConnectionString))
        {
            return await connection.QuerySingleOrDefaultAsync<T>("",
            new { id });
        }

    }

    public virtual async Task Save(T request)
    {
        using (var connection = new NpgsqlConnection(_config.ConnectionString))
        {
            await connection.QueryAsync<T>("",
            new
            {
                // request.Id,
                // request.Name,
                // request.Email
            });
        }
    }

    public Task<T> Update(T request)
    {
        throw new NotImplementedException();
    }

    #endregion

}

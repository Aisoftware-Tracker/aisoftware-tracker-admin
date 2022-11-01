using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using Aisoftware.Tracker.Worker.DAO.Common.Base;
using Aisoftware.Tracker.Borders.Models;
using Aisoftware.Tracker.Worker.DAO.Users;
using Aisoftware.Tracker.Worker.DAO.Common.Configurations;

using Microsoft.AspNetCore.Http;

namespace Aisoftware.Tracker.Worker.DAO.Users;

public class UserDAO : IBaseDAO<User>
{
    private readonly IAppConfiguration _config;

    #region constructor
    public UserDAO(IAppConfiguration config)
    {
        _config = config;
    }

    public async Task<IEnumerable<User>> FindAll()
    {
        using (var connection = new NpgsqlConnection(_config.ConnectionString))
        {
            return (await connection.QueryAsync<User>(UserQuery.FindAll,
            new 
            {
        
            })).AsEnumerable();
        }
    }

    public async Task<User> FindById(int id)
    {
        
        var response = new HttpResponseMessage();

        System.Console.WriteLine(id);
        
        using (var connection = new NpgsqlConnection(_config.ConnectionString))
        {
            return await connection.QuerySingleOrDefaultAsync<User>(UserQuery.FindById,
            new { id });
        }

    }

    public async Task Save(User request)
    {
        using (var connection = new NpgsqlConnection(_config.ConnectionString))
        {
            await connection.QueryAsync<User>(UserQuery.Save,
            new
            {
                request.Id,
                request.Name,
                request.Email
            });
        }
    }

    public Task<User> Update(User request)
    {
        throw new NotImplementedException();
    }

    #endregion

}

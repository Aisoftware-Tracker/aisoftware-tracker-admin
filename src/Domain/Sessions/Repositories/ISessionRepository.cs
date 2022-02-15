using System.Collections.Generic;
using System.Threading.Tasks;
using Aisoftware.Tracker.Admin.Models;
using System.Net;
using Aisoftware.Tracker.Admin.Domain.Common.Base.Repositories;

namespace Aisoftware.Tracker.Admin.Domain.Sessions.Repositories
{
    public interface ISessionRepository : IBaseRepository<Session>
    {
        Task<Session> Find(string endpoint);
        Task<Session> Create(Dictionary<string, string> request, string endpoint);
        Task Delete(string endpoint);
        Cookie GetCookie();
    }
}

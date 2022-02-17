using System.Collections.Generic;
using System.Threading.Tasks;
using Aisoftware.Tracker.Admin.Models;
using System.Net;

namespace Aisoftware.Tracker.Admin.Domain.Sessions.Repositories
{
    public interface ISessionRepository
    {
        Task<Session> Find(string cookieValue);
        Task<Session> Create(Dictionary<string, string> request, string cookieValue);
        Task Delete(string cookieValue);
        Cookie GetCookie();
    }
}

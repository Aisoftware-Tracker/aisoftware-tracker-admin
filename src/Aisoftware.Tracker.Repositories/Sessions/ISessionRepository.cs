using Aisoftware.Tracker.Borders.Models;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Aisoftware.Tracker.Repositories.Sessions.Repositories
{
    public interface ISessionRepository
    {
        Task<Session> Find(string cookieValue);
        Task<Session> Create(Dictionary<string, string> request, string cookieValue);
        Task Delete(string cookieValue);
        Cookie GetCookie();
    }
}

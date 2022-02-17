using System.Threading.Tasks;
using Aisoftware.Tracker.Admin.Models;

namespace Aisoftware.Tracker.Admin.Domain.Sessions.UseCases
{
    public interface ISessionUseCase
    {
        Task<Session> Find(string cookieValue);
        Task<Session> Create(Login login, string cookieValue);
        Task Delete(string cookieValue);
        string GetCookieValue();
    }
}

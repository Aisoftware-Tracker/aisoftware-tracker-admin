using Aisoftware.Tracker.Borders.Models;
using System.Threading.Tasks;

namespace Aisoftware.Tracker.UseCases.Sessions.UseCases
{
    public interface ISessionUseCase
    {
        Task<Session> Find(string cookieValue);
        Task<Session> Create(Login login, string cookieValue);
        Task Delete(string cookieValue);
        string GetCookieValue();
    }
}

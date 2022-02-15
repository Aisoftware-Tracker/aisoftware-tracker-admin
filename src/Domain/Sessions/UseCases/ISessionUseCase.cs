using System.Collections.Generic;
using System.Threading.Tasks;
using Aisoftware.Tracker.Admin.Models;
using Aisoftware.Tracker.Admin.Domain.Common.Base.UseCases;

namespace Aisoftware.Tracker.Admin.Domain.Sessions.UseCases
{
    public interface ISessionUseCase : IBaseUseCase<Session>
    {
        Task<Session> Find();
        Task<Session> Create(Login login);
        string GetCookieValue();
    }
}

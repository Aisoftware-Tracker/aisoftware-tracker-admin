using System.Collections.Generic;
using System.Threading.Tasks;
using Aisoftware.Tracker.Admin.Models;
using Aisoftware.Tracker.Admin.Domain.Sessions.Repositories;

namespace Aisoftware.Tracker.Admin.Domain.Sessions.UseCases
{
    public class SessionUseCase : ISessionUseCase
    {
        private readonly ISessionRepository _repository;

        public SessionUseCase(ISessionRepository repository)
        {
            _repository = repository;
        }

        public async Task<Session> Find(string cookieValue)
        {
            return await _repository.Find(cookieValue);
        }

        public async Task<Session> Create(Login login, string cookieValue)
        {
            var request = new Dictionary<string, string>
            {
                {"email", login.Email},
                {"password", login.Password}
            };

            return await _repository.Create(request, cookieValue);
        }

        public async Task Delete(string cookieValue)
        {
            await _repository.Delete(cookieValue);
        }

        public string GetCookieValue()
        {
           return _repository.GetCookie().Value;
        }

    }
}

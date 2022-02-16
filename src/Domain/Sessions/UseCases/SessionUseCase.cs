using System.Collections.Generic;
using System.Threading.Tasks;
using Aisoftware.Tracker.Admin.Models;
using Aisoftware.Tracker.Admin.Domain.Sessions.UseCases;
using Aisoftware.Tracker.Admin.Domain.Sessions.Repositories;
using Aisoftware.Tracker.Admin.Domain.Common.Configurations;
using Microsoft.AspNetCore.Http;
using Aisoftware.Tracker.Admin.Domain.Common.Constants;
using Aisoftware.Tracker.Admin.Domain.Common.Base.Repositories;
using Aisoftware.Tracker.Admin.Domain.Common.Base.UseCases;
using System;

namespace Aisoftware.Tracker.Admin.Domain.Sessions.UseCases
{
    public class SessionUseCase : ISessionUseCase
    {
        private readonly ISessionRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionUseCase(ISessionRepository repository, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Session> Find()
        {

            return await _repository.Find(Endpoints.SESSION);
        }

        public async Task<Session> Create(Login login)
        {
            var request = new Dictionary<string, string>
            {
                {"email", login.Email},
                {"password", login.Password}
            };

            return await _repository.Create(request, Endpoints.SESSION);
        }

        public async Task Delete()
        {
            var _cookieValue = _httpContextAccessor.HttpContext.Session.GetString(CookieName.JSESSIONID);
            
            await _repository.Delete(Endpoints.SESSION);
        }

        public string GetCookieValue()
        {
            return _repository.GetCookie().Value;
        }

        public async Task<IEnumerable<Session>> FindAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Session> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Session> Save(Session request)
        {
            throw new NotImplementedException();
        }

        public async Task<Session> Update(Session request)
        {
            throw new NotImplementedException();
        }

        public virtual async Task Delete(int id)
        {
            throw new NotImplementedException();
        }

    }
}

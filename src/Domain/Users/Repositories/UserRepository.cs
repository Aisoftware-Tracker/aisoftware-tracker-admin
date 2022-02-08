using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using Aisoftware.Tracker.Admin.Models;
using Aisoftware.Tracker.Admin.Domain.Users.Repositories;
using Aisoftware.Tracker.Admin.Domain.Common.Constants;
using Aisoftware.Tracker.Admin.Domain.Common.Configurations;
using Microsoft.AspNetCore.Http;

namespace Aisoftware.Tracker.Admin.Domain.Users.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IAppConfiguration _config;
        private readonly IHttpContextAccessor HttpContextAccessor;
        public static Cookie _cookie;
        private readonly string _url;
        private readonly Uri _uri;
        private string cookieValue;

        public UserRepository(IAppConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _config = config;
            _url = $"{_config.BaseUrl}/api/{Endpoints.USERS}";
            _uri = new Uri(_url);
            HttpContextAccessor = httpContextAccessor;
        }

        public Task Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<User>> FindAll()
        {
            string cookieValue = HttpContextAccessor.HttpContext.Session.GetString(CookieName.JSESSIONID);

            var handler = new HttpClientHandler
            {
                CookieContainer = GetCookieContainer(cookieValue)
            };

            IEnumerable<User> users = new List<User>();

            using (var httpClient = new HttpClient(handler))
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = _uri
                };
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType.JSON));
                request.Headers.Host = _uri.Host;

                var response = await httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    users = await response.Content.ReadAsAsync<IEnumerable<User>>();
                }

                return users;
            }

        }

        public Task<User> FindById(int userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> Save(User request)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> Update(User request)
        {
            throw new System.NotImplementedException();
        }

        private CookieContainer GetCookieContainer(string cookie)
        {
            _cookie = new Cookie(CookieName.JSESSIONID, cookie, "/", _config.BaseDomain);
            var cookies = new CookieContainer();
            cookies.Add(_cookie);
            return cookies;
        }
    }
}

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
using Newtonsoft.Json;
using System.Text;

namespace Aisoftware.Tracker.Admin.Domain.Users.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IAppConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public static Cookie _cookie;
        private readonly string _url;
        private string _cookieValue;

        public UserRepository(IAppConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _config = config;
            _url = $"{_config.BaseUrl}/api/{Endpoints.USERS}";
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task Delete(int id)
        {
            Uri uri = new Uri($"{_url}/{id}");

            var handler = new HttpClientHandler
            {
                CookieContainer = GetCookieContainer(_cookieValue)
            };

            using (var httpClient = new HttpClient(handler))
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Delete,
                    RequestUri = uri
                };
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType.JSON));
                request.Headers.Host = uri.Host;

                var response = await httpClient.SendAsync(request);
            }
        }

        public async Task<IEnumerable<User>> FindAll()
        {
            string _cookieValue = _httpContextAccessor.HttpContext.Session.GetString(CookieName.JSESSIONID);
            
            Uri uri = new Uri(_url);

            var handler = new HttpClientHandler
            {
                CookieContainer = GetCookieContainer(_cookieValue)
            };

            IEnumerable<User> users = new List<User>();

            using (var httpClient = new HttpClient(handler))
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = uri
                };
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType.JSON));
                request.Headers.Host = uri.Host;

                var response = await httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    users = await response.Content.ReadAsAsync<IEnumerable<User>>();
                }

                return users;
            }

        }

        public async Task<User> FindById(int userId)
        {
            Uri uri = new Uri($"{_url}/{userId}");

            string _cookieValue = _httpContextAccessor.HttpContext.Session.GetString(CookieName.JSESSIONID);

            var handler = new HttpClientHandler
            {
                CookieContainer = GetCookieContainer(_cookieValue)
            };

            User user = new User();

            using (var httpClient = new HttpClient(handler))
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = uri
                };
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType.JSON));
                request.Headers.Host = uri.Host;

                var response = await httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    user = await response.Content.ReadAsAsync<User>();
                }

                return user;
            }

        }

        public async Task<User> Save(User content)
        {
            Uri uri = new Uri(_url);

            var handler = new HttpClientHandler
            {
                CookieContainer = GetCookieContainer(_cookieValue)
            };

            using (var httpClient = new HttpClient(handler))
            {
                var json = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, ContentType.JSON);

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = uri, 
                    Content = json
                };

                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType.JSON));
                request.Headers.Host = uri.Host;

                var response = await httpClient.SendAsync(request);

                return await response.Content.ReadAsAsync<User>();

            }
        }


        public async Task<User> Update(User content)
        {
            Uri uri = new Uri(_url);

            var handler = new HttpClientHandler
            {
                CookieContainer = GetCookieContainer(_cookieValue)
            };

            using (var httpClient = new HttpClient(handler))
            {
                var json = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, ContentType.JSON);

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Put,
                    RequestUri = uri, 
                    Content = json
                };

                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType.JSON));
                request.Headers.Host = uri.Host;

                var response = await httpClient.SendAsync(request);

                return await response.Content.ReadAsAsync<User>();

            }
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

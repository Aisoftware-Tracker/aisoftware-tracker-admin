using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Aisoftware.Tracker.Admin.Domain.Common.Configurations;
using Aisoftware.Tracker.Admin.Domain.Common.Constants;
using Aisoftware.Tracker.Admin.Domain.Users.Repositories;
using Aisoftware.Tracker.Admin.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Linq;

namespace Aisoftware.Tracker.Admin.Domain.Common.Base.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly IAppConfiguration _config;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public static Cookie _cookie;

        private readonly string _url;

        private string _cookieValue;
        T item;

        public BaseRepository(IAppConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _config = config;
            _url = $"{_config.BaseUrl}";
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task Delete(int id, string endpoint)
        {
            _cookieValue = _httpContextAccessor.HttpContext.Session.GetString(CookieName.JSESSIONID);

            Uri uri = new Uri($"{_url}/{endpoint}/{id}");

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

        public async Task<IEnumerable<T>> FindAll(string endpoint)
        {
            _cookieValue = _httpContextAccessor.HttpContext.Session.GetString(CookieName.JSESSIONID);

            Uri uri = new Uri($"{_url}/{endpoint}");

            var handler = new HttpClientHandler
            {
                CookieContainer = GetCookieContainer(_cookieValue)
            };

            IEnumerable<T> items = new List<T>();

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
                    items = await response.Content.ReadAsAsync<IEnumerable<T>>();
                }

                return items;
            }

        }

        public async Task<T> FindById(int id, string endpoint)
        {
            _cookieValue = _httpContextAccessor.HttpContext.Session.GetString(CookieName.JSESSIONID);

            Uri uri = new Uri($"{_url}/{endpoint}/{id}");

            var handler = new HttpClientHandler
            {
                CookieContainer = GetCookieContainer(_cookieValue)
            };

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
                    item = await response.Content.ReadAsAsync<T>();
                }

                return item;
            }

        }

        public async Task<T> Save(T content, string endpoint)
        {
            _cookieValue = _httpContextAccessor.HttpContext.Session.GetString(CookieName.JSESSIONID);

            Uri uri = new Uri($"{_url}/{endpoint}");

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

                return await response.Content.ReadAsAsync<T>();

            }
        }

        public async Task<T> Update(T content, string endpoint)
        {
            _cookieValue = _httpContextAccessor.HttpContext.Session.GetString(CookieName.JSESSIONID);

            Uri uri = new Uri($"{_url}/{endpoint}");

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

                return await response.Content.ReadAsAsync<T>();

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

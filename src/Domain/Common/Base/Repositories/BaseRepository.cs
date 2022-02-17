using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Aisoftware.Tracker.Admin.Domain.Common.Configurations;
using Aisoftware.Tracker.Admin.Domain.Common.Constants;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Aisoftware.Tracker.Admin.Domain.Common.Base.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly IAppConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public static Cookie _cookie;
        private readonly string _url;
        private string _cookieValue;
        private T item;
        private HttpClientHandler _handler;
        private Uri _uri;

        public BaseRepository(IAppConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _config = config;
            _url = $"{_config.BaseUrl}";
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<T>> FindAll(string endpoint)
        {
            _cookieValue = _httpContextAccessor.HttpContext.Session.GetString(CookieName.JSESSIONID);

            _uri = new Uri($"{_url}/{endpoint}");

            _handler = new HttpClientHandler();
            _handler.CookieContainer = GetCookieContainer(_cookieValue);

            IEnumerable<T> items = new List<T>();

            using (var httpClient = new HttpClient(_handler))
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
                    items = await response.Content.ReadAsAsync<IEnumerable<T>>();
                }

                return items;
            }

        }

        public async Task<T> FindById(int id, string endpoint)
        {
            _cookieValue = _httpContextAccessor.HttpContext.Session.GetString(CookieName.JSESSIONID);

            _uri = new Uri($"{_url}/{endpoint}/{id}");

            _handler = new HttpClientHandler();
            _handler.CookieContainer = GetCookieContainer(_cookieValue);

            using (var httpClient = new HttpClient(_handler))
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
                    item = await response.Content.ReadAsAsync<T>();
                }

                return item;
            }

        }

        public async Task<T> Save(T content, string endpoint)
        {
            _uri = new Uri($"{_url}/{endpoint}");
            _handler = new HttpClientHandler();
            _cookieValue = _httpContextAccessor.HttpContext.Session.GetString(CookieName.JSESSIONID);
            _handler.CookieContainer = GetCookieContainer(_cookieValue);

            using (var httpClient = new HttpClient(_handler))
            {
                var json = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, ContentType.JSON);

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = _uri,
                    Content = json
                };

                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType.JSON));
                request.Headers.Host = _uri.Host;

                var response = await httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    item = await response.Content.ReadAsAsync<T>();
                }

                return item;
            }
        }

        public async Task<T> Update(T content, string endpoint)
        {
            _uri = new Uri($"{_url}/{endpoint}");
            _handler = new HttpClientHandler();
            _handler.UseCookies = false;

            using (var httpClient = new HttpClient(_handler))
            {
                using (var request = new HttpRequestMessage(HttpMethod.Put, _uri))
                {
                    // request.Headers.TryAddWithoutValidation("Authorization", "Basic c3Vwb3J0ZUBhaXNvZnR3YXJlLmNvbS5icjpAMjAyMQ==");
                    request.Headers.TryAddWithoutValidation(HeaderKey.COOKIE, $"{CookieName.JSESSIONID}={_cookieValue}");

                    var json = JsonConvert.SerializeObject(content);

                    request.Content = new StringContent(json, Encoding.UTF8, ContentType.JSON);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse(ContentType.JSON);

                    var response = await httpClient.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        item = await response.Content.ReadAsAsync<T>();
                    }

                    return item;
                }
            }
        }

        public async Task Delete(int id, string endpoint)
        {
            _uri = new Uri($"{_url}/{endpoint}/{id}");

            _handler = new HttpClientHandler();
            _handler.UseCookies = false;

            using (var httpClient = new HttpClient(_handler))
            {
                using (var request = new HttpRequestMessage(HttpMethod.Delete, _uri))
                {
                    request.Headers.TryAddWithoutValidation(HeaderKey.COOKIE, $"{CookieName.JSESSIONID}={_cookieValue}");

                    var response = await httpClient.SendAsync(request);
                }
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

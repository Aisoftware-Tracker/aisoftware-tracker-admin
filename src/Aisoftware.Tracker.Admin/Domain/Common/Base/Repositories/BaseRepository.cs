using Aisoftware.Tracker.Admin.Domain.Common.Configurations;
using Aisoftware.Tracker.Borders.Constants;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Aisoftware.Tracker.Admin.Domain.Common.Base.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly IAppConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public static Cookie _cookie;
        private readonly string _url;
        private string _cookieValue;
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

                return await GetResponse(response, id);
            }

        }

        public async Task<T> Save(T input, string endpoint)
        {
            var content = ReplaceContent(input);

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

                return await GetResponse(response, content);
            }
        }

        public async Task<T> Update(T content, string endpoint)
        {
            _cookieValue = _httpContextAccessor.HttpContext.Session.GetString(CookieName.JSESSIONID);
            _uri = new Uri($"{_url}/{endpoint}/{GetIdValue(content)}");
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

                    return await GetResponse(response, content);
                }
            }
        }

        public async Task<HttpResponseMessage> Delete(int id, string endpoint)
        {
            _cookieValue = _httpContextAccessor.HttpContext.Session.GetString(CookieName.JSESSIONID);
            _uri = new Uri($"{_url}/{endpoint}/{id}");
            _handler = new HttpClientHandler();
            _handler.UseCookies = false;

            HttpResponseMessage response = new HttpResponseMessage();

            using (var httpClient = new HttpClient(_handler))
            {
                using (var request = new HttpRequestMessage(HttpMethod.Delete, _uri))
                {
                    request.Headers.TryAddWithoutValidation(HeaderKey.COOKIE, $"{CookieName.JSESSIONID}={_cookieValue}");

                    response = await httpClient.SendAsync(request);

                    if (response.StatusCode != HttpStatusCode.NoContent)
                    {
                        throw new Exception($"{response.ReasonPhrase} -> Error deleting item");
                    }
                }
            }

            return response;
        }

        private CookieContainer GetCookieContainer(string cookie)
        {
            _cookie = new Cookie(CookieName.JSESSIONID, cookie, "/", _config.BaseDomain);
            var cookies = new CookieContainer();
            cookies.Add(_cookie);
            return cookies;
        }

        private async Task<T> GetResponse(HttpResponseMessage response, T content)
        {
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<T>();
            }
            else
            {
                throw new Exception($"{response.ReasonPhrase} -> {response.RequestMessage}\n CONTENT: {JsonConvert.SerializeObject(content)}");
            }
        }

        private async Task<T> GetResponse(HttpResponseMessage response, int id)
        {
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<T>();
            }
            else
            {
                throw new Exception($"{response.ReasonPhrase} -> {response.RequestMessage}\n ID: {id}");
            }
        }

        ///TODO Remover metodo duplicado
        private int GetIdValue(T content)
        {
            var id = 0;
            var type = content.GetType();
            var property = type.GetProperty("Id");
            if (!(property is null))
            {
                id = (int)property.GetValue(content);
            }
            return id;
        }

        private T ReplaceContent(T content)
        {
            var type = content.GetType();

            foreach (var property in type.GetProperties())
            {
                if (property.PropertyType == typeof(string))
                {
                    if (property.GetValue(content) is null)
                    {
                        property.SetValue(content, "");
                    }
                }
                if (property.PropertyType == typeof(object))
                {
                    if (property.GetValue(content) is null)
                    {
                        property.SetValue(content, new object());
                    }
                }
                else if (property.PropertyType == typeof(DateTime))
                {
                    if (property.GetValue(content) is null)
                    {
                        property.SetValue(content, "");
                    }
                }
                else if (property.PropertyType == typeof(bool))
                {
                    if (property.GetValue(content) is null)
                    {
                        property.SetValue(content, false);
                    }
                }
                else if (property.PropertyType == typeof(int))
                {
                    if (property.GetValue(content) is null)
                    {
                        property.SetValue(content, 0);
                    }
                }
                else if (property.PropertyType == typeof(decimal))
                {
                    if (property.GetValue(content) is null)
                    {
                        property.SetValue(content, 0);
                    }
                }
                else if (property.PropertyType == typeof(long))
                {
                    if (property.GetValue(content) is null)
                    {
                        property.SetValue(content, 0);
                    }
                }
            }

            return content;
        }

    }
}

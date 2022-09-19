using Aisoftware.Tracker.Common.Configurations;
using Aisoftware.Tracker.Borders.Constants;
using Aisoftware.Tracker.Borders.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Aisoftware.Tracker.Sessions.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly IAppConfiguration _config;
        public static Cookie _cookie;
        private readonly string _url;
        private readonly Uri _uri;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionRepository(IAppConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _config = config;
            _url = $"{_config.BaseUrl}/{Endpoints.SESSION}";
            _uri = new Uri(_url);
            _httpContextAccessor = httpContextAccessor;
        }

        public Cookie GetCookie()
        {
            return _cookie;
        }

        public async Task<Session> Find(string cookieValue)
        {
            cookieValue = _httpContextAccessor.HttpContext.Session.GetString(CookieName.JSESSIONID);

            var handler = new HttpClientHandler
            {
                CookieContainer = GetCookieContainer(cookieValue)
            };

            var session = new Session();

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
                    session = await response.Content.ReadAsAsync<Session>();
                }

                return session;
            }
        }

        public async Task<Session> Create(Dictionary<string, string> request, string cookieValue)
        {
            return await PostFormUrlEncoded<Session>(_url, request, cookieValue);
        }

        private async Task<TResult> PostFormUrlEncoded<TResult>(string url, IEnumerable<KeyValuePair<string, string>> postData, string cookieValue)
        {
            var cookies = new CookieContainer();
            var handler = new HttpClientHandler
            {
                CookieContainer = cookies
            };

            using (var httpClient = new HttpClient(handler))
            {
                using (var content = new FormUrlEncodedContent(postData))
                {
                    content.Headers.Clear();
                    content.Headers.Add("Content-Type", ContentType.FORM_URLENCODE);

                    var response = await httpClient.PostAsync(url, content);

                    var cookieList = cookies.GetCookies(_uri);

                    _cookie = cookieList[0];

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception(response.ReasonPhrase);
                    }

                    var t = await response.Content.ReadAsAsync<TResult>();

                    return t;
                }
            }
        }

        public async Task Delete(string cookieValue)
        {
            var handler = new HttpClientHandler
            {
                CookieContainer = GetCookieContainer(cookieValue)
            };

            using (var httpClient = new HttpClient(handler))
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Delete,
                    RequestUri = _uri
                };
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType.JSON));
                request.Headers.Host = _uri.Host;

                await httpClient.SendAsync(request);
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

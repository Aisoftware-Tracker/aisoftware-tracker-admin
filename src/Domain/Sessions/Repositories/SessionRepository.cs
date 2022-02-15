using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using Aisoftware.Tracker.Admin.Models;
using Aisoftware.Tracker.Admin.Domain.Sessions.Repositories;
using Aisoftware.Tracker.Admin.Domain.Common.Constants;
using Aisoftware.Tracker.Admin.Domain.Common.Configurations;
using Microsoft.AspNetCore.Http;
using Aisoftware.Tracker.Admin.Domain.Common.Base.Repositories;

namespace Aisoftware.Tracker.Admin.Domain.Sessions.Repositories
{

    public class SessionRepository : BaseRepository<Session>, ISessionRepository
    {
        private readonly IAppConfiguration _config;
        public static Cookie _cookie;
        private readonly string _url;
        private Uri uri;
        private string _cookieValue;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public SessionRepository(IAppConfiguration config, IHttpContextAccessor httpContextAccessor)
        : base(config, httpContextAccessor)
        {
            _config = config;
            _url = $"{_config.BaseUrl}";
            _httpContextAccessor = httpContextAccessor;
        }

        
        public Cookie GetCookie()
        {
            return _cookie;
        }

        public async Task<Session> Find(string endpoint)
        {
            _cookieValue = _httpContextAccessor.HttpContext.Session.GetString(CookieName.JSESSIONID);

            Uri uri = new Uri($"{_url}/{endpoint}");

            var handler = new HttpClientHandler
            {
                CookieContainer = GetCookieContainer(_cookieValue)
            };

            var session = new Session();

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
                    session = await response.Content.ReadAsAsync<Session>();
                }

                return session;
            }
        }

        public async Task<Session> Create(Dictionary<string, string> request, string endpoint)
        {
            return await PostFormUrlEncoded<Session>(request, endpoint);
        }

        //TODO Criar interfaces e Classes genericas paras os verbos http e suas implementacoes
        private async Task<TResult> PostFormUrlEncoded<TResult>(IEnumerable<KeyValuePair<string, string>> postData, string endpoint)
        {
            string url = $"{_url}/{endpoint}";

            Uri uri = new Uri(url);

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

                    var cookieList = cookies.GetCookies(uri);

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

        public async Task Delete(string endpoint)
        {
            Uri uri = new Uri($"{_url}/{endpoint}");
            
            _cookieValue = _httpContextAccessor.HttpContext.Session.GetString(CookieName.JSESSIONID);

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

                var t = response;
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

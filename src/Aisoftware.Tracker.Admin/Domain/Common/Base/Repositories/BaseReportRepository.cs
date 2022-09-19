using Aisoftware.Tracker.Admin.Domain.Common.Configurations;
using Aisoftware.Tracker.Borders.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Aisoftware.Tracker.Admin.Domain.Common.Base.Repositories
{
    public class BaseReportRepository<T> : IBaseReportRepository<T> where T : class
    {
        private readonly IAppConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public static Cookie _cookie;
        private readonly string _url;
        private string _cookieValue;
        private HttpClientHandler _handler;
        private Uri _uri;

        public BaseReportRepository(IAppConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _config = config;
            _url = $"{_config.BaseUrl}";
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<T>> FindAll(string endpoint, IDictionary<string, string> queryParams)
        {
            _cookieValue = _httpContextAccessor.HttpContext.Session.GetString(CookieName.JSESSIONID);

            var url = QueryHelpers.AddQueryString($"{_url}/{endpoint}", queryParams);

            _uri = new Uri(url);

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

        private CookieContainer GetCookieContainer(string cookie)
        {
            _cookie = new Cookie(CookieName.JSESSIONID, cookie, "/", _config.BaseDomain);
            var cookies = new CookieContainer();
            cookies.Add(_cookie);
            return cookies;
        }

    }
}

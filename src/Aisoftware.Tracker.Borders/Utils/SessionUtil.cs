using System;

using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using Aisoftware.Tracker.Borders.Models;
using Aisoftware.Tracker.Borders.Constants;
using Microsoft.AspNetCore.Http;

namespace Aisoftware.Tracker.Borders.Services
{
    public class SessionUtil : ISessionUtil
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionUtil(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserNameAndEmail()
        {
            var userName = string.Empty;
            var userEmail = string.Empty;

            if (_httpContextAccessor.HttpContext.Session != null)
            {
                userName = _httpContextAccessor.HttpContext.Session.GetString(SessionKey.USER_NAME);
                userEmail = _httpContextAccessor.HttpContext.Session.GetString(SessionKey.USER_EMAIL);
            }

            return $"{userName} ({userEmail})";
        }
    }
}
using System;

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Microsoft.AspNetCore.Routing;
using System.Net.Http.Headers;
using Aisoftware.Tracker.Borders.Constants;

namespace Aisoftware.Tracker.Borders.Services
{
    public interface ISessionUtil
    {
        string GetUserNameAndEmail();
    } 
    
}
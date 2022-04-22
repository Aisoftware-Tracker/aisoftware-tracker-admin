using System;
using Aisoftware.Tracker.Admin;

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Microsoft.AspNetCore.Routing;
using System.Net.Http.Headers;
using Aisoftware.Tracker.Admin.Domain.Common.Constants;

namespace Aisoftware.Tracker.Admin.Common.Util
{
    public interface ILogUtil
    {
        string Succes(string className, string ActionName, string message = "");
        string Info(string className, string ActionName, string message = "");
        string Error(string className, string ActionName, Exception exception = null, string message = "");
        string Forbidden(string className, string ActionName, string message = "");
    } 
    
}
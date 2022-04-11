using System;
using Aisoftware.Tracker.Admin;
using Aisoftware.Tracker.Admin.Common.Util;

namespace Aisoftware.Tracker.Admin.Common.Util
{
    public class LogUtil : ILogUtil
    {
        private readonly ISessionUtil _sessionUtil;

        public LogUtil(ISessionUtil sessionUtil)
        {
            _sessionUtil = sessionUtil;
        }

        public string Succes(string className, string ActionName)
        {
            return GetMessage("SUCCESS", className, ActionName);
        }

        public string Error(string className, string ActionName, Exception exception = null)
        {
            return GetMessage("ERROR", className, ActionName, exception);
        }

        public string Unauthorized(string className, string ActionName)
        {
            return GetMessage("ACCESS_DENIED", className, ActionName);
        }

        private string GetMessage(string type, string className, string actionName, Exception exception = null)
        {
            string exceptionMessage = exception == null ? string.Empty :  $"\nEXCEPTION: {ExceptionHelper.InnerException(exception).Message}";
            string userMessage = "USER: ";
            userMessage += string.IsNullOrEmpty(_sessionUtil.GetUserNameAndEmail()) ? "ERROR_NOT_FOUND" : _sessionUtil.GetUserNameAndEmail();

            return $"{type}: {className}::{actionName}; {userMessage}; {exceptionMessage}";
        }

    }
}
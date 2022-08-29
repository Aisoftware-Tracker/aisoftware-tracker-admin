using Aisoftware.Tracker.Borders.Exceptions;

namespace Aisoftware.Tracker.Borders.Utils
{
    public class LogUtil : ILogUtil
    {
        private readonly ISessionUtil _sessionUtil;

        public LogUtil(ISessionUtil sessionUtil)
        {
            _sessionUtil = sessionUtil;
        }

        public string Succes(string className, string ActionName, string message = "")
        {
            return GetMessage("SUCCESS", className, ActionName, message);
        }

        public string Info(string className, string ActionName, string message = "")
        {
            return GetMessage("INFO", className, ActionName, message);
        }

        public string Error(string className, string ActionName, Exception exception = null, string message = "" )
        {
            return GetMessage("ERROR", className, ActionName, message, exception);
        }

        public string Forbidden(string className, string ActionName, string message = "")
        {
            return GetMessage("FORBIDDEN", className, ActionName, message);
        }

        private string GetMessage(string type, string className, string actionName, string message, Exception exception = null)
        {
            string exceptionMessage = exception == null ? string.Empty : $"\nEXCEPTION: {ExceptionHelper.InnerException(exception).Message}";
            string userMessage = "USER: ";
            userMessage += string.IsNullOrEmpty(_sessionUtil.GetUserNameAndEmail()) ? "ERROR_NOT_FOUND" : _sessionUtil.GetUserNameAndEmail();

            return $"{type}: {className}::{actionName}; {userMessage}; {message} {exceptionMessage}";
        }

    }
}
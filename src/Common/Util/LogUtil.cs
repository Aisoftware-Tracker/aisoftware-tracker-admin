using System;
using Aisoftware.Tracker.Admin;

namespace Aisoftware.Tracker.Admin.Common.Util
{
    public static class LogUtil
    {
        public static string Succes(string className, string ActionName)
        {
            return $"SUCCESS: { className }::{ ActionName }";
        }

        public static string Error(string className, string ActionName, Exception exception)
        {
            return $"ERROR: { className }::{ ActionName }\nEXCEPTION:{ExceptionHelper.InnerException(exception)?.Message}";
        }

        public static string Error(string className, string ActionName, string message)
        {
            return $"ERROR: { className }::{ ActionName }\nMESSAGE:{message}";
        }
    }
}
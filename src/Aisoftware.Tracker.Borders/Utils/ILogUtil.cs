namespace Aisoftware.Tracker.Borders.Services;
public interface ILogUtil
{
    string Succes(string className, string ActionName, string message = "");
    string Info(string className, string ActionName, string message = "");
    string Error(string className, string ActionName, Exception? exception = null, string message = "");
    string Forbidden(string className, string ActionName, string message = "");
}

using Aisoftware.Tracker.Borders.Models;

namespace Aisoftware.Tracker.Common.Base.Services
{
    public interface ITokenService
    {
        string GenerateToken(Session user, string cookieValue);
    }
}
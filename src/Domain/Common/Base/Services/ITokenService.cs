using Aisoftware.Tracker.Admin.Models;

namespace Aisoftware.Tracker.Admin.Domain.Common.Base.Services
{
    public interface ITokenService
    {
        string GenerateToken(Session user, string cookieValue);
        string GenerateRefreshToken();
        void SaveRefreshToken(string username, string refreshToken);
        
    }
}
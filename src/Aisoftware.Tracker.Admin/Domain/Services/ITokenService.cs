using Aisoftware.Tracker.Borders.Models;

namespace Aisoftware.Tracker.Admin.Domain.Services;

public interface ITokenService
{
    string GenerateToken(Session user, string cookieValue);
}

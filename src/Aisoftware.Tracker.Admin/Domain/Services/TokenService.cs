using Aisoftware.Tracker.Admin.Domain.Services;
using Aisoftware.Tracker.Repositories.Common.Configurations;
using Aisoftware.Tracker.Borders.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Aisoftware.Tracker.Borders.Services;

public class TokenService : ITokenService
{
    private readonly IAppConfiguration _config;
    private const int TIME_EXPIRATION = 8;

    public TokenService(IAppConfiguration config)
    {
        _config = config;
    }

    public string GenerateToken(Session user, string cookieValue)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_config.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.DeviceReadonly ? "readonly" : "admin"),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("JSESSIONID", cookieValue)
            }),
            Expires = DateTime.UtcNow.AddHours(TIME_EXPIRATION),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

}
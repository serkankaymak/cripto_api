using Application.Settings;
using Domain.Domains.IdentityDomain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services.InternalServices.JwtService;
public class JwtTokenService : IJwtTokenService
{
    private readonly JwtConfig jwtSettings;

    public JwtTokenService(JwtConfig jwtSettings)
    {
        this.jwtSettings = jwtSettings;
    }

    public string GenerateToken(UserIdentity user)
    {
        var key = Encoding.UTF8.GetBytes(jwtSettings.Secret);
        var securityKey = new SymmetricSecurityKey(key);
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                // Diğer claim'ler...
            };

        var token = new JwtSecurityToken(
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(jwtSettings.ExpiryMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


    public bool ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(jwtSettings.Secret);
        var validationParameters = jwtSettings.GetValidationParameters();

        var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
        if (principal != null)
        {
            var isAdminClaim = principal.Claims.FirstOrDefault(c => c.Type == "isAdmin")?.Value;
            var isVerifiedClaim = principal.Claims.FirstOrDefault(c => c.Type == "isVerified")?.Value;
            bool isAdmin = isAdminClaim != null && bool.Parse(isAdminClaim);
            bool isVerified = isVerifiedClaim != null && bool.Parse(isVerifiedClaim);
        }

        return true;

    }

}
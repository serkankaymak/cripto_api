using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Application.Settings;

public class JwtConfig
{
    public string Secret { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int ExpiryMinutes { get; set; }

    public bool ValidateIssuer { get; set; } = false;
    public bool ValidateAudience { get; set; } = false;
    public bool ValidateLifetime { get; set; } = false;
    public bool ValidateIssuerSigningKey { get; set; } = false;


    public TokenValidationParameters GetValidationParameters()
    {
        var key = Encoding.UTF8.GetBytes(Secret);
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = ValidateIssuer,
            ValidateAudience = ValidateAudience,
            ValidateLifetime = ValidateLifetime,
            ValidateIssuerSigningKey = ValidateIssuerSigningKey,
            ValidIssuer = Issuer,
            ValidAudience = Audience,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };

        return validationParameters;
    }

}
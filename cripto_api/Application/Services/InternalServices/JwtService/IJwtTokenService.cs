using Domain.Domains.IdentityDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.InternalServices.JwtService;

public interface IJwtTokenService
{
    string GenerateToken(UserIdentity user);
    bool ValidateToken(string token);
}
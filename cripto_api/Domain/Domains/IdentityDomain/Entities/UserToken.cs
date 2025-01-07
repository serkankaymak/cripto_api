using Microsoft.AspNetCore.Identity;
using Shared.ApiResponse;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domains.IdentityDomain.Entities
{
    public class UserToken : IdentityUserToken<int>, IEntity
    {
        public int Id { get; set; }
        public string? Jwt { get; set; }
        public DateTime? JwtExpiredAt { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiredAt { get; set; }


        public void UpdateToken(string jwt, string? refreshToken = null)
        {
            if (jwt.Length < 10) throw ExceptionFactory.UnprocessableEntity();
            this.Jwt = jwt;
            this.RefreshToken = refreshToken;
        }
    }
}

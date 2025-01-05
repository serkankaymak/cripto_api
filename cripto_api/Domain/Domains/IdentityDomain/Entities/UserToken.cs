using Microsoft.AspNetCore.Identity;
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
        public string? jwt { get; set; }
        public DateTime? jwtExpiredAt { get; set; }
        public string? refreshToken { get; set; }
        public DateTime? refreshTokenExpiredAt { get; set; }



        public void UpdateToken(string jwt, string? refreshToken = null)
        {
            this.jwt = jwt;
            this.refreshToken = refreshToken;
        }
    }
}

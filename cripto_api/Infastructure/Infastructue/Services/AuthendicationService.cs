using Application;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Services.InternalServices.JwtService;
using Application.Services.ExternalServices;
using Domain.Domains.IdentityDomain.Entities;

namespace Infastructure.Infastructue.Services
{
    public class AuthendicationService : IAuthendicationService
    {
        UserManager<UserIdentity> userManager;
        RoleManager<RoleIdentity> roleManager;
        IJwtTokenService jwtTokenService;

        public AuthendicationService(UserManager<UserIdentity> userManager, RoleManager<RoleIdentity> roleManager, IJwtTokenService jwtTokenService)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.jwtTokenService = jwtTokenService;
        }


        public async Task<string> AuthenticateAsync(string email, string password)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (ApplicationManager.isDeveloping) return jwtTokenService.GenerateToken(new UserIdentity { Email = email, PasswordHash = password });

            if (user == null) throw new UnauthorizedAccessException("Geçersiz kullanıcı adı veya şifre.");
            //bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            bool isPasswordValid = password == user.PasswordHash;
            if (!isPasswordValid) throw new UnauthorizedAccessException("Geçersiz kullanıcı adı veya şifre.");

            return jwtTokenService.GenerateToken(user);


        }

    }
}
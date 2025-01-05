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
using Shared.ApiResponse;
using Shared.Events;
using Application.Events;

namespace Infastructure.Infastructue.Services
{
    public class IdentityService : IIdentityService
    {
        IEventDispatcher eventDispatcher;
        UserManager<UserIdentity> userManager;
        RoleManager<RoleIdentity> roleManager;
        IJwtTokenService jwtTokenService;

        public IdentityService(UserManager<UserIdentity> userManager, RoleManager<RoleIdentity> roleManager, IJwtTokenService jwtTokenService, IEventDispatcher eventDispatcher)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.jwtTokenService = jwtTokenService;
            this.eventDispatcher = eventDispatcher;
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

        public async Task UpdateMobilePushNotificationTokenOfUser(string email, string token)
        {
            UserIdentity? user = await userManager.FindByEmailAsync(email);
            if (user == null) throw ExceptionFactory.NotFound();
            try
            {
                user.UpdateMobileNotificationToken(token);
                await userManager.UpdateAsync(user);
                _ = eventDispatcher.Publish(new UserMobileNotificationTokenUpdatedEvent(user.Id, token, user.Email));
            }
            catch (Exception)
            { throw ExceptionFactory.InternalServerError(""); }

        }



    }
}
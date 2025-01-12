using Application;
using Microsoft.AspNetCore.Identity;
using Application.Services.ExternalServices;
using Domain.Domains.IdentityDomain.Entities;
using Shared.ApiResponse;
using Shared.Events;
using Application.Events;
using Infastructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Domain.Events;
using Domain.Domains.IdentityDomain.JwtService;

namespace Infastructure.Infastructue.Services
{
    public class IdentityService : AGenericRepository<UserIdentity>, IIdentityService
    {
        IEventDispatcher _eventDispatcher;
        UserManager<UserIdentity> _userManager;
        RoleManager<RoleIdentity> _roleManager;
        IJwtTokenService _jwtTokenService;

        public IdentityService(ApplicationDbContext context, IEventDispatcher eventDispatcher, UserManager<UserIdentity> userManager, RoleManager<RoleIdentity> roleManager, IJwtTokenService jwtTokenService) : base(context)
        {
            this._eventDispatcher = eventDispatcher;
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._jwtTokenService = jwtTokenService;
        }

        public async Task<string> AuthenticateAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (ApplicationManager.isDeveloping) return _jwtTokenService.GenerateToken(new UserIdentity { Email = email, PasswordHash = password });

            if (user == null) throw new UnauthorizedAccessException("Geçersiz kullanıcı adı veya şifre.");
            //bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            bool isPasswordValid = password == user.PasswordHash;
            if (!isPasswordValid) throw new UnauthorizedAccessException("Geçersiz kullanıcı adı veya şifre.");
            return _jwtTokenService.GenerateToken(user);
        }

        public async Task<List<UserIdentity>> GetPushNotificationTokensInUserPermittedTopic(PushNotifcationTopics topic)
        {

            // Predicate: Kullanıcı aktif ve silinmemiş olmalı
            Expression<Func<UserIdentity, bool>> predicate = user => user.Preferences.Any(x => x.ToPreference().IsTrue(topic)) && !user.IsDeleted;

            // Includes: Preferences ilişkisini dahil et
            Func<IQueryable<UserIdentity>, IQueryable<UserIdentity>> includePreferences = query => query.Include(user => user.Preferences);

            // GetAsync metodunu çağır
            var users = await WhereAsync(
                predicate: predicate,
                includes: new Func<IQueryable<UserIdentity>, IQueryable<UserIdentity>>[] { includePreferences },
                includeSoftDeleted: false // Soft delete filtrelemesi zaten predicate'te yapılıyor
            );
            if (users == null) return new List<UserIdentity>();
            return users.ToList();
        }



        public async Task UpdateMobilePushNotificationTokenOfUser(string email, string token)
        {
            UserIdentity? user = await _userManager.FindByEmailAsync(email);
            if (user == null) throw ExceptionFactory.NotFound();
            string? oldToken = user.PushNotificationToken;
            try
            {
                user.UpdateMobileNotificationToken(token);
                await _userManager.UpdateAsync(user);
                var @event = new UserMobileNotificationTokenUpdatedEvent(user.Id, user.Preferences,
                   oldToken, token, user.Email, user.MobileClientType);
                _ = _eventDispatcher.Publish(this, @event);
            }
            catch (Exception)
            { throw ExceptionFactory.InternalServerError(""); }

        }


        /// <summary>
        /// Yeni bir kullanıcı kaydı gerçekleştirir ve JWT token döndürür.
        /// </summary>
        /// <param name="email">Kullanıcının e-posta adresi.</param>
        /// <param name="password">Kullanıcının parolası.</param>
        /// <returns>JWT token string.</returns>
        /// <exception cref="ArgumentException">Geçersiz parametreler.</exception>
        /// <exception cref="InvalidOperationException">Kullanıcı oluşturulamadı.</exception>
        public async Task<string> RegisterAsync(string email, string password, string? name = null)
        {
            // 1. Parametrelerin doğrulanması
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                throw ExceptionFactory.ArgumentException();


            // 2. Kullanıcının mevcut olup olmadığının kontrolü
            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser != null)
                throw ExceptionFactory.BusinessRuleViolation("Bu e-posta adresiyle zaten bir kullanıcı bulunmaktadır.");

            // 3. Yeni kullanıcı nesnesinin oluşturulması
            var newUser = new UserIdentity
            {
                Name = name != null ? name : email, // Genellikle kullanıcı adı olarak e-posta kullanılır
                Email = email,
                IsActive = true, // Varsayılan olarak aktif
                                 // Diğer gerekli alanları burada ayarlayın

            };

            // 4. Kullanıcının oluşturulması
            var createResult = await _userManager.CreateAsync(newUser, password);
            if (!createResult.Succeeded)
            {
                // Hataları toplamak ve istisna fırlatmak
                var errors = string.Join("; ", createResult.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Kullanıcı oluşturulamadı: {errors}");
            }



            // 6. İlgili olayları tetiklemek (Opsiyonel)
            var registrationEvent = new UserCreatedEvent(newUser.Id, newUser.Email);
            await _eventDispatcher.Publish(this, registrationEvent);

            // 7. JWT Token Oluşturma
            var token = _jwtTokenService.GenerateToken(newUser);

            return token;
        }



    }
}
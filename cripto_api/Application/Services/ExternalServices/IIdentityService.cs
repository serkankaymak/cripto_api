using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ExternalServices;

public interface IIdentityService : IExternalService
{
    /// <summary>
    /// Login işlemi...
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns>Geriye claimsleri tutan jwt token döndürür.</returns>
    Task<string> AuthenticateAsync(string email, string password);
    /// <summary>
    /// mobile notification token güncelleme...
    /// </summary>
    /// <param name="email"></param>
    /// <param name="token"></param>
    /// <returns>void</returns>
    Task UpdateMobilePushNotificationTokenOfUser(string email, string token);
}
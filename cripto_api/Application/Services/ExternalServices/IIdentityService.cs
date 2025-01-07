using Domain.Domains.IdentityDomain.Entities;

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

    Task<string> RegisterAsync(string email, string password);
    /// <summary>
    /// mobile notification token güncelleme...
    /// </summary>
    /// <param name="email"></param>
    /// <param name="token"></param>
    /// <returns>void</returns>
    /// 
    Task UpdateMobilePushNotificationTokenOfUser(string email, string token);
    /// <summary>
    /// user ın topic e bağlı notification izni var ise , token ve mobile tipi ni döndürür.
    /// </summary>
    /// <param name="email"></param>
    /// <param name="token"></param>
    Task<List<UserIdentity>> GetPushNotificationTokensInUserPermittedTopic(PushNotifcationTopics topic);
}
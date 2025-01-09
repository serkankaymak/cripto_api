using Application.Dtos;
using Domain.Domains.IdentityDomain.Entities;

namespace Application.Services.ExternalServices;

public interface INotificationService : IExternalService
{
    Task sendEmailAsync(string toEmailAdress, string subject, string htmlMessage, Action? onSuccess = null);
    Task SendPushNotification(string deviceToken, MobileClientType clientType, string title, string body, object? data = null);
    Task SendPushNotification(PushNotifcationTopics topic, string title, string body, object? data = null);
   
}


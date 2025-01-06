using Domain.Domains.IdentityDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.InternalServices.MobilePushNotificationService;




public interface IMobilePushNotificationService
{
    public Task SendPushNotificationAsync(string deviceToken, string title, string body, object? data = null);
    public Task SendPushNotificationAsync(PushNotifcationTopics topic, string title, string body, object? data = null);
    Task SubscribeTokenToTopicAsync(string topic, string deviceToken);
    Task UnSubscribeTokenToTopicAsync(string topic, string deviceToken);
}


public interface IAndroidPushNotificationService : IMobilePushNotificationService
{ }
public interface IIosPushNotificationService : IMobilePushNotificationService
{ }
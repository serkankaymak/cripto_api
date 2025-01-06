using Domain.Domains.IdentityDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.InternalServices.MobilePushNotificationService;

public class IosMobilePushNotificationService : IIosPushNotificationService
{
    public Task SendPushNotificationAsync(string deviceToken, string title, string body, object? data = null)
    {
        throw new NotImplementedException();
    }

    public Task SendPushNotificationAsync(PushNotifcationTopics topic, string title, string body, object? data = null)
    {
        throw new NotImplementedException();
    }

    public Task SubscribeTokenToTopicAsync(string topic, string deviceToken)
    {
        throw new NotImplementedException();
    }

    public Task UnSubscribeTokenToTopicAsync(string topic, string deviceToken)
    {
        throw new NotImplementedException();
    }
}

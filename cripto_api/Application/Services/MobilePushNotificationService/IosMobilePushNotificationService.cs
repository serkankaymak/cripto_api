using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.MobilePushNotificationService;

public class IosMobilePushNotificationService : IMobilePushNotificationService
{
    public Task SendPushNotification(string deviceToken, string title, string body, object? data = null)
    {
        throw new NotImplementedException();
    }

    public Task SendPushNotification(MobilePushNotificationTopics topic, string title, string body, object? data = null)
    {
        throw new NotImplementedException();
    }

    public Task SubscribeTokenToTopicAsync(string serviceAccountJson, string topic, string deviceToken)
    {
        throw new NotImplementedException();
    }

    public Task UnSubscribeTokenToTopicAsync(string serviceAccountJson, string topic, string deviceToken)
    {
        throw new NotImplementedException();
    }
}

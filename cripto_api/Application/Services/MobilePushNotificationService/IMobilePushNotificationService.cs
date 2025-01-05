using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.MobilePushNotificationService;

public enum MobilePushNotificationTopics
{
    general_topic = 0,
    new_analyses_received_topic = 1,
}


public interface IMobilePushNotificationService
{
    public Task SendPushNotification(string deviceToken, string title, string body, object? data = null);
    public Task SendPushNotification(MobilePushNotificationTopics topic, string title, string body, object? data = null);
    Task SubscribeTokenToTopicAsync(string serviceAccountJson, string topic, string deviceToken);
    Task UnSubscribeTokenToTopicAsync(string serviceAccountJson, string topic, string deviceToken);
}

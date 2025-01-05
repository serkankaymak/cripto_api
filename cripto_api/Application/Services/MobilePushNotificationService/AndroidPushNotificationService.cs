using Application.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Services.MobilePushNotificationService;

public class AndroidPushNotificationService : IMobilePushNotificationService
{
    readonly string firebasePushNotificationConfig;

    public AndroidPushNotificationService(IOptions<FirebasePushNotificationConfig> firebasePushNotificationConfig)
    {
        this.firebasePushNotificationConfig = JsonSerializer.Serialize(firebasePushNotificationConfig.Value);
    }


    public async Task SendPushNotification(string deviceToken, string title, string body, object? data = null)
    {
        await FirebasePushNotifiactionSender.SendPushViaHttpV1Async(firebasePushNotificationConfig, deviceToken, title, body, data);
    }

    public async Task SendPushNotification(MobilePushNotificationTopics topic, string title, string body, object? data = null)
    {
        await FirebasePushNotifiactionSender.SendPushToTopicAsync(firebasePushNotificationConfig, topic.ToString(), title, body, data);
    }

    public async Task SubscribeTokenToTopicAsync(string serviceAccountJson, string topic, string deviceToken)
    {
        await FirebasePushNotifiactionSender.SubscribeTokensToTopicAsync(firebasePushNotificationConfig, topic, new List<string> { deviceToken });
    }

    public async Task UnSubscribeTokenToTopicAsync(string serviceAccountJson, string topic, string deviceToken)
    {
        await FirebasePushNotifiactionSender.UnSubscribeTokenToTopicAsync(firebasePushNotificationConfig, topic, deviceToken);
    }
}

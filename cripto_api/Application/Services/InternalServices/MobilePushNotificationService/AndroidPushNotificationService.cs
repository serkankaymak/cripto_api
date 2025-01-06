using Application.Settings;
using Domain.Domains.IdentityDomain.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Services.InternalServices.MobilePushNotificationService;

public class AndroidPushNotificationService : IAndroidPushNotificationService
{
    readonly string firebasePushNotificationConfig;

    public AndroidPushNotificationService(IOptions<FirebasePushNotificationConfig> firebasePushNotificationConfig)
    {
        this.firebasePushNotificationConfig = JsonSerializer.Serialize(firebasePushNotificationConfig.Value);
    }


    public async Task SendPushNotificationAsync(string deviceToken, string title, string body, object? data = null)
    {
        await FirebasePushNotifiactionSender.SendPushViaHttpV1Async(firebasePushNotificationConfig, deviceToken, title, body, data);
    }

    public async Task SendPushNotificationAsync(PushNotifcationTopics topic, string title, string body, object? data = null)
    {
        await FirebasePushNotifiactionSender.SendPushToTopicAsync(firebasePushNotificationConfig, topic.ToString(), title, body, data);
    }

    public async Task SubscribeTokenToTopicAsync(string topic, string deviceToken)
    {
        await FirebasePushNotifiactionSender.SubscribeTokensToTopicAsync(firebasePushNotificationConfig, topic, new List<string> { deviceToken });
    }

    public async Task UnSubscribeTokenToTopicAsync(string topic, string deviceToken)
    {
        await FirebasePushNotifiactionSender.UnSubscribeTokenToTopicAsync(firebasePushNotificationConfig, topic, deviceToken);
    }
}

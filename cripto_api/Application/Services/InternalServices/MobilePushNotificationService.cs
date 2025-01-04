using Application.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Services.InternalServices;

public interface IMobilePushNotificationService
{
    public Task SendPushNotification(string deviceToken, string title, string body, object? data = null);
}


public class MobilePushNotificationService : IMobilePushNotificationService
{
    readonly FirebasePushNotificationConfig firebasePushNotificationConfig;

    public MobilePushNotificationService(IOptions<FirebasePushNotificationConfig> firebasePushNotificationConfig)
    {
        this.firebasePushNotificationConfig = firebasePushNotificationConfig.Value;
    }


    public async Task SendPushNotification(string deviceToken, string title, string body, object? data = null)
    {
        await FirebasePushNotifiactionSender.SendPushViaHttpV1Async(JsonSerializer.Serialize(firebasePushNotificationConfig), deviceToken, title, body, data);
    }

}

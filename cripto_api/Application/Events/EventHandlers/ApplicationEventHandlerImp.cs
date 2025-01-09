using Application.Services.ExternalServices;
using Application.Services.InternalServices.MobilePushNotificationService;
using Domain.Domains.IdentityDomain.Entities;
using Shared.ApiResponse;
using Shared.Events;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Events.EventHandlers;

public class ApplicationEventHandlerImp : IEventHandler<TickerFetchFailedEvent>, IEventHandler<UserMobileNotificationTokenUpdatedEvent>
{
    IIdentityService identityService;
    INotificationService notificationService;
    IAndroidPushNotificationService androidPushNotificationService;
    IIosPushNotificationService iosPushNotificationService;

    public ApplicationEventHandlerImp(INotificationService notificationService, IAndroidPushNotificationService androidPushNotificationService, IIosPushNotificationService iosPushNotificationService, IIdentityService identityService)
    {
        this.notificationService = notificationService;
        this.androidPushNotificationService = androidPushNotificationService;
        this.iosPushNotificationService = iosPushNotificationService;
        this.identityService = identityService;
    }



    public Task HandleAsync(TickerFetchFailedEvent @event)
    {
        _ = notificationService.sendEmailAsync("kaymak__serkan35@hotmail.com", "Cripto", "New cripto tickers fetching failed", null);
        return Task.CompletedTask;
    }


    public Task HandleAsync(UserMobileNotificationTokenUpdatedEvent @event)
    {
        if (@event.MobileClientType == null) return Task.CompletedTask;
        IMobilePushNotificationService mobilePushNotificationService;
        if (@event.MobileClientType == MobileClientType.Android) mobilePushNotificationService = androidPushNotificationService;
        else if (@event.MobileClientType == MobileClientType.Ios) mobilePushNotificationService = iosPushNotificationService;
        else throw ExceptionFactory.InternalServerError();
        foreach (PushNotifcationTopics topic in EnumExtension.GetValues<PushNotifcationTopics>())
        {
            if (@event.Preferences.Any(x => x.PreferenceType.ToLower() == topic.ToString().ToLower()))
            {
                if (@event.MobileNotificationTokenOld != null) _ = mobilePushNotificationService.UnSubscribeTokenToTopicAsync(topic.ToString(), @event.MobileNotificationTokenOld);
                _ = mobilePushNotificationService.SubscribeTokenToTopicAsync(topic.ToString(), @event.MobileNotificationTokenNew);
            }
        }

        return Task.CompletedTask;
    }

}

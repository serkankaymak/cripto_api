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

public class ApplicationEventHandlerImp : IEventHandler<TickerFetchFailedEvent>, IEventHandler<TickersFetchedEvent>, IEventHandler<UserMobileNotificationTokenUpdatedEvent>
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



    public Task Handle(TickerFetchFailedEvent @event)
    {
        _ = notificationService.sendEmailAsync("kaymak.serkan35@gmail.com", "Cripto", "New cripto tickers fetching failed", null);
        return Task.CompletedTask;
    }

    Task IEventHandler<TickersFetchedEvent>.Handle(TickersFetchedEvent @event)
    {
        //var users = await identityService.GetPushNotificationTokensInUserPermittedTopic(PushNotifyTopics.notify_when_criptos_analysed);
        //var tokens = users.Where(x => x.MobileClientType == MobileClientType.Android && x.PushNotificationToken != null).Select(x => x.PushNotificationToken);
        _ = androidPushNotificationService.SendPushNotificationAsync(PushNotifcationTopics.notify_when_criptos_analysed, "", "", null);
        _ = iosPushNotificationService.SendPushNotificationAsync(PushNotifcationTopics.notify_when_criptos_analysed, "", "", null);
        return Task.CompletedTask;
    }

    public Task Handle(UserMobileNotificationTokenUpdatedEvent @event)
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

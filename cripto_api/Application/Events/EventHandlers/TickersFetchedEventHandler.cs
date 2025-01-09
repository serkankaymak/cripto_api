using Application.Dtos;
using Application.Services.ExternalServices;
using Application.Services.InternalServices.MobilePushNotificationService;
using Domain.Domains.ChyriptoDomain.CryptoTechnicalAnalyses;
using Domain.Domains.IdentityDomain.Entities;
using Shared.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Events.EventHandlers;

interface ITickersFetchedMessageStrategy
{
    string getMessage(List<CryproAnalysesDto> cryproAnalysesDto);
}

class TickersFetchedMessageStrategy : ITickersFetchedMessageStrategy
{
    public string getMessage(List<CryproAnalysesDto> cryproAnalysesDto)
    {
        throw new NotImplementedException();
    }
}



public class TickersFetchedEventHandler : IEventHandler<TickersFetchedEvent>
{
    ICriptoService criptoService;
    IIdentityService identityService;
    INotificationService notificationService;

    public TickersFetchedEventHandler(IIdentityService identityService, INotificationService notificationService, IAndroidPushNotificationService androidPushNotificationService, IIosPushNotificationService iosPushNotificationService, ICriptoService criptoService)
    {
        this.identityService = identityService;
        this.notificationService = notificationService;
        this.criptoService = criptoService;
    }




    public async Task HandleAsync(TickersFetchedEvent @event)
    {
        var analyses = await criptoService.GetCryptosTechnicalAnalyses();
        ITickersFetchedMessageStrategy messageStrategy = new TickersFetchedMessageStrategy();
        var message = messageStrategy.getMessage(analyses);
        _ = notificationService.SendPushNotification(PushNotifcationTopics.notify_when_criptos_analysed, "", message, analyses);
      
    }
}

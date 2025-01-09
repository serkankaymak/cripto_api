
using Application.Dtos;
using Application.Events;
using Application.Mapping;
using Application.Services.ExternalServices;
using Application.Services.InternalServices.EmailService;
using Application.Services.InternalServices.MobilePushNotificationService;
using Domain.Domains.IdentityDomain.Entities;
using Microsoft.AspNetCore.SignalR;

using Shared.EmailSender;
using Shared.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infastructure.Infastructue.Services;

public class NotificationService : INotificationService, IEmailService
{
    IEmailService _emailService;
    IAndroidPushNotificationService _android;
    IIosPushNotificationService _ios;


    public NotificationService(EmailService emailService, IAndroidPushNotificationService mobilePushNotificationService, IIosPushNotificationService ios)
    {
        _emailService = emailService;
        _android = mobilePushNotificationService;
        _ios = ios;
    }

    public Task sendEmailAsync(string emailAdress, string subject, string htmlMessage, Action? onSuccess = null)
    {
        return _emailService.sendEmailAsync(emailAdress, subject, htmlMessage, onSuccess);
    }

    public Task SendPushNotification(string deviceToken, MobileClientType clientType, string title, string body, object? data = null)
    {
        if (clientType == MobileClientType.Android) return _android.SendPushNotificationAsync(deviceToken, title, body, data);
        return _ios.SendPushNotificationAsync(deviceToken, title, body, data);
    }

    public Task SendPushNotification(PushNotifcationTopics topic, string title, string body, object? data = null)
    {
        _ = _android.SendPushNotificationAsync(topic, title, body, data);
        _ = _ios.SendPushNotificationAsync(topic, title, body, data);
        return Task.CompletedTask;
    }


}


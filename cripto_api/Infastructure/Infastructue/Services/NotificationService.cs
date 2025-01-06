
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
    IMobilePushNotificationService _mobilePushNotificationService;

    public NotificationService(EmailService emailService, IMobilePushNotificationService mobilePushNotificationService)
    {
        _emailService = emailService;
        _mobilePushNotificationService = mobilePushNotificationService;
    }

    public Task sendEmailAsync(string emailAdress, string subject, string htmlMessage, Action? onSuccess = null)
    {
        return _emailService.sendEmailAsync(emailAdress, subject, htmlMessage, onSuccess);
    }

    public Task SendPushNotification(string deviceToken, string title, string body, object? data = null)
    {
        return _mobilePushNotificationService.SendPushNotificationAsync(deviceToken, title, body, data);
    }

    public Task SendPushNotification(PushNotifcationTopics topic, string title, string body, object? data = null)
    {
        return _mobilePushNotificationService.SendPushNotificationAsync(topic, title, body, data);
    }


}


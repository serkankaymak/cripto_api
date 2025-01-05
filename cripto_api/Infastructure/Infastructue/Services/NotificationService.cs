
using Application.Dtos;
using Application.Events;
using Application.Mapping;
using Application.Services.ExternalServices;
using Application.Services.InternalServices.EmailService;
using Application.Services.MobilePushNotificationService;
using Microsoft.AspNetCore.SignalR;

using Shared.EmailSender;
using Shared.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infastructure.Infastructue.Services;

public class NotificationService : INotificationService
{
    IEmailService _emailService;
    IMobilePushNotificationService _mobilePushNotificationService;

    public NotificationService(IEmailService emailService, IMobilePushNotificationService mobilePushNotificationService)
    {
        _emailService = emailService;
        _mobilePushNotificationService = mobilePushNotificationService;
    }
}


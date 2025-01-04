using api.EmailSender;
using Application.Settings;
using Microsoft.Extensions.Options;
using Shared.ApiResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.InternalServices.EmailService;

public interface IEmailService
{
    public Task sendEmailAsync(string emailAdress, string subject, string htmlMessage, Action? onSuccess = null);
}


public class EmailService : IEmailService
{
    EmailConfig _emailConfiguration;
    EmailSenderImpl emailSender;

    public EmailService(IOptions<EmailConfig> emailConfiguration)
    {
        this._emailConfiguration = emailConfiguration.Value;
        emailSender = new EmailSenderImpl();
        emailSender.port = emailConfiguration.Value.port;
        emailSender.host = emailConfiguration.Value.host;
        emailSender.password = emailConfiguration.Value.password;
        emailSender.enableSSL = emailConfiguration.Value.enableSSL;
        emailSender.userName = emailConfiguration.Value.userName;
    }

    public async Task sendEmailAsync(string emailAdress, string subject, string htmlMessage, Action? onSuccess = null)
    {
        try
        {
            await emailSender.SendEmailAsync(emailAdress, subject, htmlMessage);
            if (onSuccess != null) { onSuccess.Invoke(); }
        }
        catch (Exception)
        {
            // hatayı üst katmana iletelim..
            throw ExceptionFactory.InternalServerError();

        }
    }
}
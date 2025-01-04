
using SendGrid;
using SendGrid.Helpers.Mail;
using Shared.EmailSender;
using System.Net;
using System.Net.Mail;


namespace api.EmailSender;

public class EmailSenderImpl : AEmailSender
{

    public override Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var client = new SmtpClient(host, port);
        client.Credentials = new NetworkCredential(userName, password);
        client.EnableSsl = this.enableSSL;
        return client.SendMailAsync(new MailMessage(userName, email, subject, htmlMessage)
        {
            IsBodyHtml = true
        });
    }
}
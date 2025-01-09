
using SendGrid;
using SendGrid.Helpers.Mail;
using Shared.EmailSender;
using System.Net;
using System.Net.Mail;


namespace api.EmailSender;

public class EmailSenderImpl : AEmailSender
{

    public override async Task SendEmailAsync(string toEmail, string subject, string htmlMessage)
    {
        try
        {
            using (var client = new SmtpClient(host, port))
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(userName, password);
                client.EnableSsl = this.enableSSL;

                using (var mailMessage = new MailMessage(fromEmail, toEmail, subject, htmlMessage)
                {
                    IsBodyHtml = false
                })
                {
                    await client.SendMailAsync(mailMessage);
                }
            }
        }
        catch (SmtpException smtpEx)
        {
            // SMTP ile ilgili hataları burada ele alın
            // Örneğin, loglama yapabilirsiniz
            // LogError(smtpEx);
            throw; // Hatanın üst katmana iletilmesini sağlamak için yeniden fırlatabilirsiniz
        }
        catch (Exception ex)
        {
            // Diğer genel hataları burada ele alın
            // LogError(ex);
            throw;
        }
    }

}
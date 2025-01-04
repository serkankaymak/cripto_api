namespace Shared.EmailSender;

public abstract class AEmailSender : IEmailSender
{
    public string host { get; set; }
    public int port { get; set; }
    public bool enableSSL { get; set; }
    public string userName { get; set; }
    public string password { get; set; }

    public abstract Task SendEmailAsync(string email, string subject, string htmlMessage);
}


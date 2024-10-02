using EmailSenderApp.Models;

public interface IEmailService
{
    void SendEmail(EmailRequest request);
}

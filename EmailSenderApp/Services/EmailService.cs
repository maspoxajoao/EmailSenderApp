using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
using EmailSenderApp.Models;

namespace EmailSenderApp.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Implementação correta com o EmailRequest
        public void SendEmail(EmailRequest request)
        {
            var emailSettings = _configuration.GetSection("EmailSettings");

            var fromName = emailSettings["FromName"];
            var fromAddress = emailSettings["FromAddress"];
            var smtpServer = emailSettings["SmtpServer"];
            var smtpPort = int.Parse(emailSettings["SmtpPort"]);
            var smtpPassword = emailSettings["SmtpPassword"];
            var useSSL = bool.Parse(emailSettings["UseSSL"]);

            foreach (var email in request.To)
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(fromName, fromAddress));
                message.To.Add(new MailboxAddress("", email));
                message.Subject = request.Subject;
                message.Body = new TextPart("plain")
                {
                    Text = request.Body
                };

                using (var client = new SmtpClient())
                {
                    client.Connect(smtpServer, smtpPort, useSSL);
                    client.Authenticate(fromAddress, smtpPassword);
                    client.Send(message);
                    client.Disconnect(true);
                }
            }
        }
    }
}

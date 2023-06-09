using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;
using PuckDrop.Core.Settings;
using Microsoft.Extensions.Options;

namespace PuckDrop.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly string _emailSender;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(emailSettings.Value.EmailSender, emailSettings.Value.EmailPassword),
                EnableSsl = true,
            };

            _emailSender = emailSettings.Value.EmailSender;
        }

        public async Task SendErrorEmail(string errorDetails)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSender),
                Subject = "NHL Reminder Error Notification",
                Body = errorDetails,
                IsBodyHtml = true
            };

            mailMessage.To.Add(_emailSender); 

            await _smtpClient.SendMailAsync(mailMessage);
        }
    }
}

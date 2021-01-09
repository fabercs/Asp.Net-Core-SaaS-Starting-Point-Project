using EMSApp.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace EMSApp.Infrastructure
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var client = new SendGridClient(_configuration.GetSection("SendGridSettings")["ApiKey"]);
                var from = new EmailAddress("info@exms.io");
                var to = new EmailAddress(email);
                var msg = MailHelper.CreateSingleEmail(from, to, subject, null, null);
                msg.HtmlContent = message;
                await client.SendEmailAsync(msg);
            }
            catch
            {
                throw;
            }
            
        }
    }
}

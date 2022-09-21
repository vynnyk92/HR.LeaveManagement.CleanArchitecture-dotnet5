using HR.LeaveManagement.Application.Infrastructure.Contracts;
using HR.LeaveManagement.Application.Models;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace HR.LEaveManagement.Infrastructure.Email
{
    public class EmailSender : IEmailSender
    { 
        private readonly EmailSettings _settings;

        public EmailSender(IOptions<EmailSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<bool> SendEmail(LeaveManagement.Application.Models.Email email)
        {
            var client = new SendGridClient(_settings.ApiKey);
            var to = new EmailAddress(email.To);
            var from = new EmailAddress(_settings.FromAddress, _settings.FromName);

            var message = MailHelper.CreateSingleEmail(to, from, email.Subject, email.Body, null);

            var res = await client.SendEmailAsync(message);
            return res.IsSuccessStatusCode;
        }
    }
}

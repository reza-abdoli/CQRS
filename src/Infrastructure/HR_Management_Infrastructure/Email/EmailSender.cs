using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR_Management_Application.Contracts.Infrastructure;
using HR_Management_Application.Models;
using Microsoft.Extensions.Options;
using HR_Management_Application.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace HR_Management_Infrastructure.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSetting _emailSettings;
        public EmailSender(IOptions<EmailSetting> emailSetting)
        {
            _emailSettings = emailSetting.Value;
        }
        public async Task<bool> SendEmail(HR_Management_Application.Models.Email email)
        {
            var client = new SendGridClient(_emailSettings.ApiKey);
            var to = new EmailAddress(email.To);
            var from = new EmailAddress
            {
                Email = _emailSettings.FromAddress,
                Name = _emailSettings.FromName
            };
            var message = MailHelper.CreateSingleEmail(from, to, email.Subject, email.Body, email.Body);
            var response = await client.SendEmailAsync(message);
            return response.StatusCode == System.Net.HttpStatusCode.OK ||
                   response.StatusCode == System.Net.HttpStatusCode.Accepted;
        }
    }
}
using System;
using System.Threading.Tasks;
using api.Interfaces;
using api.Migrations;
using api.Models;
using FluentEmail.Core;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace api.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;

        private readonly IFluentEmail _singleEmail;

        public MailService(IOptions<MailSettings> mailSettings, IFluentEmail singleEmail)
        {
            _mailSettings = mailSettings.Value;
            _singleEmail = singleEmail;
        }

        public async Task SendEmail(MailRequest mailRequest)
        {
            Console.WriteLine(mailRequest.To);
            var model = new { Name = mailRequest.FullName, RandomString = mailRequest.RandomString };

            var email = _singleEmail
                .To(mailRequest.To)
                .Subject(mailRequest.Subject)
                .UsingTemplateFromFile(mailRequest.Template, model);
                // .UsingTemplate(template, model);

            await email.SendAsync();

            // var email = new MimeMessage();
            // email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            // email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            // email.Subject = mailRequest.Subject;
            // var builder = new BodyBuilder();
            // builder.HtmlBody = mailRequest.Body;
            // email.Body = builder.ToMessageBody();
            // using var smtp = new SmtpClient();
            // smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            // smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            // await smtp.SendAsync(email);
            // smtp.Disconnect(true);
        }
    }
}
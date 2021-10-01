using System;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using FluentEmail.Core;
using Microsoft.Extensions.Options;

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
            var model = new { Name = mailRequest.FullName, RandomString = mailRequest.RandomString, food = mailRequest?.Content?.food, drink = mailRequest?.Content?.drink, diet = mailRequest?.Content?.diet };

            var email = _singleEmail
                .To(mailRequest.To)
                .Subject(mailRequest.Subject)
                .UsingTemplateFromFile(mailRequest.Template, model);

            await email.SendAsync();
        }
    }
}
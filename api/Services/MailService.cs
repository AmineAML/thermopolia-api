using System;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using FluentEmail.Core;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace api.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;

        private readonly IFluentEmail _singleEmail;

        private string _thermopoliaUrl;

        private string _thermopoliaEmailConfirmationUrl;

        private string _thermopoliaUnsubscribeFromNewsletter;

        private string _thermopoliaEmail;

        public IConfiguration Configuration { get; }

        public MailService(IOptions<MailSettings> mailSettings, IFluentEmail singleEmail, IConfiguration configuration)
        {
            _mailSettings = mailSettings.Value;
            _singleEmail = singleEmail;
            Configuration = configuration;
            _thermopoliaUrl = Configuration.GetValue<string>("Thermopolia:URL");
            _thermopoliaEmailConfirmationUrl = Configuration.GetValue<string>("Thermopolia:ConfirmEmailUrl");
            _thermopoliaUnsubscribeFromNewsletter = Configuration.GetValue<string>("Thermopolia:UnsubscribeFromNewsletter");
            _thermopoliaEmail = configuration.GetValue<string>("SMTP:User");
        }

        public async Task SendEmail(MailRequest mailRequest)
        {
            Console.WriteLine(mailRequest.To);
            var model = new {
                Name = mailRequest.FullName,
                RandomString = mailRequest.RandomString,
                food = mailRequest?.Content?.food,
                drink = mailRequest?.Content?.drink,
                diet = mailRequest?.Content?.diet,
                thermopoliaUrl = _thermopoliaUrl,
                thermopoliaEmailConfirmationUrl = _thermopoliaEmailConfirmationUrl,
                thermopoliaNewsletterEmail = _thermopoliaEmail,
                // example for url to unsubscribe from the Thermopolia url
                thermopoliaUnsubscribeFromNewsletter = _thermopoliaUnsubscribeFromNewsletter + "/example"
            };

            var email = _singleEmail
                .To(mailRequest.To)
                .Subject(mailRequest.Subject)
                .UsingTemplateFromFile(mailRequest.Template, model);

            await email.SendAsync();
        }
    }
}
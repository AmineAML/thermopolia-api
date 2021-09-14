using System.Threading.Tasks;
using api.Models;
using Microsoft.Extensions.Options;

namespace api.Services
{
    public interface IMailService
    {
        Task SendEmail(MailRequest mailRequest);
    }
    public class MailService
    {
        private readonly MailSettings _mailSettings;

        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
    }
}
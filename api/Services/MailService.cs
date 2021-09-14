using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using Microsoft.Extensions.Options;

namespace api.Services
{
    public class MailService/* : IMailService*/
    {
        private readonly MailSettings _mailSettings;

        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
    }
}
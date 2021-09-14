using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IMailService
    {
        Task SendEmail(MailRequest mailRequest);
    }
}
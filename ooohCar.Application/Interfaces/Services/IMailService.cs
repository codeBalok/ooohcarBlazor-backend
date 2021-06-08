using ooohCar.Application.Requests.Mail;
using System.Threading.Tasks;

namespace ooohCar.Application.Interfaces.Services
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
    }
}
using System.Threading.Tasks;

namespace bitirme.webui.EmailService
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
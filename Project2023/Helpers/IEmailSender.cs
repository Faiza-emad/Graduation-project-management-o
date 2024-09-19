using Project2023.Models;
using Project2023.ViewModels;

namespace EmailService.Helpers.EmailHelper
{
    public interface IEmailSender

    {
       //Task SendEmail(string email,string subject , string body );
        Task SendFirstEmail(SendEmail emailModel);
    }
}

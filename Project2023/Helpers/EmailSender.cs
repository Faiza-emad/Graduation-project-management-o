using MimeKit;
using MailKit;
using MailKit.Net.Smtp;
using Project2023.Models;
using System.Text;
using Project2023.ViewModels;

namespace EmailService.Helpers.EmailHelper
{
    public class EmailSender : IEmailSender
    {
        string Server = "smtp.office365.com";
        int Port = 587;
        string SenderEmail = "GraduationManagement@outlook.com";
        string SenderName = "GraduationProjectManagement";
        string SenderPassword = "Project@2024";

        private async Task SendEmail(string email, string Subject, string Body)
        {
            try
            {
                var emailmasseg = new MimeMessage();
                emailmasseg.From.Add(new MailboxAddress(SenderName, SenderEmail));//الشخص الي طلع منو الايميل
                emailmasseg.To.Add(new MailboxAddress("", email));//الايميل الي واصليني
                emailmasseg.Subject = Subject;
                emailmasseg.Body = new TextPart("html") { Text = Body };
                var clint = new SmtpClient();//port بستقبل من خلالو الايميلات
                await clint.ConnectAsync(Server, Port, MailKit.Security.SecureSocketOptions.StartTls);//StatTls Secure of Part
                await clint.AuthenticateAsync(SenderEmail, SenderPassword);
                await clint.SendAsync(emailmasseg);
                await clint.DisconnectAsync(true);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task SendFirstEmail(SendEmail emailModel)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/EmailTemplate", "ManageProjectsSystem.html");
            var builder = new StringBuilder();
            using (StreamReader reader = File.OpenText(path))
            {
                builder.Append(reader.ReadToEnd());
            }
            string message = builder.ToString()
                .Replace("[EmailAddress]", emailModel.Email);
            await SendEmail(emailModel.Email, "Manage Projects System", message);
        }



    }
}





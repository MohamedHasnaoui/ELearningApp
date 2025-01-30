using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ELearningApp.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly string _emailSender = "helpdesk.elearningapp@gmail.com";
        private readonly string _password = "pdyh dpib wibh jfxd"; 

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                using (var mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress(_emailSender);
                    mailMessage.To.Add(email);
                    mailMessage.Subject = subject;
                    mailMessage.Body = htmlMessage;
                    mailMessage.IsBodyHtml = true;

                    using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtpClient.EnableSsl = true;
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = new NetworkCredential(_emailSender, _password);
                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                        await smtpClient.SendMailAsync(mailMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception (use a logging library like Serilog, NLog, etc.)
                Console.WriteLine($"Error sending email: {ex.Message}");
                throw; // Re-throw the exception if needed
            }
        }

    }
}

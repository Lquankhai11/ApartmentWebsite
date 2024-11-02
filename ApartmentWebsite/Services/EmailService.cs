namespace ApartmentWebsite.Services
{
    using MailKit.Net.Smtp;
    using MimeKit;
    using System.Threading.Tasks;
    using ApartmentWebsite.Models;
    using Microsoft.Extensions.Options;

    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
        Task SendOtpAsync(String to, string otp);
    }

    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings) 
        {
            _emailSettings = emailSettings.Value; 
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Apartment King",_emailSettings.From));
            emailMessage.To.Add(new MailboxAddress("",to));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("html") { Text = body };

            using (var client = new SmtpClient())
            {
                client.Connect(_emailSettings.SmtpServer, _emailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
                client.Authenticate(_emailSettings.Username, _emailSettings.Password);
                await client.SendAsync(emailMessage);
                client.Disconnect(true);
            }
        }
        public async Task SendOtpAsync(string to, string otp)
        {
            var subject = "Your OTP for Password Reset";
            var body = $"<p>Your OTP is: <strong>{otp}</strong></p>";
            await SendEmailAsync(to, subject, body);
        }
    }
}
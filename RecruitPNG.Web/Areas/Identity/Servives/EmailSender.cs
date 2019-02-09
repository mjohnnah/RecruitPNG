using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace RecruitPNG.Web.Areas.Identity.Services
{
    public class EmailSender : IEmailSender


    {
       
        public AppOptions Options { get; } //set only via Secret Manager
        public EmailSender(IOptions<AppOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        
        public Task SendEmailAsync(string email, string subject, string message)
        {
            SmtpClient client = new SmtpClient(Options.SmtpServer, Options.SmtpPort);
            client.UseDefaultCredentials = false;
            client.EnableSsl = Options.EnableSsl;
            client.Credentials = new NetworkCredential(Options.SmtpUsername, Options.SmtpPassword);

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(Options.SmtpFromEmail, Options.SmtpFromName);
            mailMessage.To.Add(email);
            mailMessage.Subject = subject;
            mailMessage.Body = message;
            mailMessage.IsBodyHtml = true;
            client.Send(mailMessage);

            return Task.CompletedTask;
        }

        
    }
}

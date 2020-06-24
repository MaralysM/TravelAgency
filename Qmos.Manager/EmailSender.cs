using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Qmos.Manager
{
    public class EmailSender : IEmailSender
    {
        public ILoggerErrorManager LoggerErrorManager { get; }
        public IConfiguration Configuration { get; }

        public EmailSender(ILoggerErrorManager loggerErrorManager, IConfiguration configuration)
        {
            LoggerErrorManager = loggerErrorManager;
            Configuration = configuration;
        }

        public Task SendEmailAsync(string email, string subject, string message, Stream rutaArchivo)
        {
            byte retryAttempts = 4;
            bool failed;
            do
            {
                failed = false;
                try
                {
                    SmtpClient Cliente = new SmtpClient(Configuration.GetSection("EmailSender:Smtp").Value, 587)
                    {
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(Configuration.GetSection("EmailSender:EmailFrom").Value, Configuration.GetSection("EmailSender:PasswordEmailFrom").Value),
                        EnableSsl = true
                    };

                    MailMessage mailMessage = new MailMessage
                    {
                        From = new MailAddress(Configuration.GetSection("EmailSender:EmailFrom").Value, Configuration.GetSection("EmailSender:DisplayName").Value)
                    };

                    mailMessage.To.Add(email);
                    mailMessage.Body = message;
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Subject = subject;
                    mailMessage.Priority = MailPriority.High;
                    if (rutaArchivo != null)
                    {
                        mailMessage.Attachments.Add(new Attachment(rutaArchivo, "reporte.pdf"));
                    }

                    Cliente.Send(mailMessage);

                }
                catch (Exception ex)
                {
                    failed = true;
                    retryAttempts--;
                    LoggerErrorManager.Error($"EMAIL | {ex.Message}. Retrying ({retryAttempts})...");
                }
            } while (failed && retryAttempts != 0);
            return Task.CompletedTask;
        }

        public Task SendEmailAsync(string email, string subject, string message, bool sendImages)
        {
            byte retryAttempts = 4;
            bool failed;
            do
            {
                failed = false;
                try
                {
                    SmtpClient Cliente = new SmtpClient(Configuration.GetSection("EmailSender:Smtp").Value, 587)
                    {
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(Configuration.GetSection("EmailSender:EmailFrom").Value, Configuration.GetSection("EmailSender:PasswordEmailFrom").Value),
                        EnableSsl = true
                    };

                    MailMessage mailMessage = new MailMessage
                    {
                        From = new MailAddress(Configuration.GetSection("EmailSender:EmailFrom").Value, Configuration.GetSection("EmailSender:DisplayName").Value)
                    };

                    mailMessage.To.Add(email);
                    mailMessage.Body = message;
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Subject = subject;
                    mailMessage.Priority = MailPriority.High;


                    Attachment a1 = new Attachment("wwwroot/images/Header.png");
                    a1.ContentId = "Header";
                    Attachment a2 = new Attachment("wwwroot/images/Icon.png");
                    a2.ContentId = "Icon";
                    Attachment a3 = new Attachment("wwwroot/images/Footer.png");
                    a3.ContentId = "Footer";
                    mailMessage.Attachments.Add(a1);
                    mailMessage.Attachments.Add(a2);
                    mailMessage.Attachments.Add(a3);

                    Cliente.Send(mailMessage);

                }
                catch (Exception ex)
                {
                    failed = true;
                    retryAttempts--;
                    LoggerErrorManager.Error($"EMAIL | {ex.Message}. Retrying ({retryAttempts})...");
                }
            } while (failed && retryAttempts != 0);
            return Task.CompletedTask;
        }
    }
}




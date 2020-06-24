using Qmos.Data;
using Qmos.Entities;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Qmos.Manager
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message, Stream RutaArchivo);
        Task SendEmailAsync(string email, string subject, string message, bool sendImages);
    }
}

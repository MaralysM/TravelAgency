using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Qmos.Entities;

namespace Qmos.Manager.Abstractions
{
    public interface IAccountManager
    {
        Task<bool> ForgotPasswordNotification( string email);
        string FindIdUser(string iduser);
    }
}

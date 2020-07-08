using Qmos.Data.Extensions;
using Qmos.Manager.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Qmos.Manager.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddManagerConnector(this IServiceCollection services)
        {
            services.AddDataConnector();
            services.AddSingleton<ILoggerErrorManager, LoggerErrorManager>();
            
            services.AddTransient<ILoggerActionsManager, LoggerActionsManager>();
            
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IRoleManager, RoleManager>();
            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<IElementManager, ElementManager>();

            services.AddTransient<IAccountManager, AccountManager>();
            services.AddTransient<IUpdateTimeManager, UpdateTimeManager>();

            return services;
        }
    }
}

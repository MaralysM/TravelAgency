using Qmos.Data.Repositories;
using Qmos.Data.Repositories.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Qmos.Data.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddDataConnector(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerErrorRepository, LoggerErrorRepository>();
            
            services.AddTransient<ILogger_actionsRepository, LoggerActionsRepository>();
            services.AddTransient<IUpdateTimeRepository, UpdateTimeRepository>();
            services.AddTransient<ITransitionParametersRepository, TransitionParametersRepository>();
            services.AddTransient<IReferenceParametersRepository, ReferenceParametersRepository>();
            
            return services;
        }
    }

}

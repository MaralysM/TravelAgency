using TravelAgency.Data.Extensions;
using TravelAgency.Manager.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace TravelAgency.Manager.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddManagerConnector(this IServiceCollection services)
        {
            services.AddDataConnector();
            services.AddTransient<IUserManager, UserManager>();  
            services.AddTransient<ITravellersManager, TravellersManager>();
            services.AddTransient<ITravelsManager, TravelsManager>();
            services.AddTransient<IRequestsManager, RequestsManager>();

            return services;
        }
    }
}

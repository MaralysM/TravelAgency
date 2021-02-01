using TravelAgency.Data.Repositories;
using TravelAgency.Data.Repositories.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace TravelAgency.Data.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddDataConnector(this IServiceCollection services)
        {
            services.AddTransient<ITravellersRepository, TravellersRepository>();
            services.AddTransient<ITravelsRepository, TravelsRepository>();
            services.AddTransient<IRequestsRepository, RequestsRepository>();
            
            return services;
        }
    }

}

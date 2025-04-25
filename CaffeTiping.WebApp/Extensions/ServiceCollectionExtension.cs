using CaffeTipping.DbDomain.Repositories;
using CaffeTipping.DbInfrastructure.Repositories;
using CaffeTipping.DbServices.Services;


namespace CaffeTiping.WebApp.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddDbServices(this IServiceCollection services)
    {
        services.AddTransient<IOrderTipRepository, OrderTipRepository>();
        services.AddTransient<IOrderTipService, OrderTipService>();
        services.AddTransient<IStatisticsRepository, StatisticsRepository>();
        services.AddTransient<IStatisticsService, StatisticsService>();
        
        return services;
    }
}
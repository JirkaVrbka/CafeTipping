using CaffeTipping.DbDomain.Repositories;
using CaffeTipping.DbInfrastructure.Repositories;
using CaffeTipping.DbServices.Services;
using CaffeTipping.FileServices;
using CaffeTipping.ServicesContract;


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
    
    public static IServiceCollection AddFileStorageServices(this IServiceCollection services)
    {
        services.AddTransient<IOrderTipService, FileOrderTipService>();
        services.AddTransient<IStatisticsService, FileStatisticsService>();
        
        return services;
    }
}
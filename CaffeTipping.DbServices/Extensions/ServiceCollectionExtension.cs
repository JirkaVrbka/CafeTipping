using CaffeTipping.DbInfrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CaffeTipping.DbServices.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDbContextFactory<CaffeTippingContext>(
            opt =>
            {
                opt.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
                opt.EnableSensitiveDataLogging();
                opt.EnableDetailedErrors();
            });
        
        return services;
    }
}
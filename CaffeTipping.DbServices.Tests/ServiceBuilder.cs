using CaffeTipping.DbDomain.Repositories;
using CaffeTipping.DbInfrastructure.Contexts;
using CaffeTipping.DbInfrastructure.Repositories;
using CaffeTipping.DbServices.Services;
using CaffeTipping.ServicesContract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CaffeTipping.DbServices.Tests;

public class ServiceBuilder
{
    public static ServiceProvider GetServiceProvider()
    {
        var services = new ServiceCollection();
        
        services.AddDbContextFactory<CaffeTippingContext>(
            opt =>
            {
                opt.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
                opt.EnableSensitiveDataLogging();
                opt.EnableDetailedErrors();
            });
        
        services.AddLogging(op =>
        {
            op.ClearProviders();
            op.AddConsole();
            op.SetMinimumLevel(LogLevel.Trace);
        });
        
        services.AddTransient<IOrderTipRepository, OrderTipRepository>();
        services.AddTransient<IOrderTipService, OrderTipService>();

        return services.BuildServiceProvider();
    }
}
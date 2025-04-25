using CaffeTiping.WebApp.Configuration;
using CaffeTipping.DbServices.Extensions;

namespace CaffeTiping.WebApp.Extensions;

public static class WebApplicationBuilderExtension
{
    public static WebApplicationBuilder AddStorageServices(this WebApplicationBuilder builder)
    {
        // Configuration
        builder
            .Configuration
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);
        StorageOptions storageOptions = new();
        builder
            .Configuration
            .GetSection(nameof(StorageOptions))
            .Bind(storageOptions);

        if (storageOptions.Storage == StorageOptionsValue.File)
        {
            // File storage
            builder
                .Services
                .AddFileStorageServices();
        }
        else
        {
            // Database
            builder
                .Services
                .AddDatabase();

            builder
                .Services
                .AddDbServices();
        }
        
        return builder;
    }
}
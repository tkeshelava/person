using Microsoft.Extensions.DependencyInjection;
using PersonManagement.Infrastructure.Storage;
using PersonManagment.Application.Abstractions;

namespace PersonManagement.Infrastructure;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddOptions<StorageOptions>().BindConfiguration(nameof(StorageOptions));
        services.AddScoped<IStorageClient, FileSystemStorageClient>();

        return services;
    }
}
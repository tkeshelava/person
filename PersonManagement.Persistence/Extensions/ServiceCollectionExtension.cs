using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonManagement.Persistence.Repositories;
using PersonManagment.Application.Abstractions;
using PersonManagment.Application.Abstractions.Repositories;

namespace PersonManagement.Persistence.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PersonDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("PersonDbConnection"))
        );
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
        services.AddTransient<IPersonRepository, PersonRepository>();

        return services;
    }
}
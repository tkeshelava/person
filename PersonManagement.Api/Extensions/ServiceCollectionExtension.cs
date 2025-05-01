using System.Reflection;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using PersonManagement.Api.Configurations;
using PersonManagement.Api.Configurations.Exceptions;
using PersonManagement.Api.Configurations.Swagger;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PersonManagement.Api.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddApiVersioningAndSwagger();

        services.AddProblemDetails();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddLocalization();

        return services;
    }

    public static IServiceCollection AddApiVersioningAndSwagger(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        // Add Swagger
        services.AddSwaggerGen();
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>>(provider =>
        {
            var apiVersionDescriptionProvider = provider.GetRequiredService<IApiVersionDescriptionProvider>();
            var entryAssemblyName = Assembly.GetEntryAssembly()?.GetName().Name ?? "API";
            return new ConfigureSwaggerOptions(apiVersionDescriptionProvider, entryAssemblyName);
        });

        return services;
    }
}
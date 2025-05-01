using System.Globalization;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using PersonManagement.Persistence;

namespace PersonManagement.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        PersonDbContext dbContext = scope.ServiceProvider.GetRequiredService<PersonDbContext>();
        dbContext.Database.Migrate();
    }

    public static void UseLocalization(this IApplicationBuilder app, IConfiguration configuration)
    {
        var supportedCultures = configuration
            .GetSection("Localization:SupportedCultures")
            .Get<string[]>()
            ?.Select(culture => new CultureInfo(culture))
            .ToArray();

        var defaultCulture = configuration["Localization:DefaultCulture"] ?? "ka";

        app.UseRequestLocalization(new RequestLocalizationOptions()
        {
            DefaultRequestCulture = new RequestCulture(defaultCulture),
            SupportedCultures = supportedCultures,
            SupportedUICultures = supportedCultures
        });
    }

    public static IApplicationBuilder UseVersionedSwagger(this IApplicationBuilder app)
    {
        var apiVersionDescriptionProvider =
            app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }
        });

        return app;
    }
}
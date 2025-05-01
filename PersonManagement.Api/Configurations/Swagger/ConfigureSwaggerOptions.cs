using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PersonManagement.Api.Configurations.Swagger;

public class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider, string entryAssemblyName)
    : IConfigureOptions<SwaggerGenOptions>
{
    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description, entryAssemblyName));
        }
    }

    private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description, string entryAssemblyName)
    {
        var openApiInfoTitle = string.Join(" ", entryAssemblyName.Split('.'));

        var info = new OpenApiInfo()
        {
            Title = openApiInfoTitle,
            Version = description.ApiVersion.ToString(),
            Description = $"Description for the example {openApiInfoTitle}"
        };

        if (description.IsDeprecated)
        {
            info.Description += " This API version has been deprecated.";
        }

        return info;
    }
}
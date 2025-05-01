using PersonManagement.Api.Configurations;
using PersonManagement.Api.Configurations.Swagger;
using PersonManagement.Api.Extensions;
using PersonManagement.Infrastructure;
using PersonManagement.Persistence;
using PersonManagement.Persistence.Extensions;
using PersonManagment.Application;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Host.UseSerilog();

builder.Services
    .AddApi()
    .AddApplication()
    .AddInfrastructure()
    .AddPersistence(builder.Configuration);

var app = builder.Build();

app.ApplyMigrations();
app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();
app.UseLocalization(builder.Configuration);
app.UseExceptionHandler();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseVersionedSwagger();
}

app.Run();
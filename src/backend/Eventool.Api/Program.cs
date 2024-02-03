using System.Text.Json;
using Eventool.Api.Apis;
using Eventool.Api.Middlewares;
using Eventool.Application;
using Eventool.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services
    .ConfigureHttpJsonOptions(options =>
    {
        options.SerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
        options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    })
    .AddInfrastructure(configuration)
    .AddApplication();

services.AddScoped<ValidationExceptionMiddleware>();

// App
var app = builder.Build();

app.UseMiddleware<ValidationExceptionMiddleware>();

app.MapGroup("/api")
    .MapOrganizerApi();

app.Run();
using Alicunde.System.Exam.Api.ApiDoc;
using Alicunde.System.Exam.Api.Client;
using Alicunde.System.Exam.Contracts;
using Alicunde.System.Exam.EntityFrameworkCore.DbContext;
using Alicunde.System.Exam.EntityFrameworkCore.Repositories;
using Alicunde.System.Exam.Services;
using Alicunde.System.Exam.Services.Helpers;
using Alicunde.System.Exam.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Refit;

namespace Alicunde.System.Exam.Api.Extensions;

public static class ApplicationConfigurationExtension
{
    public static void RegisterDataBaseContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AlicundeSystemExamDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
    }

    public static void RegisterHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddRefitClient<IOpenDataApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration["ApiUrls:OpenDataApiUrl"]));
    }
    
    public static void RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddTransient(typeof(IApiResponseManager<>), typeof(ApiResponseManager<>));
        services.AddScoped<IOpenDataApiService, OpenDataApiService>();
    }
    
    public static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    }

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1.0.0",
                    Title = "Alicunde System Exam API",
                    Description = "API to get information from https://api.opendata.esett.com/ and manage the bank in the local database.",
                    Contact = new OpenApiContact
                    {
                        Name = "Alvaro Luis Padilla Moya",
                        Email = "alpadilla2006@gmail.com"
                    }
                });
            
                c.DocumentFilter<CustomDocumentFilter>();
            }
        );
    }
    
    public static void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AlicundeSystemExamDbContext>();
        dbContext.Database.Migrate();
    }
}
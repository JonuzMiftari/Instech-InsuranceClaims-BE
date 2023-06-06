using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var configurationSection = configuration.GetSection("CosmosDb");
        string accountEndpoint = configurationSection.GetSection("Account").Value;
        string accountKey = configurationSection.GetSection("Key").Value;
        string databaseName = configurationSection.GetSection("DatabaseName").Value;

        services.AddDbContext<ClaimsDbContext>(options =>
        {
            options.UseCosmos(accountEndpoint, accountKey, databaseName);
        });

        services.AddScoped<IClaimsDbContext>(provider => provider.GetRequiredService<ClaimsDbContext>());

        services.AddScoped<ClaimsDbContextInitialiser>();

        services.AddScoped<IClaimsRepo, ClaimsRepo>();
        // services.AddScoped<IClaimsRepository, ClaimsRepository>();

        return services;
    }
}
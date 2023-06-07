using Application.Common.Interfaces;
using Application.Premiums;
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

        services.AddDbContext<AuditorDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                builder => builder.MigrationsAssembly(typeof(AuditorDbContext).Assembly.FullName)));

        services.AddScoped<ClaimsDbContextInitialiser>();
        
        services.AddSingleton<PremiumCalculator>();

        return services;
    }
}
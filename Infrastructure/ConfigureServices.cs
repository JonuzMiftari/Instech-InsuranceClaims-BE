using System.Reflection;
using Application.Common.Interfaces;
using Application.Premiums;
using Infrastructure.Messaging;
using Infrastructure.Messaging.Consumers;
using Infrastructure.Persistence;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, 
        IConfiguration configuration)
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

        services.AddScoped<IAuditorDbContext>(provider => provider.GetRequiredService<AuditorDbContext>());

        services.AddScoped<ClaimsDbContextInitialiser>();

        services.AddScoped<AuditorDbContextInitialiser>();

        services.AddSingleton<PremiumCalculator>();

        services.AddMassTransit(x =>
        {

            var assembly = Assembly.GetExecutingAssembly();

            x.AddConsumers(assembly);

            x.UsingInMemory((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
            });
        });

        services.AddScoped<IMessagePublisher, MessagePublisher>();

        return services;
    }
}
using ExchangeService.Application.Domains.Abstractions.Entities;
using ExchangeService.Application.Domains.Abstractions.Interfaces;
using ExchangeService.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExchangeService.Database;

public static class ServiceCollection
{
    public static void AddInfrastructureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ExchangeServiceContext>(options => options.UseNpgsql(configuration.GetConnectionString("DbConnection"), b=>b.MigrationsAssembly("ExchangeService")), ServiceLifetime.Transient);

        services.AddTransient<IRepository<ExchangeEntity>, ExchangesRepository>();
        services.AddTransient<IRepository<OperationEntity>, OperationsRepository>();
        services.AddTransient<IRepository<DirectionOperationEntity>, DirectionsOperationRepository>();
        services.AddTransient<IRepository<DirectionExchangeEntity>, DirectionsExchangeRepository>();
    }
}

using ExchangeService.Application.Domains.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExchangeService.Database;

public class ExchangeServiceContext : DbContext
{
    public DbSet<OperationEntity> Operations { get; set; }
    public DbSet<ExchangeEntity> Exchanges { get; set; }
    public DbSet<DirectionOperationEntity> DirectionsOperation { get; set; }
    public DbSet<DirectionExchangeEntity> DirectionsExchange { get; set; }

    static ExchangeServiceContext()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public ExchangeServiceContext(DbContextOptions<ExchangeServiceContext> options) : base(options)
    {
    }
}

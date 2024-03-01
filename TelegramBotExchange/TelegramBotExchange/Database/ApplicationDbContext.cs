using Microsoft.EntityFrameworkCore;
using TelegramBotExchange.Database.Models;

namespace TelegramBotExchange.Database;

public class ApplicationDbContext : DbContext
{
    public DbSet<OperationEntity> Operations { get; set; }
    public DbSet<ExchangeEntity> Exchanges { get; set; }
    public DbSet<DirectionExchangeEntity> DirectionsExchange { get; set; }
    public DbSet<DirectionOperationEntity> DirectionsOperation { get; set; }
    public DbSet<UserEntity> Users { get; set; }

    static ApplicationDbContext()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
}

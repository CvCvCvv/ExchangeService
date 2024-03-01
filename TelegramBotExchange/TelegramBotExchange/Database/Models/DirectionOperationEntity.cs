namespace TelegramBotExchange.Database.Models;

public class DirectionOperationEntity
{
    public Guid Id { get; set; }
    public Guid? ItemId { get; set; }
    public UserEntity User { get; set; } = null!;
    public int UserId { get; set; }
    public string? Name { get; set; } = null!;
    public Guid? DirectionExchangeId { get; set; }
}

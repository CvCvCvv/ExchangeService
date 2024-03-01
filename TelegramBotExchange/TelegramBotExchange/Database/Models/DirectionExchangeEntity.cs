namespace TelegramBotExchange.Database.Models;

public class DirectionExchangeEntity
{
    public Guid Id { get; set; }
    public Guid? ItemId { get; set; }
    public UserEntity User { get; set; } = null!;
    public int UserId { get; set; }
    public string? Name { get; set; }
    public DateTime? DateStart { get; set; }
}

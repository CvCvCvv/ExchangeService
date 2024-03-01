using TelegramBotExchange.Database.Models.Enums;

namespace TelegramBotExchange.Database.Models;

public class OperationEntity
{
    public Guid Id { get; set; }
    public Guid? ItemId { get; set; }
    public UserEntity User { get; set; } = null!;
    public int UserId { get; set; }
    public Guid? DirectionOperationId { get; set; }
    public Guid? ExchangeId { get; set; }
    public decimal? Sum { get; set; }
    public decimal? Volume { get; set; }
    public OperationType? OperationType { get; set; }
}

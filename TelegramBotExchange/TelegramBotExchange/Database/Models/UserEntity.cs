using TelegramBotExchange.Helpers.UserStates;

namespace TelegramBotExchange.Database.Models;

public class UserEntity
{
    public int Id { get; set; }
    public long ChatId { get; set; }
    public TypeOperationEnum TypeOperation { get; set; }
    public ListType ListType { get; set; }
    public int State { get; set; }
    public int Page { get; set; }
    public Guid ItemId { get; set; }
}

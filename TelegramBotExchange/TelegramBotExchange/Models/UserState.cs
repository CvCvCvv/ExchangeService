using TelegramBotExchange.Helpers.UserStates;

namespace TelegramBotExchange.Models
{
    public class UserState
    {
        public long ChatId { get; set; }
        public TypeOperationEnum TypeOperation { get; set; }
        public int State { get; set; }
    }
}

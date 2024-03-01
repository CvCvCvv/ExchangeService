using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBotExchange.Helpers.Telegram;

public static class ButtonsTelegram
{
    public static readonly InlineKeyboardMarkup MainMenu = new(new InlineKeyboardButton[][]
    {
        new []{ new InlineKeyboardButton("Обмен") { CallbackData="exchange"}, new InlineKeyboardButton("Направление обмена") { CallbackData= "directionExchange" } },
         new []{ new InlineKeyboardButton("Операции") { CallbackData="operation"}, new InlineKeyboardButton("Направление операциии") { CallbackData = "directionOperation" } }
    });

    public static readonly InlineKeyboardMarkup ExchangeMenu = new(new InlineKeyboardButton[][]
    {
        new []{ new InlineKeyboardButton("Просмотр истории") { CallbackData="exchangeHistory"} },
         new []{ new InlineKeyboardButton("Добавить новый") { CallbackData="exchangeAdd"} },
         new []{ new InlineKeyboardButton("В меню") { CallbackData="menu"}}
    });

    public static readonly InlineKeyboardMarkup OperationMenu = new(new InlineKeyboardButton[][]
   {
        new []{ new InlineKeyboardButton("Просмотр истории") { CallbackData="operationHistory"} },
         new []{ new InlineKeyboardButton("Добавить новый") { CallbackData="operationAdd"} },
         new []{ new InlineKeyboardButton("В меню") { CallbackData="menu"}}
   });

    public static readonly InlineKeyboardMarkup DirectionExchangeMenu = new(new InlineKeyboardButton[][]
   {
        new []{ new InlineKeyboardButton("Просмотр истории") { CallbackData="exchangeDirectionHistory"} },
         new []{ new InlineKeyboardButton("Добавить новый") { CallbackData= "exchangeDirectionAdd" } },
         new []{ new InlineKeyboardButton("В меню") { CallbackData="menu"}}
   });

    public static readonly InlineKeyboardMarkup DirectionOperationMenu =(new InlineKeyboardButton[][]
   {
        new []{ new InlineKeyboardButton("Просмотр истории") { CallbackData= "operationDirectionHistory" } },
         new []{ new InlineKeyboardButton("Добавить новый") { CallbackData= "operationDirectionAdd" } },
         new []{ new InlineKeyboardButton("В меню") { CallbackData="menu"}}
   });

    public static readonly InlineKeyboardMarkup YesNoMenu = new(new InlineKeyboardButton[][]
   {
        new []{ new InlineKeyboardButton("Да") { CallbackData= "yes" } },
         new []{ new InlineKeyboardButton("Нет") { CallbackData= "no" } }
   });

    public static readonly InlineKeyboardMarkup TypeOperationMenu = new(new InlineKeyboardButton[][]
  {
        new []{ new InlineKeyboardButton("Arrival") { CallbackData= "arrival" } },
         new []{ new InlineKeyboardButton("Expense") { CallbackData= "expense" } }
  });

    public static readonly InlineKeyboardMarkup MovePageMenu = new(new InlineKeyboardButton[][]
    {
        new []{ new InlineKeyboardButton("Назад") { CallbackData= "back" }, new InlineKeyboardButton("Вперёд") { CallbackData= "forward" } },
         new []{ new InlineKeyboardButton("В меню") { CallbackData="menu"}}
    });

    public static readonly InlineKeyboardMarkup MoveBackPageMenu = new(new InlineKeyboardButton[][]
    {
         new []{ new InlineKeyboardButton("Назад") { CallbackData= "back" } },
         new []{ new InlineKeyboardButton("В меню") { CallbackData="menu"}}
    });

    public static readonly InlineKeyboardMarkup MoveForwardPageMenu = new(new InlineKeyboardButton[][]
    {
         new []{ new InlineKeyboardButton("Вперёд") { CallbackData= "forward" } },
         new []{ new InlineKeyboardButton("В меню") { CallbackData="menu"}}
    });

    public static readonly InlineKeyboardMarkup MoveMenu = new(new InlineKeyboardButton[][]
     {
         new []{ new InlineKeyboardButton("В меню") { CallbackData="menu"}}
    });

    public static readonly InlineKeyboardMarkup EditMenu = new(new InlineKeyboardButton[][]
    {
        new []{ new InlineKeyboardButton("Удалить") { CallbackData="delete"},    new InlineKeyboardButton("Редактировать") { CallbackData="edit"}},
         new []{ new InlineKeyboardButton("В меню") { CallbackData="menu"}}
   });

    public static readonly InlineKeyboardMarkup DeleteAcceptedMenu = new(new InlineKeyboardButton[][]
   {
        new []{ new InlineKeyboardButton("Да") { CallbackData= "yesDeleted" }, new InlineKeyboardButton("Нет") { CallbackData= "noDeleted" } }
   });

    public static InlineKeyboardMarkup GetMoveButtons(int page, int count, List<InlineKeyboardButton> button, int skipElement = 5)
    {
        if (page == 0 && count == 0)
        {
            return MoveMenu;
        }
        else if (page == 0 && count < skipElement)
        {
            return new InlineKeyboardMarkup(new InlineKeyboardButton[][]
            {
                button.ToArray(),
                new []{ new InlineKeyboardButton("В меню") { CallbackData="menu"}}
            });
        }
        else if (page == 0)
        {
            return new InlineKeyboardMarkup(new InlineKeyboardButton[][]
            {
                button.ToArray(),
                new []{ new InlineKeyboardButton("Вперёд") { CallbackData= "forward" } },
                new []{ new InlineKeyboardButton("В меню") { CallbackData="menu"}}
            });
        }
        else if (page > 0 && count < skipElement)
        {
            return new InlineKeyboardMarkup(new InlineKeyboardButton[][]
            {
                button.ToArray(),
                new []{ new InlineKeyboardButton("Назад") { CallbackData= "back" } },
                new []{ new InlineKeyboardButton("В меню") { CallbackData="menu"}}
            });
        }
        else
        {
            return new InlineKeyboardMarkup(new InlineKeyboardButton[][]
            {
                button.ToArray(),
                new []{ new InlineKeyboardButton("Назад") { CallbackData= "back" }, new InlineKeyboardButton("Вперёд") { CallbackData= "forward" } },
                new []{ new InlineKeyboardButton("В меню") { CallbackData="menu"}}
            });
        }
    }
}

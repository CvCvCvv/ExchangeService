using ExchangeService.Client.Client;
using ExchangeService.Client.Common;
using ExchangeService.Client.Requests.Enums;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotExchange.Database.Models;
using TelegramBotExchange.Helpers.Telegram;
using TelegramBotExchange.Helpers.UserStates;

namespace TelegramBotExchange.Services;

public class ChatService : IChatService
{
    private readonly IStateMashineService _stateMashineService;
    private readonly IStateStoreService _stateStoreService;
    private readonly string _serviceUrl;
    private readonly int _skipElement;

    public ChatService(IStateMashineService mashineService, IStateStoreService stateStoreService, IConfiguration configuration)
    {
        _stateMashineService = mashineService;
        _stateStoreService = stateStoreService;

        _serviceUrl = configuration.GetValue<string>("ExchangeService:Url")!;
        _skipElement = configuration.GetValue<int>("ExchangeService:SkipElement")!;
    }

    public async Task GetCallbackMessage(ITelegramBotClient telegramBot, Update message)
    {
        var user = await _stateStoreService.GetUser(message.CallbackQuery!.Message!.Chat.Id);

        switch (message.CallbackQuery!.Data)
        {
            case "exchange":
                await telegramBot.EditMessageTextAsync(message.CallbackQuery.Message!.Chat.Id, message.CallbackQuery.Message.MessageId, "Что вы хотите сделать с данными обмена", replyMarkup: ButtonsTelegram.ExchangeMenu);
                break;
            case "operation":
                await telegramBot.EditMessageTextAsync(message.CallbackQuery.Message!.Chat.Id, message.CallbackQuery.Message.MessageId, "Что вы хотите сделать с данными операций", replyMarkup: ButtonsTelegram.OperationMenu);
                break;
            case "directionExchange":
                await telegramBot.EditMessageTextAsync(message.CallbackQuery.Message!.Chat.Id, message.CallbackQuery.Message.MessageId, "Что вы хотите сделать с данными направления обмена", replyMarkup: ButtonsTelegram.DirectionExchangeMenu);
                break;
            case "directionOperation":
                await telegramBot.EditMessageTextAsync(message.CallbackQuery.Message!.Chat.Id, message.CallbackQuery.Message.MessageId, "Что вы хотите сделать с данными направления операций", replyMarkup: ButtonsTelegram.DirectionOperationMenu);
                break;


            case "operationDirectionAdd":
                await InitializeAddDialogItem(telegramBot, message, TypeOperationEnum.AddDirectionOperation);
                break;
            case "exchangeDirectionAdd":
                await InitializeAddDialogItem(telegramBot, message, TypeOperationEnum.AddDirectionExchange);
                break;
            case "exchangeAdd":
                await InitializeAddDialogItem(telegramBot, message, TypeOperationEnum.AddExchange);
                break;
            case "operationAdd":
                await InitializeAddDialogItem(telegramBot, message, TypeOperationEnum.AddOperation);
                break;

            case "edit":
                await InitializeEditDialogItem(telegramBot, message, user.ListType, user);
                break;


            case "exchangeDirectionHistory":
                await _stateStoreService.ResetPage(message.CallbackQuery.Message!.Chat.Id);
                await ListShow(telegramBot, message, ListType.DirectionsExchange);
                break;
            case "operationDirectionHistory":
                await _stateStoreService.ResetPage(message.CallbackQuery.Message!.Chat.Id);
                await ListShow(telegramBot, message, ListType.DirectionsOperation);
                break;
            case "operationHistory":
                await _stateStoreService.ResetPage(message.CallbackQuery.Message!.Chat.Id);
                await ListShow(telegramBot, message, ListType.Operations);
                break;
            case "exchangeHistory":
                await _stateStoreService.ResetPage(message.CallbackQuery.Message!.Chat.Id);
                await ListShow(telegramBot, message, ListType.Exchanges);
                break;


            case "back":
                await _stateStoreService.MovePage(message.CallbackQuery!.Message!.Chat.Id, false);

                await ListShow(telegramBot, message, user.ListType);
                break;
            case "forward":
                await _stateStoreService.MovePage(message.CallbackQuery!.Message!.Chat.Id, true);

                await ListShow(telegramBot, message, user.ListType);
                break;

            case "delete":
                await telegramBot.EditMessageTextAsync(message.CallbackQuery.Message!.Chat.Id, message.CallbackQuery.Message.MessageId, "Вы действительно желаете удалить элемент?", replyMarkup: ButtonsTelegram.DeleteAcceptedMenu);
                break;



            case "yesDeleted":
                await DeleteItem(telegramBot, message, user);
                break;
            case "noDeleted":
                await ItemShow(telegramBot, message, user, false);
                break;



            case "yes":
                await telegramBot.EditMessageTextAsync(message.CallbackQuery.Message!.Chat.Id, message.CallbackQuery.Message.MessageId, "Дата начала  в формате ГГГГ-ММ-ДД:");
                await _stateStoreService.SaveExchangeData((long)message.CallbackQuery!.Message!.Chat!.Id, TypeOperationEnum.AddExchange, new ExchangeEntity() { Closed = message.CallbackQuery.Data == "yes" });
                await _stateStoreService.MoveState((long)message.CallbackQuery!.Message!.Chat!.Id, TypeOperationEnum.AddExchange);
                break;

            case "no":
                await telegramBot.EditMessageTextAsync(message.CallbackQuery.Message!.Chat.Id, message.CallbackQuery.Message.MessageId, "Дата начала  в формате ГГГГ-ММ-ДД:");
                await _stateStoreService.SaveExchangeData((long)message.CallbackQuery!.Message!.Chat!.Id, TypeOperationEnum.AddExchange, new ExchangeEntity() { Closed = message.CallbackQuery.Data == "yes" });
                await _stateStoreService.MoveState((long)message.CallbackQuery!.Message!.Chat!.Id, TypeOperationEnum.AddExchange);
                break;

            case "arrival":
                await _stateStoreService.SaveOperationData((long)message.CallbackQuery!.Message!.Chat!.Id, TypeOperationEnum.AddOperation, new OperationEntity() { OperationType = Database.Models.Enums.OperationType.Arrival });
                await SelectOperation(telegramBot, message);
                break;

            case "expense":
                await _stateStoreService.SaveOperationData((long)message.CallbackQuery!.Message!.Chat!.Id, TypeOperationEnum.AddOperation, new OperationEntity() { OperationType = Database.Models.Enums.OperationType.Expense });
                await SelectOperation(telegramBot, message);
                break;

            case "menu":
                await telegramBot.EditMessageTextAsync(message.CallbackQuery.Message!.Chat.Id, message.CallbackQuery.Message.MessageId, "Выберите действие:", replyMarkup: ButtonsTelegram.MainMenu);
                break;



            default:

                switch (user.TypeOperation)
                {
                    case TypeOperationEnum.AddExchange:
                        await UserSelectedExchange(telegramBot, message, user);
                        return;
                    case TypeOperationEnum.EditExchange:
                        return;
                    case TypeOperationEnum.AddOperation:
                        await UserSelectedOperation(telegramBot, message, user);
                        return;
                    case TypeOperationEnum.EditOperation:
                        return;
                    case TypeOperationEnum.AddDirectionOperation:
                        await UserSelectedDirectionOperation(telegramBot, message, user);
                        return;
                    case TypeOperationEnum.EditDirectiontOperation:
                        await UserSelectedDirectionOperation(telegramBot, message, user);
                        return;
                    default:
                        break;
                }

                if (user.ListType != ListType.None)
                {
                    await ItemShow(telegramBot, message, user);

                    return;
                }

                break;
        }
    }

    public async Task GetMessage(ITelegramBotClient telegramBot, Update message)
    {
        switch (message.Message!.Text)
        {
            case "/start":
                if (await _stateStoreService.GetUser(message.Message!.Chat.Id) != null)
                    await _stateStoreService.DeleteData(message.Message!.Chat.Id);
                await _stateStoreService.CreateState(message.Message!.Chat.Id, TypeOperationEnum.None);
                await telegramBot.SendTextMessageAsync(message.Message!.Chat.Id, "Добрый день! Выберите действие", replyMarkup: ButtonsTelegram.MainMenu);
                break;
            case "/menu":
                await telegramBot.SendTextMessageAsync(message.Message!.Chat.Id, "Выберите действие:", replyMarkup: ButtonsTelegram.MainMenu);
                break;
            default:
                var user = await _stateStoreService.GetUser(message.Message!.Chat.Id);

                if (user != null && message.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
                    _stateMashineService.MovingState(_stateStoreService, telegramBot, message);
                break;
        }

        await Task.CompletedTask;
    }

    #region [InitializeDialogs]

    private async Task InitializeAddDialogItem(ITelegramBotClient telegramBot, Update message, TypeOperationEnum typeOperation)
    {
        await _stateStoreService.ResetItemId(message.CallbackQuery!.Message!.Chat.Id, typeOperation);

        switch (typeOperation)
        {
            case TypeOperationEnum.AddExchange:
                await telegramBot.EditMessageTextAsync(message.CallbackQuery!.Message!.Chat.Id, message.CallbackQuery.Message.MessageId, "Введите имя поставщика:");
                await _stateStoreService.CreateState(message.CallbackQuery.Message!.Chat.Id, TypeOperationEnum.AddExchange);
                break;
            case TypeOperationEnum.AddOperation:
                await telegramBot.EditMessageTextAsync(message.CallbackQuery!.Message!.Chat.Id, message.CallbackQuery.Message.MessageId, "Введите сумму:");
                await _stateStoreService.CreateState(message.CallbackQuery.Message!.Chat.Id, TypeOperationEnum.AddOperation);
                break;
            case TypeOperationEnum.AddDirectionExchange:
                await telegramBot.EditMessageTextAsync(message.CallbackQuery!.Message!.Chat.Id, message.CallbackQuery.Message.MessageId, "Введите название:");
                await _stateStoreService.CreateState(message.CallbackQuery.Message!.Chat.Id, TypeOperationEnum.AddDirectionExchange);
                break;
            case TypeOperationEnum.AddDirectionOperation:
                await telegramBot.EditMessageTextAsync(message.CallbackQuery!.Message!.Chat.Id, message.CallbackQuery.Message.MessageId, "Введите название:");
                await _stateStoreService.CreateState(message.CallbackQuery.Message!.Chat.Id, TypeOperationEnum.AddDirectionOperation);
                break;
        }
    }

    private async Task InitializeEditDialogItem(ITelegramBotClient telegramBot, Update message, ListType listType, UserEntity user)
    {
        switch (listType)
        {
            case ListType.Exchanges:
                await _stateStoreService.SaveExchangeData(user.ChatId, TypeOperationEnum.AddExchange, new ExchangeEntity() { ItemId = user.ItemId });
                await telegramBot.EditMessageTextAsync(message.CallbackQuery!.Message!.Chat.Id, message.CallbackQuery.Message.MessageId, "Введите имя поставщика:");
                await _stateStoreService.CreateState(message.CallbackQuery.Message!.Chat.Id, TypeOperationEnum.AddExchange);
                break;
            case ListType.Operations:
                await _stateStoreService.SaveOperationData(user.ChatId, TypeOperationEnum.AddOperation, new OperationEntity() { ItemId = user.ItemId });
                await telegramBot.EditMessageTextAsync(message.CallbackQuery!.Message!.Chat.Id, message.CallbackQuery.Message.MessageId, "Введите сумму:");
                await _stateStoreService.CreateState(message.CallbackQuery.Message!.Chat.Id, TypeOperationEnum.AddOperation);
                break;
            case ListType.DirectionsExchange:
                await _stateStoreService.SaveDirectionExchangeData(user.ChatId, TypeOperationEnum.AddDirectionExchange, new DirectionExchangeEntity() { ItemId = user.ItemId });
                await telegramBot.EditMessageTextAsync(message.CallbackQuery!.Message!.Chat.Id, message.CallbackQuery.Message.MessageId, "Введите название:");
                await _stateStoreService.CreateState(message.CallbackQuery.Message!.Chat.Id, TypeOperationEnum.AddDirectionExchange);
                break;
            case ListType.DirectionsOperation:
                await _stateStoreService.SaveDirectionOperationData(user.ChatId, TypeOperationEnum.AddDirectionOperation, new DirectionOperationEntity() { ItemId = user.ItemId });
                await telegramBot.EditMessageTextAsync(message.CallbackQuery!.Message!.Chat.Id, message.CallbackQuery.Message.MessageId, "Введите название:");
                await _stateStoreService.CreateState(message.CallbackQuery.Message!.Chat.Id, TypeOperationEnum.AddDirectionOperation);
                break;
        }
    }

    #endregion

    #region [ShowListRegion]

    private async Task ShowListDirectionExchange(ITelegramBotClient telegramBot, Update message, int page)
    {
        var client = new ExchangeServiceClient(_serviceUrl);
        var result = await client.DirectionsExchange.GetDirectionsExchange(null, null);
        var otv = new StringBuilder("<i>Все направления обмена:</i>\n");

        if (result.Data == null)
        {
            await telegramBot.EditMessageTextAsync(message.CallbackQuery!.Message!.Chat.Id, message.CallbackQuery.Message.MessageId, "Выбор элемента или просмотр истории невозможен, список направлений обмена пуст", replyMarkup: ButtonsTelegram.GetMoveButtons(0, 0, new List<InlineKeyboardButton>()));
            return;
        }

        List<InlineKeyboardButton> buttons = new();

        var list = result.Data.Skip(page * _skipElement).Take(_skipElement).ToList();
        for (int i = 0; i < list.Count; i++)
        {
            otv.Append(MessageHelper.GetDirectionExchangeInfo(list[i]));
            buttons.Add(new InlineKeyboardButton((i + 1).ToString()) { CallbackData = list[i].Id.ToString() });
        }

        await telegramBot.EditMessageTextAsync(message.CallbackQuery!.Message!.Chat.Id, message.CallbackQuery.Message.MessageId, otv.ToString(), replyMarkup: ButtonsTelegram.GetMoveButtons(page, list.Count, buttons, _skipElement), parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
    }

    private async Task ShowListDirectionOperation(ITelegramBotClient telegramBot, Update message, int page)
    {
        var client = new ExchangeServiceClient(_serviceUrl);
        var result = await client.DirectionsOperation.GetDirectionsOperation(null, null);
        var otv = new StringBuilder("<i>Все направления операций:</i>\n");

        if (result.Data == null)
        {
            await telegramBot.EditMessageTextAsync(message.CallbackQuery!.Message!.Chat.Id, message.CallbackQuery.Message.MessageId, "Выбор элемента или просмотр истории невозможен, список направлений операций пуст", replyMarkup: ButtonsTelegram.GetMoveButtons(0, 0, new List<InlineKeyboardButton>()));
            return;
        }

        List<InlineKeyboardButton> buttons = new();

        var list = result.Data.Skip(page * _skipElement).Take(_skipElement).ToList();
        for (int i = 0; i < list.Count; i++)
        {
            otv.Append(MessageHelper.GetDirectionOperationInfo(list[i], (await client.DirectionsExchange.GetDirectionExchangeById(list[i].DirectionExchangeId)).Data!));
            buttons.Add(new InlineKeyboardButton((i + 1).ToString()) { CallbackData = list[i].Id.ToString() });
        }

        await telegramBot.EditMessageTextAsync(message.CallbackQuery!.Message!.Chat.Id, message.CallbackQuery.Message.MessageId, otv.ToString(), replyMarkup: ButtonsTelegram.GetMoveButtons(page, list.Count, buttons, _skipElement), parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
    }

    private async Task ShowListOperation(ITelegramBotClient telegramBot, Update message, int page)
    {
        var client = new ExchangeServiceClient(_serviceUrl);
        var result = await client.Operations.GetOperations(null, null, null, null, null);
        var otv = new StringBuilder("<i>Все операции:</i>\n");

        if (result.Data == null)
        {
            await telegramBot.EditMessageTextAsync(message.CallbackQuery!.Message!.Chat.Id, message.CallbackQuery.Message.MessageId, "Выбор элемента или просмотр истории невозможен, список операций пуст", replyMarkup: ButtonsTelegram.GetMoveButtons(0, 0, new List<InlineKeyboardButton>()));
            return;
        }

        List<InlineKeyboardButton> buttons = new();

        var list = result.Data.Skip(page * _skipElement).Take(_skipElement).ToList();
        for (int i = 0; i < list.Count; i++)
        {
            otv.Append(MessageHelper.GetOperationInfo(list[i], (await client.Exchanges.GetExchangeById(list[i].ExchangeId)).Data!,
                (await client.DirectionsOperation.GetDirectionOperationById(list[i].DirectionOperationId)).Data!));
            buttons.Add(new InlineKeyboardButton((i + 1).ToString()) { CallbackData = list[i].Id.ToString() });
        }

        await telegramBot.EditMessageTextAsync(message.CallbackQuery!.Message!.Chat.Id, message.CallbackQuery.Message.MessageId, otv.ToString(), replyMarkup: ButtonsTelegram.GetMoveButtons(page, list.Count, buttons, _skipElement), parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
    }

    private async Task ShowListExchange(ITelegramBotClient telegramBot, Update message, int page)
    {
        var client = new ExchangeServiceClient(_serviceUrl);
        var result = await client.Exchanges.GetExchanges(null, null, null, null, null, null);
        var otv = new StringBuilder($"<i>Все обмены:</i>\n");

        if (result.Data == null)
        {
            await telegramBot.EditMessageTextAsync(message.CallbackQuery!.Message!.Chat.Id, message.CallbackQuery.Message.MessageId, "Выбор элемента или просмотр истории невозможен, список обменов пуст", replyMarkup: ButtonsTelegram.GetMoveButtons(0, 0, new List<InlineKeyboardButton>()));
            return;
        }
        List<InlineKeyboardButton> buttons = new();

        var list = result.Data.Skip(page * _skipElement).Take(_skipElement).ToList();
        for (int i = 0; i < list.Count; i++)
        {
            otv.Append(MessageHelper.GetExchangeInfo(list[i], (await client.DirectionsExchange.GetDirectionExchangeById(list[i].DirectionExchangeId)).Data!));
            buttons.Add(new InlineKeyboardButton((i + 1).ToString()) { CallbackData = list[i].Id.ToString() });
        }

        await telegramBot.EditMessageTextAsync(message.CallbackQuery!.Message!.Chat.Id, message.CallbackQuery.Message.MessageId, otv.ToString(), replyMarkup: ButtonsTelegram.GetMoveButtons(page, list.Count, buttons, _skipElement), parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
    }

    private async Task ListShow(ITelegramBotClient telegramBot, Update message, ListType listType)
    {
        await _stateStoreService.SetTypeList(message.CallbackQuery!.Message!.Chat.Id, listType);
        var user = await _stateStoreService.GetUser(message.CallbackQuery!.Message!.Chat.Id);

        switch (listType)
        {
            case ListType.Exchanges:
                await ShowListExchange(telegramBot, message, user.Page);
                break;
            case ListType.Operations:
                await ShowListOperation(telegramBot, message, user.Page);
                break;
            case ListType.DirectionsExchange:
                await ShowListDirectionExchange(telegramBot, message, user.Page);
                break;
            case ListType.DirectionsOperation:
                await ShowListDirectionOperation(telegramBot, message, user.Page);
                break;
            default:
                break;
        }
    }

    #endregion

    #region [SelectedRegion]

    private async Task<bool> UserSelectedDirectionOperation(ITelegramBotClient telegramBot, Update message, UserEntity user)
    {
        await _stateStoreService.SaveDirectionOperationData(message.CallbackQuery!.Message!.Chat.Id, user.TypeOperation, new DirectionOperationEntity() { DirectionExchangeId = new Guid(message.CallbackQuery!.Data!) });

        var directionOperation = await _stateStoreService.GetDirectionOperationData(message.CallbackQuery!.Message!.Chat.Id);
        if (directionOperation == null)
            return false;

        var client = new ExchangeServiceClient(_serviceUrl);

        if (directionOperation.ItemId == null)
        {
            var result = await client.DirectionsOperation.CreateDirectionOperation(directionOperation.Name!, (Guid)directionOperation.DirectionExchangeId!);

            if (result.Success)
            {
                await telegramBot.EditMessageTextAsync(message.CallbackQuery!.Message!.Chat.Id, message.CallbackQuery.Message.MessageId, "Сохранено", replyMarkup: ButtonsTelegram.DirectionOperationMenu);
                return true;
            }
            else
                await telegramBot.EditMessageTextAsync(message.CallbackQuery!.Message!.Chat.Id, message.CallbackQuery.Message.MessageId, "произошла ошибка", replyMarkup: ButtonsTelegram.DirectionOperationMenu);
        }
        else
        {
            var result = await client.DirectionsOperation.UpdateDirectionOperation((Guid)directionOperation.ItemId, directionOperation.Name!, (Guid)directionOperation.DirectionExchangeId!);

            if (result.Success)
            {
                await telegramBot.EditMessageTextAsync(message.CallbackQuery!.Message!.Chat.Id, message.CallbackQuery.Message.MessageId, "Сохранено", replyMarkup: ButtonsTelegram.DirectionOperationMenu);
                return true;
            }
            else
                await telegramBot.EditMessageTextAsync(message.CallbackQuery!.Message!.Chat.Id, message.CallbackQuery.Message.MessageId, "произошла ошибка", replyMarkup: ButtonsTelegram.DirectionOperationMenu);
        }

        return false;
    }

    private async Task<bool> UserSelectedExchange(ITelegramBotClient telegramBot, Update message, UserEntity user)
    {
        await _stateStoreService.SaveExchangeData(message.CallbackQuery!.Message!.Chat.Id, user.TypeOperation, new ExchangeEntity() { DirectionExchangeId = new Guid(message.CallbackQuery!.Data!) });

        var exchangeData = await _stateStoreService.GetExchangeData(message.CallbackQuery!.Message!.Chat.Id);
        if (exchangeData == null)
            return false;

        var client = new ExchangeServiceClient(_serviceUrl);

        if (exchangeData.ItemId == null)
        {
            var result = await client.Exchanges.CreateExchange((Guid)exchangeData.DirectionExchangeId!, (DateTime)exchangeData.DateStart!, (DateTime)exchangeData.DateEnd!, exchangeData.NameExecutor!, exchangeData.Symbol!, (bool)exchangeData.Closed!);

            if (result.Success)
            {
                await telegramBot.EditMessageTextAsync(message.CallbackQuery!.Message!.Chat.Id, message.CallbackQuery.Message.MessageId, "Сохранено", replyMarkup: ButtonsTelegram.ExchangeMenu);
                return true;
            }
            else
                await telegramBot.EditMessageTextAsync(message.CallbackQuery!.Message!.Chat.Id, message.CallbackQuery.Message.MessageId, "произошла ошибка", replyMarkup: ButtonsTelegram.ExchangeMenu);
        }
        else
        {
            var result = await client.Exchanges.UpdateExchange((Guid)exchangeData.ItemId!, (Guid)exchangeData.DirectionExchangeId!, (DateTime)exchangeData.DateStart!, (DateTime)exchangeData.DateEnd!, exchangeData.NameExecutor!, exchangeData.Symbol!, (bool)exchangeData.Closed!);

            if (result.Success)
            {
                await telegramBot.EditMessageTextAsync(message.CallbackQuery!.Message!.Chat.Id, message.CallbackQuery.Message.MessageId, "Сохранено", replyMarkup: ButtonsTelegram.ExchangeMenu);
                return true;
            }
            else
                await telegramBot.EditMessageTextAsync(message.CallbackQuery!.Message!.Chat.Id, message.CallbackQuery.Message.MessageId, "произошла ошибка", replyMarkup: ButtonsTelegram.ExchangeMenu);
        }

        return false;
    }

    private async Task<bool> UserSelectedOperation(ITelegramBotClient telegramBot, Update message, UserEntity user)
    {
        switch (user.State)
        {
            case 4:
                await _stateStoreService.SaveOperationData(message.CallbackQuery!.Message!.Chat.Id, user.TypeOperation, new OperationEntity() { ExchangeId = new Guid(message.CallbackQuery!.Data!) });

                var clientExchange = new ExchangeServiceClient(_serviceUrl);
                var directionsOperation = (await clientExchange.DirectionsOperation.GetDirectionsOperation(null, null)).Data;

                if (directionsOperation == null)
                {
                    await telegramBot.EditMessageTextAsync(message.CallbackQuery!.Message!.Chat.Id, message.CallbackQuery.Message.MessageId, "В базе данных нет записей направления операций", replyMarkup: ButtonsTelegram.MainMenu);
                    break;
                }

                await _stateStoreService.MoveState(message.CallbackQuery!.Message!.Chat.Id, TypeOperationEnum.AddOperation);

                await _stateStoreService.ResetPage(message.CallbackQuery!.Message!.Chat.Id);
                await ListShow(telegramBot, message, ListType.DirectionsOperation);

                break;

            case 5:
                await _stateStoreService.SaveOperationData(message.CallbackQuery!.Message!.Chat.Id, user.TypeOperation, new OperationEntity() { DirectionOperationId = new Guid(message.CallbackQuery!.Data!) });

                var operationData = await _stateStoreService.GetOperationData(message.CallbackQuery!.Message!.Chat.Id);
                if (operationData == null)
                    return false;

                var client = new ExchangeServiceClient(_serviceUrl);

                if (operationData.ItemId == null)
                {
                    var result = await client.Operations.CreateOperation((Guid)operationData.DirectionOperationId!, (Guid)operationData.ExchangeId!, (decimal)operationData.Sum!, (decimal)operationData.Volume!, (OperationTypeEnum)operationData.OperationType!);

                    if (result.Success)
                    {
                        await telegramBot.EditMessageTextAsync(message.CallbackQuery!.Message!.Chat.Id, message.CallbackQuery.Message.MessageId, "Сохранено", replyMarkup: ButtonsTelegram.OperationMenu);
                        return true;
                    }
                    else
                    {
                        await telegramBot.EditMessageTextAsync(message.CallbackQuery!.Message!.Chat.Id, message.CallbackQuery.Message.MessageId, "Произошла ошибка", replyMarkup: ButtonsTelegram.OperationMenu);
                    }
                }
                else
                {
                    var result = await client.Operations.UpdateOperation((Guid)operationData.ItemId, (Guid)operationData.DirectionOperationId!, (Guid)operationData.ExchangeId!, (decimal)operationData.Sum!, (decimal)operationData.Volume!, (OperationTypeEnum)operationData.OperationType!);

                    if (result.Success)
                    {
                        await telegramBot.EditMessageTextAsync(message.CallbackQuery!.Message!.Chat.Id, message.CallbackQuery.Message.MessageId, "Сохранено", replyMarkup: ButtonsTelegram.OperationMenu);
                        return true;
                    }
                    else
                    {
                        await telegramBot.EditMessageTextAsync(message.CallbackQuery!.Message!.Chat.Id, message.CallbackQuery.Message.MessageId, "Произошла ошибка", replyMarkup: ButtonsTelegram.OperationMenu);
                    }
                }

                break;

            default:
                await telegramBot.EditMessageTextAsync(message.CallbackQuery!.Message!.Chat.Id, message.CallbackQuery.Message.MessageId, "Неизвестная команда", replyMarkup: ButtonsTelegram.OperationMenu);
                break;
        }


        return false;
    }

    private async Task SelectOperation(ITelegramBotClient telegramBotClient, Update update)
    {
        await _stateStoreService.MoveState((long)update.CallbackQuery!.Message!.Chat!.Id, TypeOperationEnum.AddOperation);

        await _stateStoreService.ResetPage(update.CallbackQuery!.Message!.Chat.Id);
        await ListShow(telegramBotClient, update, ListType.Exchanges);
    }

    #endregion

    #region [ItemShow]

    private async Task ItemShow(ITelegramBotClient telegramBot, Update message, UserEntity user, bool set = true)
    {
        if (set)
        {
            if (!Guid.TryParse(message.CallbackQuery!.Data, out Guid id))
            {
                await telegramBot.EditMessageTextAsync(message.CallbackQuery!.Message!.Chat.Id, message.CallbackQuery.Message.MessageId, "Произошла ошибка", replyMarkup: ButtonsTelegram.MainMenu);
                return;
            }
            await _stateStoreService.SetItemId(message.CallbackQuery.Message!.Chat.Id, id);
        }

        switch (user.ListType)
        {
            case ListType.Exchanges:
                await ViewInfoExchangeItem(telegramBot, message, user);
                break;
            case ListType.Operations:
                await ViewInfoOperationItem(telegramBot, message, user);
                break;
            case ListType.DirectionsExchange:
                await ViewInfoDirectionExchangeItem(telegramBot, message, user);
                break;
            case ListType.DirectionsOperation:
                await ViewInfoDirectionOperationItem(telegramBot, message, user);
                break;
            default:
                break;
        }
    }

    private async Task ViewInfoExchangeItem(ITelegramBotClient telegramBotClient, Update update, UserEntity user)
    {
        if (user is null)
        {
            await telegramBotClient.EditMessageTextAsync(update.CallbackQuery!.Message!.Chat.Id, update.CallbackQuery.Message.MessageId, "Выбраный элемент не найден", replyMarkup: ButtonsTelegram.MainMenu);
            return;
        }

        var client = new ExchangeServiceClient(_serviceUrl);
        var exchange = (await client.Exchanges.GetExchangeById(user.ItemId)).Data;
        if (exchange is null)
        {
            await telegramBotClient.EditMessageTextAsync(update.CallbackQuery!.Message!.Chat.Id, update.CallbackQuery.Message.MessageId, "Выбраный элемент не найден", replyMarkup: ButtonsTelegram.MainMenu);
            return;
        }
        string message = MessageHelper.GetExchangeInfo(exchange, (await client.DirectionsExchange.GetDirectionExchangeById(exchange.DirectionExchangeId)).Data!);

        await telegramBotClient.EditMessageTextAsync(update.CallbackQuery!.Message!.Chat.Id, update.CallbackQuery.Message.MessageId, message, replyMarkup: ButtonsTelegram.EditMenu, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
    }

    private async Task ViewInfoOperationItem(ITelegramBotClient telegramBotClient, Update update, UserEntity user)
    {
        if (user is null)
        {
            await telegramBotClient.EditMessageTextAsync(update.CallbackQuery!.Message!.Chat.Id, update.CallbackQuery.Message.MessageId, "Выбраный элемент не найден", replyMarkup: ButtonsTelegram.MainMenu);
            return;
        }

        var client = new ExchangeServiceClient(_serviceUrl);
        var operation = (await client.Operations.GetOperationById(user.ItemId)).Data;
        if (operation is null)
        {
            await telegramBotClient.EditMessageTextAsync(update.CallbackQuery!.Message!.Chat.Id, update.CallbackQuery.Message.MessageId, "Выбраный элемент не найден", replyMarkup: ButtonsTelegram.MainMenu);
            return;
        }
        string message = MessageHelper.GetOperationInfo(operation, (await client.Exchanges.GetExchangeById(operation.ExchangeId)).Data!, (await client.DirectionsOperation.GetDirectionOperationById(operation.DirectionOperationId)).Data!);

        await telegramBotClient.EditMessageTextAsync(update.CallbackQuery!.Message!.Chat.Id, update.CallbackQuery.Message.MessageId, message, replyMarkup: ButtonsTelegram.EditMenu, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
    }

    private async Task ViewInfoDirectionOperationItem(ITelegramBotClient telegramBotClient, Update update, UserEntity user)
    {
        if (user is null)
        {
            await telegramBotClient.EditMessageTextAsync(update.CallbackQuery!.Message!.Chat.Id, update.CallbackQuery.Message.MessageId, "Выбраный элемент не найден", replyMarkup: ButtonsTelegram.MainMenu);
            return;
        }

        var client = new ExchangeServiceClient(_serviceUrl);
        var directionOperation = (await client.DirectionsOperation.GetDirectionOperationById(user.ItemId)).Data;
        if (directionOperation is null)
        {
            await telegramBotClient.EditMessageTextAsync(update.CallbackQuery!.Message!.Chat.Id, update.CallbackQuery.Message.MessageId, "Выбраный элемент не найден", replyMarkup: ButtonsTelegram.MainMenu);
            return;
        }

        string message = MessageHelper.GetDirectionOperationInfo(directionOperation, (await client.DirectionsExchange.GetDirectionExchangeById(directionOperation.DirectionExchangeId)).Data!);

        await telegramBotClient.EditMessageTextAsync(update.CallbackQuery!.Message!.Chat.Id, update.CallbackQuery.Message.MessageId, message, replyMarkup: ButtonsTelegram.EditMenu, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
    }

    private async Task ViewInfoDirectionExchangeItem(ITelegramBotClient telegramBotClient, Update update, UserEntity user)
    {
        if (user is null)
        {
            await telegramBotClient.EditMessageTextAsync(update.CallbackQuery!.Message!.Chat.Id, update.CallbackQuery.Message.MessageId, "Выбраный элемент не найден", replyMarkup: ButtonsTelegram.MainMenu);
            return;
        }

        var client = new ExchangeServiceClient(_serviceUrl);
        var directionExchange = (await client.DirectionsExchange.GetDirectionExchangeById(user.ItemId)).Data;
        if (directionExchange is null)
        {
            await telegramBotClient.EditMessageTextAsync(update.CallbackQuery!.Message!.Chat.Id, update.CallbackQuery.Message.MessageId, "Выбраный элемент не найден", replyMarkup: ButtonsTelegram.MainMenu);
            return;
        }
        string message = MessageHelper.GetDirectionExchangeInfo(directionExchange);

        await telegramBotClient.EditMessageTextAsync(update.CallbackQuery!.Message!.Chat.Id, update.CallbackQuery.Message.MessageId, message, replyMarkup: ButtonsTelegram.EditMenu, parseMode: Telegram.Bot.Types.Enums.ParseMode.Html);
    }

    private async Task DeleteItem(ITelegramBotClient telegramBot, Update message, UserEntity user)
    {
        var client = new ExchangeServiceClient(_serviceUrl);
        BaseResponse<Guid> result = null!;

        InlineKeyboardMarkup buttons;
        var answer = "Удалено!";
        switch (user.ListType)
        {
            case ListType.Exchanges:
                result = await client.Exchanges.DeleteExchange(user.ItemId);
                buttons = ButtonsTelegram.ExchangeMenu;
                break;
            case ListType.Operations:
                result = await client.Operations.DeleteOperation(user.ItemId);
                buttons = ButtonsTelegram.OperationMenu;
                break;
            case ListType.DirectionsExchange:
                result = await client.DirectionsExchange.DeleteDirectionExchange(user.ItemId);
                buttons = ButtonsTelegram.DirectionExchangeMenu;
                break;
            case ListType.DirectionsOperation:
                result = await client.DirectionsOperation.DeleteDirectionOperation(user.ItemId);
                buttons = ButtonsTelegram.DirectionOperationMenu;
                break;
            default:
                buttons = ButtonsTelegram.MainMenu;
                break;
        }

        if (result is null || !result.Success)
        {
            answer = "произошла ошибка!";
        }

        await telegramBot.EditMessageTextAsync(message.CallbackQuery!.Message!.Chat.Id, message.CallbackQuery.Message.MessageId, answer, replyMarkup: buttons);
    }

    #endregion

}

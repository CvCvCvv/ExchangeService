using ExchangeService.Client.Abstractions.Models;

namespace TelegramBotExchange.Helpers.Telegram
{
    public static class MessageHelper
    {
        public static string GetExchangeInfo(Exchange exchange, DirectionExchange directionExchange)
        {
            return $"<b>имя исполнителя:</b> {exchange.NameExecutor}\n" +
                $"<b>торговая пара:</b> {exchange.Symbol}\n" +
                $"<b>дата начала:</b> {exchange.DateStart.Date.ToLongDateString()}\n" +
                $"<b>дата конца:</b> {exchange.DateEnd.Date.ToLongDateString()}\n" +
                $"<b>направление обмена:</b> {directionExchange.Name}\n" +
                $"<b>обмен закрыт:</b> {(exchange.Closed ? "да" : "нет")}\n" +
                $"***\n";
        }

        public static string GetOperationInfo(Operation operation, Exchange exchange, DirectionOperation directionOperation)
        {
            return $"<b>уровень:</b> {operation.Volume}\n" +
                $"<b>сумма:</b> {operation.Sum}\n" +
                $"<b>исполнитель обмена:</b> {exchange.NameExecutor}\n" +
                $"<b>направление операции:</b> {directionOperation.Name}\n" +
                $"<b>тип операции:</b> {operation.OperationType} \n" +
                $"***\n";
        }

        public static string GetDirectionOperationInfo(DirectionOperation directionOperation, DirectionExchange directionExchange)
        {
            return $"<b>название:</b> {directionOperation.Name}\n" +
                $"<b>направление обмена:</b> {directionExchange.Name}\n" +
                $"***\n";
        }

        public static string GetDirectionExchangeInfo(DirectionExchange directionExchange)
        {
            return $"<b>название:</b> {directionExchange.Name}\n" +
                $"<b>дата начала:</b> {directionExchange.DateStart.Date.ToLongDateString()}\n" +
                $"***\n";
        }
    }
}

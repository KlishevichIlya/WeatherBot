using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace WeatherBot
{
    public class Handlers
    {
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var handler = update.Type switch
            {
                UpdateType.Message => BotOnMessageReceived(botClient, update.Message)
            };
        }

        private static async Task BotOnMessageReceived(ITelegramBotClient botClient, Message? message)
        {
            if (string.IsNullOrWhiteSpace(message.Text))
                return;
            if (message.Text.Contains("/hi"))
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Hello from WeatherBot");
            }
        }

        internal static Task HandleErrorAsync(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            throw new NotImplementedException();
        }
    }
}

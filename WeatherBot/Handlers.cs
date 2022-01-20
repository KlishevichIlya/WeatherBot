using System.Text.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using WeatherApi.Controllers;
using WeatherBot.Models;
using Yandex.Geocoder;

namespace WeatherBot
{
    public class Handlers
    {
        private static readonly WeatherForecast _weatherForecast;

        static Handlers()
        {
            _weatherForecast = new();
        }
        
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var handler = update.Type switch
            {
                UpdateType.Message => BotOnMessageReceived(botClient, update.Message!)
            };
        }

        private static async Task BotOnMessageReceived(ITelegramBotClient botClient, Message message)
        {          

            if (message.Location is not null)
            {
                var jsonString = await _weatherForecast.GetForecastAsync(message.Location.Latitude, message.Location.Longitude);
                var weatherForecast = JsonSerializer.Deserialize<DayJson>(jsonString); 

            }

            if (!string.IsNullOrWhiteSpace(message.Text))
            {
                var action = message.Text!.Split(' ')[0] switch
                {
                    "/hi" => SendMessageAsync(botClient, message),
                    "/getForecast" => RequestCustomerCoordinate(botClient, message),                   
                    _ => UnknownMessage(botClient, message),
                };
                await action;
            }
        }

        private static async Task RequestCustomerCoordinate(ITelegramBotClient botClient, Message message)
        {            
            var keyboard = new ReplyKeyboardMarkup(new[]
                 {
                new KeyboardButton("Share your location, please :)")
                {
                    RequestLocation = true
                   
                },                
                
            });
            var temp = botClient.SendTextMessageAsync(message.Chat.Id, "Send my location", replyMarkup: keyboard).Result.Location;            
        }

        private static async Task SendMessageAsync(ITelegramBotClient botClient, Message message)
        {
            await botClient.SendTextMessageAsync(message.Chat.Id, "Hello from WeatherBot");            
        }
                
        private static async Task UnknownMessage(ITelegramBotClient botClient, Message message)
        {   
            await botClient.SendTextMessageAsync(message.Chat.Id,
                $"Hmmm...{Environment.NewLine}I don't know this command. Try to use /help for checking that you entered a right command.");
        }

        internal static Task HandleErrorAsync(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            throw new NotImplementedException();
        }
    }
}

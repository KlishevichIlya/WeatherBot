using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using WeatherBot;

public class Program
{
    private static ITelegramBotClient bot;
    private static IConfiguration config;
    public static async Task Main(string[] args)
    {
        config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();

        var token = config.GetSection("Token").Value;
        bot = new TelegramBotClient(token);
        var me = await bot.GetMeAsync();

        /// Start receiving messages
        var cts = new CancellationTokenSource();
        var cancellationToken = cts.Token;
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = { } // receive all update types
        };
        bot.StartReceiving(
            Handlers.HandleUpdateAsync,
            Handlers.HandleErrorAsync,
            receiverOptions,
            cancellationToken
        );
        Console.WriteLine($"Start listening for {me.Username}");
        Console.ReadLine();
    }
   
}

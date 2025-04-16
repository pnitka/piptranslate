using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace PIP;

class Program {
    public static async Task Main(string[] args) {
        var cts = new CancellationTokenSource();

        // Создаем один экземпляр TelegramBotClient
        var botClient = new TelegramBotClient("YOUR_BOT_TOKEN_HERE");

        // Создаем обработчики с использованием одного экземпляра клиента
        Handlers handlers = new Handlers(botClient);

        // Получаем информацию о боте
        var me = await botClient.GetMeAsync(); // Убедитесь, что метод доступен
        Console.WriteLine($"Bot started as {me.FirstName} ({me.Id}). Press Enter to stop the bot");

        // Запускаем получение обновлений
        botClient.StartReceiving(
            handlers.HandleUpdateAsync,
            handlers.HandleErrorAsync,
            cancellationToken: cts.Token
        );

        Console.ReadLine();
        
        // Останавливаем бота
        cts.Cancel();
    }
}
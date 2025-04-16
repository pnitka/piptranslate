using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace PIP;

public class Handlers {
    private readonly ITelegramBotClient bot; // Используем интерфейс ITelegramBotClient

    public Handlers(ITelegramBotClient client) {
        bot = client;
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken) {
        if (update.Type != UpdateType.Message || update.Message?.Text == null) return;

        Message msg = update.Message;

        if (msg.Text == "/start") {
            await bot.SendTextMessageAsync(msg.Chat.Id, $"Привет, {msg.Chat.Username}! Этот бот позволяет переводить сообщения. Напиши его, и бот переведет его на английский и испанский.", cancellationToken: cancellationToken);
        } else if (msg.Text == "/help") {
            await bot.SendTextMessageAsync(msg.Chat.Id, "Команды:\n/start - Запустить бота\n/help - Получить помощь", cancellationToken: cancellationToken);
        }
    }

    public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken) {
        Console.WriteLine($"Ошибка: {exception.Message}");
        return Task.CompletedTask;
    }
}
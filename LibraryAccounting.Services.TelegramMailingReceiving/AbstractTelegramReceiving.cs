using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace LibraryAccounting.Services.TelegramMailingReceiving
{
    public abstract class AbstractTelegramReceiving
    {
        private readonly TelegramBotClient botClient;
        private CancellationTokenSource cts;
        private readonly ILogger<AbstractTelegramReceiving> _logger;

        public AbstractTelegramReceiving(ILogger<AbstractTelegramReceiving> logger, IOptions<TelegramOptions> options)
        {
            _logger = logger;
            botClient = new TelegramBotClient(options.Value.TelegramToken);
        }

        protected abstract string AddBooking(string parameter);

        protected abstract string GetBooks(string parameter);

        public async Task<User> Me()
        {
            return await botClient.GetMeAsync();
        }

        public void Start()
        {
            cts = new CancellationTokenSource();
            botClient.StartReceiving(
                new DefaultUpdateHandler(HandleUpdateAsync, HandleErrorAsync),
                cts.Token);
        }

        public void Stop()
        {
            if (cts != null)
            {
                cts.Cancel();
                cts = null;
            }
        }

        Task HandleErrorAsync(
            ITelegramBotClient botClient,
            Exception exception,
            CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };
            _logger.LogError(errorMessage);
            return Task.CompletedTask;
        }

        async Task HandleUpdateAsync(
            ITelegramBotClient botClient,
            Update update,
            CancellationToken cancellationToken)
        {
            if (update.Type != UpdateType.Message)
                return;
            if (update.Message.Type != MessageType.Text)
                return;

            var chatId = update.Message.Chat.Id;

            _logger.LogInformation($"Received a '{update.Message.Text}' message in chat {chatId}.");

            var text = await AnswerAsync(update.Message.Text);
            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: text
            );
        }

        private string Answer(string command)
        {
            command = command.ToLower();

            if (command == "/start" || command == "/help")
            {
                return "Список комманд:\n" +
                    "Добавить бронировку: /add {Id книги} {Почта}\n" +
                    "Вывести список книг: /books {Автор}";
            }
            else if (command.Contains("/add"))
            {
                var answer = AddBooking(command);
                return answer ?? "Пусто";
            }
            else if (command.Contains("/books"))
            {
                var answer = "Список книг:\n";
                GetBooks(command);
                return answer;
            }
            else
            {
                return "Введите команду";
            }
        }

        private async Task<string> AnswerAsync(string command)
        {
            return await Task.Run(() => Answer(command));
        }
    }
}

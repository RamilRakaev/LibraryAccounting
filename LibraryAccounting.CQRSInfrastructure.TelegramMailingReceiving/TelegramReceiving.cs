using LibraryAccounting.Domain.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace LibraryAccounting.CQRSInfrastructure.TelegramMailingReceiving
{
    public class TelegramReceiving
    {
        private readonly TelegramBotClient botClient;
        private readonly CancellationTokenSource cts;
        private readonly ILogger _logger;
        private event MessageOutputHandler _receive;
        private event ReceiveCommandHandler _addBooking;
        private event ReceiveCommandHandler _getBooks;

        public delegate void MessageOutputHandler(string message);

        public event MessageOutputHandler Receive
        {
            add
            {
                _receive += value;
            }
            remove
            {
                _receive -= value;
            }
        }

        public delegate string ReceiveCommandHandler(string parameter);

        public event ReceiveCommandHandler AddBooking
        {
            add
            {
                _addBooking += value;
            }
            remove
            {
                _addBooking -= value;
            }
        }

        public event ReceiveCommandHandler GetBooks
        {
            add
            {
                _getBooks += value;
            }
            remove
            {
                _getBooks -= value;
            }
        }

        public TelegramReceiving(ILogger logger, string token)
        {
            _logger = logger;
            botClient = new TelegramBotClient(token);

            cts = new CancellationTokenSource();

            botClient.StartReceiving(
                new DefaultUpdateHandler(HandleUpdateAsync, HandleErrorAsync),
                cts.Token);
        }

        public async Task<User> Me()
        {
            return await botClient.GetMeAsync();
        }

        public void Stop()
        {
            cts.Cancel();
        }

        Task HandleErrorAsync(
            ITelegramBotClient botClient,
            Exception exception,
            CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };
            _logger.LogError(ErrorMessage);
            return Task.CompletedTask;
        }

        async Task HandleUpdateAsync(ITelegramBotClient botClient,
            Update update,
            CancellationToken cancellationToken)
        {
            if (update.Type != UpdateType.Message)
                return;
            if (update.Message.Type != MessageType.Text)
                return;

            var chatId = update.Message.Chat.Id;

            _logger.LogInformation($"Received a '{update.Message.Text}' message in chat {chatId}.");
            _receive?.Invoke($"Received a '{update.Message.Text}' message in chat {chatId}.");

            var text = Answer(update.Message.Text);
            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: text
            );
            _receive?.Invoke($"Sending message {text}");
        }

        private string Answer(string message)
        {
            message = message.ToLower();

            if (message == "/start" && message == "/help")
            {
                return "Список комманд:\n" +
                    "Добавить бронировку: /add {Id книги} {Почта}\n" +
                    "Вывести список книг: /books {Автор}";
            }
            else if (message.Contains("/add"))
            {
                var answer = _addBooking?.Invoke(message);
                return answer == null ? "Пусто" : answer;
            }
            else if (message.Contains("books"))
            {
                var answer = "Список книг:\n";
                answer += _getBooks?.Invoke(message);
                return answer;
            }
            else
            {
                return "Введите команду";
            }
        }
    }
}

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
        private CancellationTokenSource cts;
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
        }

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
            _receive?.Invoke(errorMessage);
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
            _receive?.Invoke($"Received a '{update.Message.Text}' message in chat {chatId}.");

            var text = Answer(update.Message.Text);
            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: text
            );
            _receive?.Invoke($"Sending message {text}");
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
                var answer = _addBooking?.Invoke(command);
                return answer == null ? "Пусто" : answer;
            }
            else if (command.Contains("/books"))
            {
                var answer = "Список книг:\n";
                answer += _getBooks?.Invoke(command);
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

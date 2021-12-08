using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
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

        public List<TelegramBookingRequest> Books { get; protected set; }
        public List<TelegramBookingRequest> Bookings { get; protected set; }

        public AbstractTelegramReceiving(IOptions<TelegramOptions> options)
        {
            botClient = new TelegramBotClient(options.Value.TelegramToken);
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

            var text = await AnswerAsync(update.Message.Text, update.Message.Chat.Id);
            await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: text
            );
        }

        private string AddBooking(string message)
        {
            string[] operands = message.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (operands.Length < 3)
            {
                return "Неправильно введена команда";
            }
            int bookId;
            try
            {
                bookId = Convert.ToInt32(operands[1]);
            }
            catch
            {
                return "Неправильно введён id";
            }
            return "Запрос на бронировку книги отправлен";
        }

        private string Answer(string command, long chatId)
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
                
                return AddBooking(command);
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

        private async Task<string> AnswerAsync(string command, long chatId)
        {
            return await Task.Run(() => Answer(command, chatId));
        }
    }
}

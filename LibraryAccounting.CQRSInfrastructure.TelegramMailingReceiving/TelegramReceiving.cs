using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using LibraryAccounting.Services.TelegramMailingReceiving;
using Microsoft.Extensions.Options;

namespace LibraryAccounting.CQRSInfrastructure.TelegramMailingReceiving
{
    public class TelegramReceiving : AbstractTelegramReceiving
    {

        public TelegramReceiving(IOptions<TelegramOptions> options) : base(options)
        {
        }

        protected override string GetBooks(string message)
        {
            string[] operands = message.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (operands.Length < 2)
            {
                return "Неправильно введена команда";
            }
            var books = BooksRepository.GetAllAsNoTracking().Where(b => b.Author.Name == operands[1]);
            if (books == null)
            {
                return "Книг данного автора нет в наличии";
            }
            string answer = "";
            foreach (var book in books)
            {
                answer += $"\nId:{book.Id} {book.Title}, " + book.Booking == null ? "в наличии;" : "забронировано;";
            }
            return answer;
        }

        protected override string AddBooking(string message)
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
            var bookings = new Booking(bookId, )
            if (user != null)
            {
                Bookings.Add(message);
            }
            else
            {
                return "Такого пользователя не существует";
            }
            return "Книга успешно забронирована";
        }
    }
}

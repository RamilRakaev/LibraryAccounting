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
        public UserManager<ApplicationUser> UserManager;
        public IRepository<Booking> Bookings;
        public IRepository<Book> BooksRepository;

        public TelegramReceiving(
            ILogger<AbstractTelegramReceiving> logger,
            IOptions<TelegramOptions> options,
            UserManager<ApplicationUser> userManager,
            IRepository<Booking> bookings,
            IRepository<Book> books) : base(logger, options)
        {
            UserManager = userManager;
            Bookings = bookings;
            BooksRepository = books;
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
            var email = operands[2];
            var user = UserManager.FindByEmailAsync(email).Result;
            if (user != null)
            {
                Bookings.AddAsync(new Booking(bookId, user.Id));
            }
            else
            {
                return "Такого пользователя не существует";
            }
            return "Книга успешно забронирована";
        }
    }
}

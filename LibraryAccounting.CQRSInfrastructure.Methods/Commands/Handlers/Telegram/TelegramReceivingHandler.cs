using LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests;
using LibraryAccounting.CQRSInfrastructure.Methods.Queries;
using LibraryAccounting.CQRSInfrastructure.Methods.Queries.Requests;
using LibraryAccounting.CQRSInfrastructure.TelegramMailingReceiving;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Handlers
{
    public class TelegramReceivingHandler : IRequestHandler<TelegramCommand, TelegramReceiving>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<Booking> _bookingRepository;
        private readonly IRepository<Book> _bookRepository;

        public TelegramReceiving Telegram { get; set; }

        public TelegramReceivingHandler(UserManager<ApplicationUser> userManager,
            IRepository<Booking> bookingRepository,
            IRepository<Book> bookRepository)
        {
            _userManager = userManager;
            _bookingRepository = bookingRepository;
            _bookRepository = bookRepository;
        }

        public async Task<TelegramReceiving> Handle(TelegramCommand request, CancellationToken cancellationToken)
        {
            Telegram = new TelegramReceiving(request.Logger, request.Token, _userManager, _bookingRepository, _bookRepository);
            Telegram.AddBooking += AddBooking;
            Telegram.GetBooks += GetBooks;
            return await Task.FromResult(Telegram);
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
            var email = operands[2];
            var user = Telegram.UserManager.FindByEmailAsync(email).Result;
            if (user != null)
            {
                Telegram.BookingRepository.AddAsync(new Booking(bookId, user.Id));
            }
            else
            {
                return "Такого пользователя не существует";
            }
            return "Книга успешно забронирована";
        }

        private string GetBooks(string message)
        {
            var books = Telegram.BookRepository.GetAllAsNoTracking();
            string[] operands = message.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (operands.Length < 2)
            {
                return "Неправильно введена команда";
            }
            books = books.Where(b => b.Author.Name == operands[1]);
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
    }
}

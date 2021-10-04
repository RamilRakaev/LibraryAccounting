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
            var telegram = new TelegramReceiving(request.Logger, request.Token);
            telegram.AddBooking += AddBooking;
            telegram.GetBooks += GetBooks;
            return await Task.FromResult(telegram);
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
            var user = _userManager.FindByEmailAsync(email).Result;
            if (user != null)
            {
                _bookingRepository.AddAsync(new Booking(bookId, user.Id));
            }
            else
            {
                return "Такого пользователя не существует";
            }
            return "Книга успешно забронирована";
        }

        private string GetBooks(string message)
        {
            var books = _bookRepository.GetAllAsNoTracking();
            string[] operands = message.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (operands.Length < 2)
            {
                return "Неправильно введена команда";
            }
            books = books.Where(b => b.Author.Name == operands[1]);
            if (books == null)
            {
                return "Книги данного автора нет в наличии";
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

using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Interfaces.PocessingRequests;
using LibraryAccounting.Domain.Model;
using LibraryAccounting.Services.ToolInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace LibraryAccounting.Infrastructure.Tools
{
    public class ClientTools : IClientTools
    {
        readonly protected IRepository<Book> BookRepository;
        readonly protected IRepository<Booking> BookingRepository;

        public ClientTools(IRepository<Booking> bookingsRepository, IRepository<Book> bookRepository)
        {
            BookingRepository = bookingsRepository;
            BookRepository = bookRepository;
        }

        #region books requests
        public Book GetBook(int id)
        {
            return BookRepository.Find(id);
        }

        public Book GetBook(IReturningResultHandler<Book, Book> resultHandler)
        {
            return resultHandler.Handle(BookRepository.GetAll());
        }

        public IEnumerable<Book> GetBooks(IRequestsHandlerComponent<Book> handlerComponent)
        {
            var elements = BookRepository.GetAll().ToList();
            handlerComponent.Handle(ref elements);
            return elements;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return BookRepository.GetAll();
        }
        #endregion

        public void AddReservation(Booking booking)
        {
            BookingRepository.Add(booking);
            BookingRepository.Save();
        }

        public void RemoveReservation(int id)
        {
            BookingRepository.Remove(BookingRepository.Find(id));
            BookingRepository.Save();
        }

        public IEnumerable<Booking> GetBookings(IRequestsHandlerComponent<Booking> handlerComponent)
        {
            var bookings = BookingRepository.GetAll().ToList();
            handlerComponent.Handle(ref bookings);
            return bookings;
        }

        public IEnumerable<Booking> GetAllBookings()
        {
            return BookingRepository.GetAll();
        }
    }
}

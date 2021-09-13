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
        readonly protected IRepository<Book> _bookRepository;
        readonly protected IRepository<Booking> _bookingRepository;

        public ClientTools(IRepository<Booking> bookingsRepository, IRepository<Book> bookRepository)
        {
            _bookingRepository = bookingsRepository;
            _bookRepository = bookRepository;
        }

        #region books requests
        public Book GetBook(int id)
        {
            return _bookRepository.Find(id);
        }

        public Book GetBook(IReturningResultHandler<Book, Book> resultHandler)
        {
            return resultHandler.Handle(_bookRepository.GetAll());
        }

        public IEnumerable<Book> GetBooks(IRequestsHandlerComponent<Book> handlerComponent)
        {
            var elements = _bookRepository.GetAll().ToList();
            handlerComponent.Handle(ref elements);
            return elements;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _bookRepository.GetAll();
        }
        #endregion

        #region reservation book
        public void AddReservation(Booking booking)
        {
            _bookingRepository.Add(booking);
            _bookingRepository.Save();
        }

        public void RemoveReservation(int id)
        {
            _bookingRepository.Remove(_bookingRepository.Find(id));
            _bookingRepository.Save();
        }
        #endregion

        #region get bookings
        public IEnumerable<Booking> GetBookings(IRequestsHandlerComponent<Booking> handlerComponent)
        {
            var bookings = _bookingRepository.GetAll().ToList();
            handlerComponent.Handle(ref bookings);
            return bookings;
        }

        public IEnumerable<Booking> GetAllBookings()
        {
            return _bookingRepository.GetAll();
        }
        #endregion
    }
}

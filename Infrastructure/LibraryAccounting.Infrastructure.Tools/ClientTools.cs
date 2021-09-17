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
            return _bookRepository.FindNoTrackingAsync(id).Result;
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
            _bookingRepository.AddAsync(booking);
            _bookingRepository.SaveAsync();
        }

        public void RemoveReservation(int id)
        {
            _bookingRepository.RemoveAsync(_bookingRepository.FindNoTrackingAsync(id).Result);
            _bookingRepository.SaveAsync();
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

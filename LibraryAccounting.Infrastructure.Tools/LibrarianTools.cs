using LibraryAccounting.Infrastructure.ObjectStructure;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Interfaces.PocessingRequests;
using LibraryAccounting.Domain.Model;
using LibraryAccounting.Domain.ToolInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryAccounting.Infrastructure.Tools
{
    public class LibrarianTools : ClientTools, ILibrarianTools
    {
        private CompositeElement<Book> CompositeElement;
        readonly private IRepository<User> UserRepository;

        public LibrarianTools(IRepository<Booking> bookingsRepository, 
            IRepository<Book> bookRepository, 
            IRepository<User> userRepository) : 
            base(bookingsRepository, bookRepository)
        {
            UserRepository = userRepository;
        }

        #region add and remove
        public void AddBook(Book book)
        {
            BookRepository.Add(book);
            BookRepository.Save();
        }

        public void RemoveBook(Book book)
        {
            BookRepository.Remove(book);
            BookRepository.Save();
        }
        #endregion

        #region booking books
        public void EditBooking(IVisitor<Booking> visitor, IReturningResultHandler<Booking, Booking> resultHandler)
        {
            resultHandler.Handle(BookingRepository.GetAll()).Accept(visitor);
            BookingRepository.Save();
        }

        public Booking GetBooking(IReturningResultHandler<Booking, Booking> requestsHandler)
        {
            var bookings = BookingRepository.GetAll().ToList();
            return requestsHandler.Handle(bookings);
        }
        #endregion

        #region get users
        public User GetUser(int id)
        {
            return UserRepository.Find(id);
        }

        public User GetUser(IReturningResultHandler<User, User> resultHandler)
        {
            return resultHandler.Handle(UserRepository.GetAll().ToList());
        }

        public IEnumerable<User> GetUsers(IRequestsHandlerComponent<User> handlerComponent)
        {
            var elements = UserRepository.GetAll().ToList();
            handlerComponent.Handle(ref elements);
            return elements;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return UserRepository.GetAll();
        }
        #endregion

        #region edit books
        public void EditBook(IVisitor<Book> visitor, int id)
        {
            if (!BookRepository.Find(id).Accept(visitor))
            {
                throw new Exception("Error when editing book");
            }
        }

        public void EditBooks(IVisitor<Book> visitor, IRequestsHandlerComponent<Book> handlerComponent = null)
        {
            CompositeElement = new CompositeElement<Book>(BookRepository.GetAll().ToList());
            if (!CompositeElement.Accept(visitor, handlerComponent))
            {
                throw new Exception("Error when editing books");
            }
        }
        #endregion
    }
}

using LibraryAccounting.Infrastructure.ObjectStructure;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Interfaces.PocessingRequests;
using LibraryAccounting.Domain.Model;
using LibraryAccounting.Services.ToolInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace LibraryAccounting.Infrastructure.Tools
{
    public class LibrarianTools : ClientTools, ILibrarianTools
    {
        private CompositeElement<Book> compositeElement;
        readonly private UserManager<ApplicationUser> _userManager;

        public LibrarianTools(IRepository<Booking> bookingsRepository, 
            IRepository<Book> bookRepository,
            UserManager<ApplicationUser> userManager) : 
            base(bookingsRepository, bookRepository)
        {
            _userManager = userManager;
        }

        #region add and remove
        public void AddBook(Book book)
        {
            _bookRepository.AddAsync(book);
            _bookRepository.SaveAsync();
        }

        public void RemoveBook(Book book)
        {
            _bookRepository.RemoveAsync(book);
            _bookRepository.SaveAsync();
        }
        #endregion

        #region booking books
        public void EditBooking(IVisitor<Booking> visitor, IReturningResultHandler<Booking, Booking> resultHandler)
        {
            resultHandler.Handle(_bookingRepository.GetAll()).Accept(visitor);
            _bookingRepository.SaveAsync();
        }

        public Booking GetBooking(IReturningResultHandler<Booking, Booking> requestsHandler)
        {
            var bookings = _bookingRepository.GetAll().ToList();
            return requestsHandler.Handle(bookings);
        }
        #endregion

        #region get users
        public ApplicationUser GetUser(int id)
        {
            return _userManager.Users.FirstOrDefault(u => u.Id == id);
        }

        public ApplicationUser GetUser(IReturningResultHandler<ApplicationUser, ApplicationUser> resultHandler)
        {
            return resultHandler.Handle(_userManager.Users.ToList());
        }

        public IEnumerable<ApplicationUser> GetUsers(IRequestsHandlerComponent<ApplicationUser> handlerComponent)
        {
            var elements = _userManager.Users.ToList();
            handlerComponent.Handle(ref elements);
            return elements;
        }

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return _userManager.Users;
        }
        #endregion

        #region edit books
        public void EditBook(IVisitor<Book> visitor, int id)
        {
            if (_bookRepository.FindAsync(id).Result.Accept(visitor) == false)
            {
                throw new Exception("Error when editing book");
            }
        }

        public void EditBooks(IVisitor<Book> visitor, IRequestsHandlerComponent<Book> handlerComponent = null)
        {
            compositeElement = new CompositeElement<Book>(_bookRepository.GetAll().ToList());
            if (compositeElement.Accept(visitor, handlerComponent) == false)
            {
                throw new Exception("Error when editing books");
            }
        }
        #endregion
    }
}

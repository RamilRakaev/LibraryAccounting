﻿using LibraryAccounting.Infrastructure.ObjectStructure;
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
            if (!BookRepository.Find(id).Accept(visitor))
            {
                throw new Exception("Error when editing book");
            }
        }

        public void EditBooks(IVisitor<Book> visitor, IRequestsHandlerComponent<Book> handlerComponent = null)
        {
            compositeElement = new CompositeElement<Book>(BookRepository.GetAll().ToList());
            if (!compositeElement.Accept(visitor, handlerComponent))
            {
                throw new Exception("Error when editing books");
            }
        }
        #endregion
    }
}

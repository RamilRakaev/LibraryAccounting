using Infrastructure.Handlers;
using Infrastructure.ObjectStructure;
using Infrastructure.Repositories;
using Infrastructure.Visitors;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Interfaces.PocessingRequests;
using LibraryAccounting.Domain.Model;
using LibraryAccounting.Domain.ToolsInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryAccounting.Infrastructure.Tools
{
    public class LibrarianTools : ILibrarianTools
    {
        readonly private IRepository<Book> BookRepository;
        readonly private IRepository<Booking> BookingsRepository;
        private CompositeElement<Book> CompositeElement;

        public LibrarianTools(IRepository<Booking> bookingsRepository, IRepository<Book> bookRepository)
        {
            BookingsRepository = bookingsRepository;
            BookRepository = bookRepository;
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

        #region reception and delivery of books
        public bool GetBookFromClient(int bookId, int clientId)
        {
            var handler = new BookingsByClientIdHandler(bookId);
            var elements = BookingsRepository.GetAll();
            handler.Handle(ref elements);
            return elements.First().Accept(new GetBookFromClientVisitor());
        }

        public bool GiveBookToClient(int bookId, int clientId)
        {
            var handler = new BookingsByClientIdHandler(bookId);
            var elements = BookingsRepository.GetAll();
            handler.Handle(ref elements);
            return elements.First().Accept(new GiveBookToClientVisitor());
        }
        #endregion

        #region books requests
        public Book GetBook(IRequestsHandlerComponent<Book> handlerComponent)
        {
            var elements = BookRepository.GetAll();
            handlerComponent.Handle(ref elements);
            return elements.FirstOrDefault();
        }

        public IEnumerable<Book> GetBooks(IRequestsHandlerComponent<Book> handlerComponent)
        {
            var elements = BookRepository.GetAll();
            handlerComponent.Handle(ref elements);
            return elements;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return BookRepository.GetAll();
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
            CompositeElement = new CompositeElement<Book>(BookRepository.GetAll());
            if (!CompositeElement.Accept(visitor, handlerComponent))
            {
                throw new Exception("Error when editing books");
            }
        }
        #endregion
    }
}

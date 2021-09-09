using LibraryAccounting.Infrastructure.Handlers;
using LibraryAccounting.Infrastructure.Repositories;
using LibraryAccounting.Infrastructure.Visitors;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Interfaces.PocessingRequests;
using LibraryAccounting.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using LibraryAccounting.Services.ToolInterfaces;
using LibraryAccounting.Infrastructure.Tools;

namespace LibraryAccounting.Infrastructure.UnitTests
{
    [TestClass]
    public class ToolsTest
    {
        private IRepository<User> userRepository;
        private IRepository<Book> bookRepository;
        private IRepository<Booking> bookingRepository;
        private IStorageRequests<UserRole> roleRequests;
        IAdminTools AdminTools;
        ILibrarianTools LibrarianTools;
        [TestMethod]
        public void LibrarianToolsTest()
        {
            bookRepository = new BookRepository(new DataContext(Startup.OnConfiguring()));
            bookingRepository = new BookingRepository(new DataContext(Startup.OnConfiguring()));
            LibrarianTools = new LibrarianTools(bookingRepository, bookRepository, userRepository);

            int count = LibrarianTools.GetAllBooks().Count();
            LibrarianTools.AddBook(new Book() { Title = "book1", Author = "author1", Genre = "genre2", Publisher = "publisher1" });
            Assert.AreEqual(LibrarianTools.GetAllBooks().Count(), count + 1);

            var handlers = new List<IRequestsHandlerComponent<Book>>() { 
                new BooksByAuthorHandler("author1"), 
                new BooksByGenreHandler("genre2"),
                new BooksByPublisherHandler("publisher1")
            };

            var decorator = new DecoratorHandler<Book>(handlers);
            var book = LibrarianTools.GetBooks(decorator).ElementAt(0);
            Assert.IsNotNull(book);

            userRepository = new UserRepository(new DataContext(Startup.OnConfiguring()));
            userRepository.Add(new User("name1", "email1@gmail.com", "password1", 1));
            userRepository.Save();
            var user = userRepository.GetAll().First(u => u.UserName == "name1");
            var booking = new Booking(book.Id, user.Id);
            bookingRepository.Add(booking);
            bookingRepository.Save();


            LibrarianTools.EditBooking(new GiveBookToClientVisitor(), new BookingByBookIdHandler(book.Id));
            Assert.IsNotNull(LibrarianTools.GetBooking(new BookingByBookIdHandler(book.Id)));

            LibrarianTools.EditBooking(new GetBookFromClientVisitor(), new BookingByBookIdHandler(book.Id));
            Assert.IsNull(LibrarianTools.GetBooking(new BookingByBookIdHandler(book.Id)));

            LibrarianTools.RemoveBook(book);
            Assert.AreEqual(LibrarianTools.GetAllBooks().Count(), count);

            userRepository.Remove(user);
            userRepository.Save();
            bookingRepository.Remove(booking);
            bookingRepository.Save();
        }

        [TestMethod]
        public void AdminToolsTest()
        {
            userRepository = new UserRepository(new DataContext(Startup.OnConfiguring()));
            roleRequests = new RoleRequests(new DataContext(Startup.OnConfiguring()));
            AdminTools = new AdminTools(userRepository, roleRequests);
            User user = new User("Name1", "email@gmail.com", "password1", 1);

            int count = AdminTools.GetAllUsers().Count();
            AdminTools.AddUser(user);
            Assert.AreEqual(AdminTools.GetAllUsers().Count(), count + 1);

            user = AdminTools.GetUser(new UserByEmailHandler("email@gmail.com"));
            Assert.AreEqual(user.UserName, "Name1");

            AdminTools.EditUser(new ChangePasswordVisitor("newPassword1"), user.Id);
            Assert.AreEqual(AdminTools.GetUser(user.Id).Password, "newPassword1");

            AdminTools.RemoveUser(user);
            Assert.AreEqual(AdminTools.GetAllUsers().Count(), count);


        }
    }
}

using LibraryAccounting.Infrastructure.Handlers;
using LibraryAccounting.Infrastructure.Repositories;
using LibraryAccounting.Infrastructure.Visitors;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Interfaces.PocessingRequests;
using LibraryAccounting.Domain.Model;
using LibraryAccounting.Domain.ToolInterfaces;
using LibraryAccounting.Infrastructure.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace LibraryAccounting.Infrastructure.UnitTests
{
    [TestClass]
    public class ToolsTest
    {
        private IRepository<User> UserRepository;
        private IRepository<Book> BookRepository;
        private IRepository<Booking> BookingRepository;
        private IStorageRequests<Role> RoleRequests;
        readonly private DbContextOptions<DataContext> opt = new DbContextOptionsBuilder<DataContext>().
            UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=LibraryAccounting;Trusted_Connection=True;").Options;
        IAdminTools AdminTools;
        ILibrarianTools LibrarianTools;

        [TestMethod]
        public void LibrarianToolsTest()
        {
            BookRepository = new BookRepository(new DataContext(opt));
            BookingRepository = new BookingRepository(new DataContext(opt));
            LibrarianTools = new LibrarianTools(BookingRepository, BookRepository, UserRepository);

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

            UserRepository = new UserRepository(new DataContext(opt));
            UserRepository.Add(new User("name1", "email1@gmail.com", "password1", 1));
            UserRepository.Save();
            var user = UserRepository.GetAll().First(u => u.Name == "name1");
            var booking = new Booking(book.Id, user.Id);
            BookingRepository.Add(booking);
            BookingRepository.Save();


            LibrarianTools.EditBooking(new GiveBookToClientVisitor(), new BookingByBookIdHandler(book.Id));
            Assert.IsNotNull(LibrarianTools.GetBooking(new BookingByBookIdHandler(book.Id)));

            LibrarianTools.EditBooking(new GetBookFromClientVisitor(), new BookingByBookIdHandler(book.Id));
            Assert.IsNull(LibrarianTools.GetBooking(new BookingByBookIdHandler(book.Id)));

            LibrarianTools.RemoveBook(book);
            Assert.AreEqual(LibrarianTools.GetAllBooks().Count(), count);

            UserRepository.Remove(user);
            UserRepository.Save();
            BookingRepository.Remove(booking);
            BookingRepository.Save();
        }

        [TestMethod]
        public void AdminToolsTest()
        {
            UserRepository = new UserRepository(new DataContext(opt));
            RoleRequests = new RoleRequests(new DataContext(opt));
            AdminTools = new AdminTools(UserRepository, RoleRequests);
            User user = new User("Name1", "email@gmail.com", "password1", 1);

            int count = AdminTools.GetAllUsers().Count();
            AdminTools.AddUser(user);
            Assert.AreEqual(AdminTools.GetAllUsers().Count(), count + 1);

            user = AdminTools.GetUser(new UserByEmailHandler("email@gmail.com"));
            Assert.AreEqual(user.Name, "Name1");

            AdminTools.EditUser(new ChangePasswordVisitor("newPassword1"), user.Id);
            Assert.AreEqual(AdminTools.GetUser(user.Id).Password, "newPassword1");

            AdminTools.RemoveUser(user);
            Assert.AreEqual(AdminTools.GetAllUsers().Count(), count);


        }
    }
}

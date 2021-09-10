using LibraryAccounting.Infrastructure.Handlers;
using LibraryAccounting.Infrastructure.Visitors;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Interfaces.PocessingRequests;
using LibraryAccounting.Domain.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using LibraryAccounting.Services.ToolInterfaces;
using Microsoft.AspNetCore.Identity;

namespace LibraryAccounting.Infrastructure.UnitTests
{
    [TestClass]
    public class ToolsTest
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Booking> _bookingRepository;
        private readonly RoleManager<ApplicationUserRole> _roleManager;
        private readonly IAdminTools _adminTools;
        private readonly ILibrarianTools _librarianTools;

        public ToolsTest(IAdminTools adminTools,
            ILibrarianTools librarianTools,
            IRepository<Book> bookRepository,
            RoleManager<ApplicationUserRole> roleManager,
            UserManager<ApplicationUser> userManager,
            IRepository<Booking> bookingRepository)
        {
            _adminTools = adminTools;
            _librarianTools = librarianTools;
            _roleManager = roleManager;
            _userManager = userManager;
            _bookRepository = bookRepository;
            _bookingRepository = bookingRepository;
        }

        [TestMethod]
        public void LibrarianToolsTest()
        {

            int count = _librarianTools.GetAllBooks().Count();
            _librarianTools.AddBook(new Book() { Title = "book1", Author = "author1", Genre = "genre2", Publisher = "publisher1" });
            Assert.AreEqual(_librarianTools.GetAllBooks().Count(), count + 1);

            var handlers = new List<IRequestsHandlerComponent<Book>>() {
                new BooksByAuthorHandler("author1"),
                new BooksByGenreHandler("genre2"),
                new BooksByPublisherHandler("publisher1")
            };

            var decorator = new DecoratorHandler<Book>(handlers);
            var book = _librarianTools.GetBooks(decorator).ElementAt(0);
            Assert.IsNotNull(book);

            _userManager.CreateAsync(new ApplicationUser("name1", "email1@gmail.com", "password1", 1));
            var user = _userManager.Users.First(u => u.UserName == "name1");
            var booking = new Booking(book.Id, user.Id);
            _bookingRepository.Add(booking);
            _bookingRepository.Save();


            _librarianTools.EditBooking(new GiveBookToClientVisitor(), new BookingByBookIdHandler(book.Id));
            Assert.IsNotNull(_librarianTools.GetBooking(new BookingByBookIdHandler(book.Id)));

            _librarianTools.EditBooking(new GetBookFromClientVisitor(), new BookingByBookIdHandler(book.Id));
            Assert.IsNull(_librarianTools.GetBooking(new BookingByBookIdHandler(book.Id)));

            _librarianTools.RemoveBook(book);
            Assert.AreEqual(_librarianTools.GetAllBooks().Count(), count);

            _userManager.DeleteAsync(user);
            _bookingRepository.Remove(booking);
            _bookingRepository.Save();
        }

        [TestMethod]
        public void AdminToolsTest()
        {
            ApplicationUser user = new ApplicationUser("Name1", "email@gmail.com", "password1", 1);

            int count = _adminTools.GetAllUsers().Count();
            _adminTools.AddUser(user);
            Assert.AreEqual(_adminTools.GetAllUsers().Count(), count + 1);

            user = _adminTools.GetUser(new UserByEmailHandler("email@gmail.com"));
            Assert.AreEqual(user.UserName, "Name1");

            _adminTools.EditUser(new ChangePasswordVisitor("newPassword1"), user.Id);
            Assert.AreEqual(_adminTools.GetUser(user.Id).Password, "newPassword1");

            _adminTools.RemoveUser(user);
            Assert.AreEqual(_adminTools.GetAllUsers().Count(), count);
        }
    }
}

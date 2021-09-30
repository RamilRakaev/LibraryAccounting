using LibraryAccounting.Infrastructure.Repositories;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.IO;
using Microsoft.AspNetCore.Identity;
using LibraryAccounting.Services.ToolInterfaces;
using Moq;
using System.Collections.Generic;
using LibraryAccounting.CQRSInfrastructure.Methods.Queries;
using LibraryAccounting.CQRSInfrastructure.Methods.Queries.Requests;
using System.Linq.Expressions;
using System.Reflection;

namespace LibraryAccounting.Infrastructure.UnitTests
{
    [TestClass]
    public class RepositoryTests
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<Booking> _bookingRepository;
        private readonly DataContext _db;


        private readonly BookAuthor author;
        private readonly Genre genre;
        private readonly Book book;
        ApplicationUser user;
        Booking booking;

        private List<ApplicationUser> _users = new List<ApplicationUser>
         {
              new ApplicationUser("User1", "user1@bv.com", "password1", 1) { Id = 1 },
              new ApplicationUser("User2", "user2@bv.com", "password2", 1) { Id = 2 }
         };

        public RepositoryTests()
        {
            author = new BookAuthor() { Name = "author1" };
            genre = new Genre() { Name = "genre2" };
            book = new Book()
            {
                Title = "title1",
                Author = author,
                Genre = genre,
                Publisher = "publisher"
            };
            _db = new DataContext(Startup.DataContextOptions());
            _userManager = MockUserManager(_users).Object;
            _bookRepository = new BookRepository(_db);
            _bookingRepository = new BookingRepository(_db);
        }

        public static Mock<UserManager<TUser>> MockUserManager<TUser>(List<TUser> ls) where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<TUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

            mgr.Setup(x => x.DeleteAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<TUser, string>((x, y) => ls.Add(x));
            mgr.Setup(x => x.UpdateAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);

            return mgr;
        }

        [TestMethod]
        public void BookRepositoryTest()
        {
            var AllElements = _bookRepository.GetAll();
            Assert.IsNotNull(AllElements);
            int AllElementsCount = AllElements.Count();

            _bookRepository.AddAsync(book);
            _bookRepository.SaveAsync();
            Assert.IsFalse(AllElementsCount == _bookRepository.GetAll().Count());

            Book ConcreteElement = _bookRepository.FindNoTrackingAsync(book.Id).Result;
            Assert.AreEqual(ConcreteElement, book);

            _bookRepository.RemoveAsync(book);
            _bookRepository.SaveAsync();
            Assert.IsTrue(AllElementsCount == _bookRepository.GetAll().Count());
        }

        [TestMethod]
        public void UserRepositoryTest()
        {
            user = new ApplicationUser()
            {
                UserName = "Иван",
                Password = "1234",
                Email = "IVAN228@gmail.com",
                RoleId = 1
            };

            using DataContext db = new DataContext(Startup.DataContextOptions());
            var AllElements = _userManager.Users;
            Assert.IsNotNull(AllElements);
            int AllElementsCount = AllElements.Count();

            _userManager.CreateAsync(user);
            Assert.IsFalse(AllElementsCount == _userManager.Users.Count());

            ApplicationUser ConcreteElement = _userManager.Users.FirstOrDefault(u => u.Id == user.Id);
            Assert.AreEqual(ConcreteElement, user);

            _userManager.DeleteAsync(user);
            Assert.IsTrue(AllElementsCount == _userManager.Users.Count());
        }

        [TestMethod]
        public void BookingRepositoryTest()
        {
            booking = new Booking(1, 1)
            {
                IsTransmitted = true,
                TransferDate = DateTime.Now
            };

            var AllElements = _bookingRepository.GetAll();
            Assert.IsNotNull(AllElements);
            int AllElementsCount = AllElements.Count();

            _bookingRepository.AddAsync(booking);
            _bookingRepository.SaveAsync();
            Assert.IsFalse(AllElementsCount == _bookingRepository.GetAll().Count());

            Booking ConcreteElement = _bookingRepository.FindNoTrackingAsync(booking.Id).Result;
            Assert.AreEqual(ConcreteElement, booking);

            _bookingRepository.RemoveAsync(booking);
            _bookingRepository.SaveAsync();
            Assert.IsTrue(AllElementsCount == _bookingRepository.GetAll().Count());
        }

        [TestMethod]
        public void FilterTest()
        {
            Func<Book, bool> func = (b) => b.AuthorId == 1 && b.GenreId == 1;
            _bookRepository
                .GetAllAsNoTracking()
                .Where(func);
        }

        [TestMethod]
        public void FilterWithExpressionsTest()
        {
            Func<Book, bool> func = (b) => b.AuthorId == 1 && b.GenreId == 1;
            Type enumerableType = typeof(IQueryable);
            var methods = enumerableType
                .GetMethods(BindingFlags.Public | BindingFlags.Static);
            var selectedMethods = methods.Where(m => m.Name == "Where");

            var method = selectedMethods.FirstOrDefault();
            method = method.MakeGenericMethod(typeof)
        }
    }
}

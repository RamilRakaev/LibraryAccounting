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

namespace LibraryAccounting.Infrastructure.UnitTests
{
    [TestClass]
    public class RepositoryTests
    {
        IRepository<Book> _bookRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        IRepository<Booking> _bookingRepository;
        Book book;
        ApplicationUser user;
        Booking booking;

        public RepositoryTests(
            IRepository<Book> bookRepository,
            UserManager<ApplicationUser> userManager,
            IRepository<Booking> bookingRepository)
        {
            _userManager = userManager;
            _bookRepository = bookRepository;
            _bookingRepository = bookingRepository;
        }


        [TestMethod]
        public void BookRepositoryTest()
        {
            using DataContext db = new DataContext(Startup.OnConfiguring());
            book = new Book()
            {
                Title = "Геном",
                Author = "Мэтт Ридли",
                Publisher = "ООО Издетельство \"Эксмо\""
            };

            _bookRepository = new BookRepository(db);

            var AllElements = _bookRepository.GetAll();
            Assert.IsNotNull(AllElements);
            int AllElementsCount = AllElements.Count();

            _bookRepository.Add(book);
            _bookRepository.Save();
            Assert.IsFalse(AllElementsCount == _bookRepository.GetAll().Count());

            Book ConcreteElement = _bookRepository.Find(book.Id);
            Assert.AreEqual(ConcreteElement, book);

            _bookRepository.Remove(book);
            _bookRepository.Save();
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

            using DataContext db = new DataContext(Startup.OnConfiguring());
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
            using DataContext db = new DataContext(Startup.OnConfiguring());
            _bookingRepository = new BookingRepository(db);
            booking = new Booking(1, 1)
            {
                IsTransmitted = true,
                TransferDate = DateTime.Now
            };

            var AllElements = _bookingRepository.GetAll();
            Assert.IsNotNull(AllElements);
            int AllElementsCount = AllElements.Count();

            _bookingRepository.Add(booking);
            _bookingRepository.Save();
            Assert.IsFalse(AllElementsCount == _bookingRepository.GetAll().Count());

            Booking ConcreteElement = _bookingRepository.Find(booking.Id);
            Assert.AreEqual(ConcreteElement, booking);

            _bookingRepository.Remove(booking);
            _bookingRepository.Save();
            Assert.IsTrue(AllElementsCount == _bookingRepository.GetAll().Count());
        }
    }
}

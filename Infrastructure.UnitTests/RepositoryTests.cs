using Infrastructure.Repositories;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace LibraryAccounting.Infrastructure.UnitTests
{
    [TestClass]
    public class RepositoryTests
    {
        IRepository<Book> BookRepository;
        IRepository<User> UserRepository;
        IRepository<Booking> BookingsRepository;
        readonly private DbContextOptions<DataContext> options = new DbContextOptionsBuilder<DataContext>().
                UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=LibraryAccounting;Trusted_Connection=True;").Options;
        Book book;
        User user;
        Booking booking;

        [TestMethod]
        public void BookRepositoryTest()
        {
            using (DataContext db = new DataContext(options))
            {
                book = new Book()
                {
                    Title = "Геном",
                    Author = "Мэтт Ридли",
                    Publisher = "ООО Издетельство \"Эксмо\""
                };

                BookRepository = new BookRepository(db);
                
                var AllElements = BookRepository.GetAll();
                Assert.IsNotNull(AllElements);
                int AllElementsCount = AllElements.Count();

                BookRepository.Add(book);
                BookRepository.Save();
                Assert.IsFalse(AllElementsCount == BookRepository.GetAll().Count());

                Book ConcreteElement = BookRepository.Find(book.Id);
                Assert.AreEqual(ConcreteElement, book);

                BookRepository.Remove(book);
                BookRepository.Save();
                Assert.IsTrue(AllElementsCount == BookRepository.GetAll().Count());
            }
        }

        [TestMethod]
        public void UserRepositoryTest()
        {
            user = new User()
            {
                Name = "Иван",
                Password = "1234",
                Email = "IVAN228@gmail.com",
                RoleId = 1
            };

            using (DataContext db = new DataContext(options))
            {
                UserRepository = new UserRepository(db);

                var AllElements = UserRepository.GetAll();
                Assert.IsNotNull(AllElements);
                int AllElementsCount = AllElements.Count();

                UserRepository.Add(user);
                UserRepository.Save();
                Assert.IsFalse(AllElementsCount == UserRepository.GetAll().Count());

                User ConcreteElement =  UserRepository.Find(user.Id);
                Assert.AreEqual(ConcreteElement, user);

                UserRepository.Remove(user);
                UserRepository.Save();
                Assert.IsTrue(AllElementsCount == UserRepository.GetAll().Count());
            }
        }

        [TestMethod]
        public void BookingRepositoryTest()
        {
            using (DataContext db = new DataContext(options))
            {
                BookingsRepository = new BookingRepository(db);
                booking = new Booking(1, 1)
                {
                    IsTransmitted = true,
                    TransferDate = DateTime.Now
                };

                var AllElements = BookingsRepository.GetAll();
                Assert.IsNotNull(AllElements);
                int AllElementsCount = AllElements.Count();

                BookingsRepository.Add(booking);
                BookingsRepository.Save();
                Assert.IsFalse(AllElementsCount == BookingsRepository.GetAll().Count());

                Booking ConcreteElement = BookingsRepository.Find(booking.Id);
                Assert.AreEqual(ConcreteElement, booking);

                BookingsRepository.Remove(booking);
                BookingsRepository.Save();
                Assert.IsTrue(AllElementsCount == BookingsRepository.GetAll().Count());
            }
        }
    }
}

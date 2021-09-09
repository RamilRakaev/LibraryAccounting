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

namespace LibraryAccounting.Infrastructure.UnitTests
{
    [TestClass]
    public class RepositoryTests
    {
        IRepository<Book> bookRepository;
        IRepository<User> userRepository;
        IRepository<Booking> bookingsRepository;
        Book book;
        User user;
        Booking booking;

        

        [TestMethod]
        public void BookRepositoryTest()
        {
            

            using (DataContext db = new DataContext(Startup.OnConfiguring()))
            {
                book = new Book()
                {
                    Title = "Геном",
                    Author = "Мэтт Ридли",
                    Publisher = "ООО Издетельство \"Эксмо\""
                };

                bookRepository = new BookRepository(db);
                
                var AllElements = bookRepository.GetAll();
                Assert.IsNotNull(AllElements);
                int AllElementsCount = AllElements.Count();

                bookRepository.Add(book);
                bookRepository.Save();
                Assert.IsFalse(AllElementsCount == bookRepository.GetAll().Count());

                Book ConcreteElement = bookRepository.Find(book.Id);
                Assert.AreEqual(ConcreteElement, book);

                bookRepository.Remove(book);
                bookRepository.Save();
                Assert.IsTrue(AllElementsCount == bookRepository.GetAll().Count());
            }
        }

        [TestMethod]
        public void UserRepositoryTest()
        {
            user = new User()
            {
                UserName = "Иван",
                Password = "1234",
                Email = "IVAN228@gmail.com",
                RoleId = 1
            };

            using (DataContext db = new DataContext(Startup.OnConfiguring()))
            {
                userRepository = new UserRepository(db);

                var AllElements = userRepository.GetAll();
                Assert.IsNotNull(AllElements);
                int AllElementsCount = AllElements.Count();

                userRepository.Add(user);
                userRepository.Save();
                Assert.IsFalse(AllElementsCount == userRepository.GetAll().Count());

                User ConcreteElement =  userRepository.Find(user.Id);
                Assert.AreEqual(ConcreteElement, user);

                userRepository.Remove(user);
                userRepository.Save();
                Assert.IsTrue(AllElementsCount == userRepository.GetAll().Count());
            }
        }

        [TestMethod]
        public void BookingRepositoryTest()
        {
            using (DataContext db = new DataContext(Startup.OnConfiguring()))
            {
                bookingsRepository = new BookingRepository(db);
                booking = new Booking(1, 1)
                {
                    IsTransmitted = true,
                    TransferDate = DateTime.Now
                };

                var AllElements = bookingsRepository.GetAll();
                Assert.IsNotNull(AllElements);
                int AllElementsCount = AllElements.Count();

                bookingsRepository.Add(booking);
                bookingsRepository.Save();
                Assert.IsFalse(AllElementsCount == bookingsRepository.GetAll().Count());

                Booking ConcreteElement = bookingsRepository.Find(booking.Id);
                Assert.AreEqual(ConcreteElement, booking);

                bookingsRepository.Remove(booking);
                bookingsRepository.Save();
                Assert.IsTrue(AllElementsCount == bookingsRepository.GetAll().Count());
            }
        }
    }
}

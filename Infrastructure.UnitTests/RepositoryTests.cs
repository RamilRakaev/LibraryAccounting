using Domain.Interfaces;
using Domain.Model;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.UnitTests
{
    [TestClass]
    public class RepositoryTests
    {
        private IRepository<Book> BookRepository;
        private IRepository<PersonAccount> PersonRepository;
        private IRepository<Booking> BookingsRepository;
        readonly private DbContextOptions<DataContext> options = new DbContextOptionsBuilder<DataContext>().
                UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=LibraryAccounting;Trusted_Connection=True;").Options;
        private IElement IRepositoryTest<Element>(IRepository<Element> repository, Element element)
        {
            var AllElements = repository.GetAll();
            Assert.IsNotNull(AllElements);
            int AllElementsCount = AllElements.Count();

            repository.Add(element);
            repository.Save();
            Assert.IsFalse(AllElementsCount == repository.GetAll().Count());

            IElement ConcreteElement = (IElement)element;
            ConcreteElement = (IElement)repository.Find(ConcreteElement.Id);
            Assert.AreEqual(ConcreteElement, element);

            repository.Remove(element);
            repository.Save();
            Assert.IsTrue(AllElementsCount == repository.GetAll().Count());

            return ConcreteElement;
        }

        [TestMethod]
        public void TestAllRepositories()
        {
            using (DataContext db = new DataContext(options))
            {
                BookRepository = new BookRepository(db);
                PersonRepository = new PersonRepository(db);
                BookingsRepository = new BookingsRepository(db);
                Book book = new Book()
                {
                    Title = "Геном",
                    Author = "Мэтт Ридли",
                    Publisher = "ООО Издетельство \"Эксмо\""
                };
                PersonAccount person = new PersonAccount()
                {
                    Name = "Иван",
                    Password = "1234",
                    Email = "IVAN228@gmail.com"
                };
                book = (Book)IRepositoryTest(BookRepository, book);
                person = (PersonAccount)IRepositoryTest(PersonRepository, person);
                Booking booking = new Booking()
                {
                    BookId = book.Id,
                    PersonAccountId = person.Id,
                    IsTransmitted = true,
                    IsReturned = false,
                    TransferDate = DateTime.Today
                };
                IRepositoryTest(BookingsRepository, booking);
            }
        }
    }
}

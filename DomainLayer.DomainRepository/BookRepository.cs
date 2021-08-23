using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using System.Collections.Generic;

namespace Infrastructure.Repositories
{
    public class BookRepository:IRepository<Book>
    {
        readonly private DataContext db;

        public BookRepository(DataContext context)
        {
            db = context;
        }

        public IEnumerable<Book> GetAll()
        {
            return db.Books;
        }

        public Book Find(int id)
        {
            return db.Books.Find(id);
        }

        public void Add(Book element)
        {
            db.Books.Add(element);
        }

        public void Remove(Book element)
        {
            db.Books.Remove(element);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}

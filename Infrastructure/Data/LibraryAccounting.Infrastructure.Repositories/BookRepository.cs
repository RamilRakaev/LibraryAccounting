using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAccounting.Infrastructure.Repositories
{
    public class BookRepository : IRepository<Book>
    {
        readonly private DataContext db;

        public BookRepository(DataContext context)
        {
            db = context;
        }

        public IEnumerable<Book> GetAll()
        {
            var result = new List<Book>(db.Books);
            return result;
        }

        public IQueryable<Book> GetAllAsQueryable()
        {
            return db.Books.AsQueryable();
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
            if (db.Books.Contains(element))
                db.Books.Remove(element);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public async Task<Book> FindAsync(int id)
        {
            return await db.Books.FindAsync(id);
        }

        public async Task AddAsync(Book element)
        {
            await db.Books.AddAsync(element);
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}

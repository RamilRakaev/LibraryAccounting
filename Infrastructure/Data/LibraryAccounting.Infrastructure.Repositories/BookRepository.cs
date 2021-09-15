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

        public IQueryable<Book> GetAll()
        {
            return db.Set<Book>().AsQueryable();
        }

        public IQueryable<Book> GetAllAsNoTracking()
        {
            return db.Set<Book>().AsNoTracking();
        }

        public async Task<Book> FindAsync(int id)
        {
            return await db.Set<Book>().FindAsync(id);
        }

        public async Task AddAsync(Book element)
        {
            await db.Set<Book>().AddAsync(element);
        }

        public async Task RemoveAsync(Book element)
        {
            if (db.Set<Book>().Contains(element))
                await Task.Run(() => db.Set<Book>().Remove(element));
        }

        public async Task RemoveRangeAsync(IEnumerable<Book> elements)
        {
            await Task.Run(() => db.Set<Book>().RemoveRange(elements));
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}

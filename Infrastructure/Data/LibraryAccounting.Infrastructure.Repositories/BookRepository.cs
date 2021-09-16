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
            return db.Set<Book>().Include(b => new { b.Genre, b.Author }).AsQueryable();
        }

        public List<Book> GetAllAsNoTracking()
        {
            return db.Set<Book>()
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .Include(b => b.Booking).Include(b => b.Booking.Client)
                .AsNoTracking().ToList();
        }

        public async Task<Book> FindAsync(int id)
        {
            return await db.Set<Book>()
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .Include(b => b.Booking)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task AddAsync(Book element)
        {
            await db.Set<Book>().AddAsync(element);
        }

        public async Task RemoveAsync(Book element)
        {
            if (await db.Set<Book>().ContainsAsync(element))
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

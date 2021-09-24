using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAccounting.Infrastructure.Repositories
{
    public class AuthorRepository : IRepository<BookAuthor>
    {
        readonly private DataContext db;

        public AuthorRepository(DataContext context)
        {
            db = context;
        }

        public IQueryable<BookAuthor> GetAll()
        {
            return db.Set<BookAuthor>().AsQueryable();
        }

        public List<BookAuthor> GetAllAsNoTracking()
        {
            return db.Set<BookAuthor>()
                .Include(a => a.Books)
                .Include(a => a.Genres)
                .AsNoTracking().ToList();
        }

        public async Task<BookAuthor> FindNoTrackingAsync(int id)
        {
            return await db.Set<BookAuthor>()
                .Include(a => a.Books)
                .Include(a => a.Genres)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<BookAuthor> FindAsync(int id)
        {
            return await db.Set<BookAuthor>()
                .Include(a => a.Books)
                .Include(a => a.Genres)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task AddAsync(BookAuthor element)
        {
            await db.Set<BookAuthor>().AddAsync(element);
        }

        public async Task RemoveAsync(BookAuthor element)
        {
            if (await db.Set<BookAuthor>().ContainsAsync(element))
                await Task.Run(() => db.Set<BookAuthor>().Remove(element));
        }

        public async Task RemoveRangeAsync(IEnumerable<BookAuthor> elements)
        {
            await Task.Run(() => db.Set<BookAuthor>().RemoveRange(elements));
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}

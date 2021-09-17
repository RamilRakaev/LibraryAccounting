using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAccounting.Infrastructure.Repositories
{
    public class AuthorRepository : IRepository<Author>
    {
        readonly private DataContext db;

        public AuthorRepository(DataContext context)
        {
            db = context;
        }

        public IQueryable<Author> GetAll()
        {
            return db.Set<Author>().AsQueryable();
        }

        public List<Author> GetAllAsNoTracking()
        {
            return db.Set<Author>()
                .Include(a => a.Books)
                .Include(a => a.Genres)
                .AsNoTracking().ToList();
        }

        public async Task<Author> FindNoTrackingAsync(int id)
        {
            return await db.Set<Author>()
                .Include(a => a.Books)
                .Include(a => a.Genres)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Author> FindAsync(int id)
        {
            return await db.Set<Author>()
                .Include(a => a.Books)
                .Include(a => a.Genres)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task AddAsync(Author element)
        {
            await db.Set<Author>().AddAsync(element);
        }

        public async Task RemoveAsync(Author element)
        {
            if (await db.Set<Author>().ContainsAsync(element))
                await Task.Run(() => db.Set<Author>().Remove(element));
        }

        public async Task RemoveRangeAsync(IEnumerable<Author> elements)
        {
            await Task.Run(() => db.Set<Author>().RemoveRange(elements));
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}

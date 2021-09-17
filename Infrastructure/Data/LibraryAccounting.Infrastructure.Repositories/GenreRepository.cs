using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAccounting.Infrastructure.Repositories
{
    public class GenreRepository : IRepository<Genre>
    {
        private readonly DataContext db;

        public GenreRepository(DataContext context)
        {
            db = context;
        }

        public IQueryable<Genre> GetAll()
        {
            return db.Set<Genre>().AsQueryable();
        }

        public List<Genre> GetAllAsNoTracking()
        {
            return db.Set<Genre>()
                .Include(g => g.Books)
                .Include(g => g.Authors)
                .AsNoTracking().ToList();
        }

        public async Task<Genre> FindNoTrackingAsync(int id)
        {
            return await db.Set<Genre>()
                .Include(g => g.Books)
                .Include(g => g.Authors)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Genre> FindAsync(int id)
        {
            return await db.Set<Genre>()
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task AddAsync(Genre element)
        {
            await db.Set<Genre>().AddAsync(element);
        }

        public async Task RemoveAsync(Genre element)
        {
            if (await db.Set<Genre>().ContainsAsync(element))
                await Task.Run(() => db.Remove(element));
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}

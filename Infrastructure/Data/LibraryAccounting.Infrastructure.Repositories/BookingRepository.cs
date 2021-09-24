using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAccounting.Infrastructure.Repositories
{
    public class BookingRepository : IRepository<Booking>
    {
        readonly private DataContext db;

        public BookingRepository(DataContext context)
        {
            db = context;
        }

        public IQueryable<Booking> GetAll()
        {
            return db.Set<Booking>().AsQueryable();
        }

        public List<Booking> GetAllAsNoTracking()
        {
            return db.Set<Booking>()
                .Include(b => b.Book)
                .Include(b => b.Client)
                .AsNoTracking().ToList();
        }

        public async Task<Booking> FindNoTrackingAsync(int id)
        {
            return await db.Set<Booking>()
                .Include(b => b.Book)
                .Include(b => b.Client)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Booking> FindAsync(int id)
        {
            return await db.Set<Booking>()
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task AddAsync(Booking element)
        {
            await db.Set<Booking>().AddAsync(element);
        }

        public async Task RemoveAsync(Booking element)
        {
            if (db.Set<Booking>().Contains(element))
                await Task.Run(() => db.Set<Booking>().Remove(element));
        }

        public async Task RemoveRangeAsync(IEnumerable<Booking> elements)
        {
            await Task.Run(() => db.Set<Booking>().RemoveRange(elements));
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}

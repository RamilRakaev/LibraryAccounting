using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryAccounting.Infrastructure.RepositoriesAsync
{
    public class BookingRepositoryAsync : IRepositoryAsync<Booking>
    {
        readonly private DataContext db;

        public BookingRepositoryAsync(DataContext context)
        {
            db = context;
        }

        public Task AddAsync(Booking element)
        {
            await db.Add
        }

        public Task<Booking> FindAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Booking>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Booking element)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}

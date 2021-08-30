using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LibraryAccounting.Infrastructure.Repositories
{
    public class BookingRepository : IRepository<Booking>
    {
        readonly private DataContext db;

        public BookingRepository(DataContext context)
        {
            db = context;
        }

        public IEnumerable<Booking> GetAll()
        {
            return db.Bookings;
        }

        public Booking Find(int id)
        {
            return db.Bookings.Find(id);
        }

        public void Add(Booking element)
        {
            db.Bookings.Add(element);
        }

        public void Remove(Booking element)
        {
            if (db.Bookings.Contains(element))
                db.Bookings.Remove(element);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}

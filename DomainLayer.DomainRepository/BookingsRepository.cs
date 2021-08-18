using Domain.Interfaces;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class BookingsRepository : IRepository<Booking>
    {
        readonly private DataContext db;

        public BookingsRepository(DataContext context)
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
            db.Bookings.Remove(element);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}

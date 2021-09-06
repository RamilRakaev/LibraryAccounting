﻿using LibraryAccounting.Infrastructure.Handlers;
using LibraryAccounting.Infrastructure.Repositories;
using LibraryAccounting.Services.ToolInterfaces;
using LibraryAccounting.Infrastructure.Tools;
using Microsoft.EntityFrameworkCore;
using Quartz;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAccounting.Timer
{
    public class DeletingOverdueBookings : IJob
    {
        readonly private IClientTools ClientTools;
        public DeletingOverdueBookings()
        {
            var db = new DataContext();
            ClientTools = new ClientTools(new BookingRepository(db), new BookRepository(db));
        }
        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Run(() => Search());
        }

        void Search()
        {
            var booksId = new ExpiredBooksIdHandler(BookingDeleteShedule.Days).Handle(ClientTools.GetAllBookings()).Select(b => b.Id); 
            foreach (int id in booksId)
            {
                ClientTools.RemoveReservation(id);
            }
        } 
    }
}

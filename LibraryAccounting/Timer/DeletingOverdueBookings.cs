using LibraryAccounting.Infrastructure.Handlers;
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
        readonly private IClientTools clientTools;
        public DeletingOverdueBookings()
        {
            var db = new DataContext();
            clientTools = new ClientTools(new BookingRepository(db), new BookRepository(db));
        }
        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Run(() => Search());
        }

        void Search()
        {
            var booksId = new ExpiredBooksIdHandler(BookingDeleteShedule.Days).Handle(clientTools.GetAllBookings()).Select(b => b.Id); 
            foreach (int id in booksId)
            {
                clientTools.RemoveReservation(id);
            }
        } 
    }
}

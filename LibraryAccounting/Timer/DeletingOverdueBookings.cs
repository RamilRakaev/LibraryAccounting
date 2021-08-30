using LibraryAccounting.Infrastructure.Handlers;
using LibraryAccounting.Infrastructure.Repositories;
using LibraryAccounting.Domain.ToolInterfaces;
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
        readonly private DbContextOptions<DataContext> options = new DbContextOptionsBuilder<DataContext>().
                UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=LibraryAccounting;Trusted_Connection=True;").Options;
        public DeletingOverdueBookings()
        {
            var db = new DataContext(options);
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

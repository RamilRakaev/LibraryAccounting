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
        readonly private IClientTools _clientTools;
        public DeletingOverdueBookings(IClientTools clientTools)
        {
            _clientTools = clientTools;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Run(() => Search());
        }

        void Search()
        {
            var booksId = new ExpiredBooksIdHandler(BookingDeleteShedule.Days).Handle(_clientTools.GetAllBookings()).Select(b => b.Id);
            foreach (int id in booksId)
            {
                _clientTools.RemoveReservation(id);
            }
        }
    }
}

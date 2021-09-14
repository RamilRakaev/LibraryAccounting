using Quartz;
using System.Threading.Tasks;
using MediatR;
using System;
using LibraryAccounting.CQRSInfrastructure.Methods.BookingMethods;

namespace LibraryAccounting.Timer
{
    public class DeletingOverdueBookings : IJob
    {
        private readonly IMediator _mediator;

        public DeletingOverdueBookings(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        public async Task Execute(IJobExecutionContext context)
        {
            await _mediator.Send(new DeletingExpiredBooksCommand());
        }
    }
}

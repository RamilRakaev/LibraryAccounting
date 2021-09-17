using FluentValidation;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.BookingMethods
{
    public class RemoveBookingCommand : IRequest<Booking>
    {
        public int Id { get; set; }

        public RemoveBookingCommand(int id)
        {
            Id = id;
        }
    }

    public class RemoveBookingHandler : IRequestHandler<RemoveBookingCommand, Booking>
    {
        private readonly IRepository<Booking> _repository;

        public RemoveBookingHandler(IRepository<Booking> repository)
        {
            _repository = repository;
        }

        public async Task<Booking> Handle(RemoveBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = await Task.Run(() =>
            _repository.GetAll()
                .FirstOrDefault(b => b.Id == request.Id));
            await _repository.RemoveAsync(booking);
            await _repository.SaveAsync();
            return booking;
        }
    }

    public class RemoveBookingValidator : AbstractValidator<RemoveBookingCommand>
    {
        public RemoveBookingValidator()
        {
            RuleFor(b => b.Id).NotNull().NotEqual(0);
        }
    }
}

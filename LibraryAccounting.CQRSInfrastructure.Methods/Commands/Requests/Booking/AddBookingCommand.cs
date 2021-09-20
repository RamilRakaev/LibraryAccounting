using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests
{
    public class AddBookingCommand : IRequest<Booking>
    {
        public int BookId { get; set; }
        public int ClientId { get; set; }

        public AddBookingCommand(int bookId, int clientId)
        {
            BookId = bookId;
            ClientId = clientId;
        }
    }
}

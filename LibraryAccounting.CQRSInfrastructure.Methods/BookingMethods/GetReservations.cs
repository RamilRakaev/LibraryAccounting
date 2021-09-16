﻿using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.BookingMethods
{
    public class GetReservationsQuery : IRequest<IEnumerable<Booking>>
    {
        public int CliendId { get; set; }
    }

    public class GetReservationsHandler : IRequestHandler<GetReservationsQuery, IEnumerable<Booking>>
    {
        private readonly IRepository<Booking> _db;

        public GetReservationsHandler(IRepository<Booking> db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Booking>> Handle(GetReservationsQuery request, CancellationToken cancellationToken)
        {
            QueryFilter<Booking, GetReservationsQuery> queryFilter
                = new QueryFilter<Booking, GetReservationsQuery>(_db.GetAllAsNoTracking());
            return await queryFilter.FilterAsync(request);
        }
    }
}

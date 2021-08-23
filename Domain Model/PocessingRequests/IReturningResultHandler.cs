using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryAccounting.Domain.Interfaces.PocessingRequests
{
    interface IReturningResultHandler<Output, Input> : IRequestsHandlerComponent<Input>
    {
        public Output Result();
    }
}

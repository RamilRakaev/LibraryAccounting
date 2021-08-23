using System.Collections.Generic;

namespace LibraryAccounting.Domain.Interfaces.PocessingRequests
{
    public interface IRequestsHandlerComponent<Element>
    {
        public void Handle(ref IEnumerable<Element> elements);
    }
}

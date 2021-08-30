using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAccounting.Domain.Interfaces.PocessingRequests
{
    public interface IReturningResultHandler<Output, Input> 
    {
        public Output Handle(IEnumerable<Input> elements);
    }

    public interface IReturningResultHandler<Output, Input1, Input2> 
    {
        public Output Handle(IEnumerable<Input1> elements1, IEnumerable<Input2> elements2);
    }
}

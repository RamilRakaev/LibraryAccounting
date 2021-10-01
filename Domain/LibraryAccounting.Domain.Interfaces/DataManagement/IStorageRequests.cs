using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAccounting.Domain.Interfaces.DataManagement
{
    public interface IStorageRequests<Element>
    {
        public Task<Element> FindNoTrackingAsync(int id);

        public Task<Element> FindAsync(int id);

        public IQueryable<Element> GetAll();

        public IQueryable<Element> GetAllAsNoTracking();
    }
}

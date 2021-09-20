using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using System;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Handlers
{
    public class BookHandler
    {
        protected readonly IRepository<Book> _db;

        public BookHandler(IRepository<Book> db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(IRepository<Book>));
        }
    }
}
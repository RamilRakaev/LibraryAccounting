using LibraryAccounting.CQRSInfrastructure.LibrarianReports;
using LibraryAccounting.CQRSInfrastructure.Methods.Commands.Requests;
using LibraryAccounting.Domain.Interfaces.DataManagement;
using LibraryAccounting.Domain.Model;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods.Commands.Handlers
{
    public class LibrarianExcelReportHandler : IRequestHandler<LibrarianExcelReportCommand, bool>
    {
        private readonly IRepository<Booking> _db;

        public LibrarianExcelReportHandler(IRepository<Booking> db)
        {
            _db = db;
        }

        public async Task<bool> Handle(LibrarianExcelReportCommand request, CancellationToken cancellationToken)
        {
            List<object> list = new List<object>();
            foreach (var booking in _db.GetAllAsNoTracking())
            {
                var obj = new { booking.BookingDate, booking.Book.Title, booking.Client.UserName, booking.Client.Email, booking.IsTransmitted };
                list.Add(obj);
            }
            WritingObjectsInExcel excel = new WritingObjectsInExcel(new Dictionary<string, string>() {
                    { "BookingDate", "Дата бронировки" },
                    { "Title", "Название книги" },
                    { "UserName", "Имя пользователя" },
                    { "Email", "Почта"},
                    { "IsTransmitted", "Передано" }
                },
                new Dictionary<string, string>() { { "True", "да" }, { "False", "нет" } });

            excel.CreateSheetHeader();
            excel.CreateSheetData(list.ToArray());
            await Task.Run(() => excel.SaveDocument("Отчёт для библиотекаря"));
            return excel.Success;
        }
    }
}

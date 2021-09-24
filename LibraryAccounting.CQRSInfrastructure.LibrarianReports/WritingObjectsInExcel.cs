using DocumentFormat.OpenXml.Spreadsheet;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LibraryAccounting.CQRSInfrastructure.LibrarianReports
{
    public class WritingObjectsInExcel
    {
        private readonly List<string> values;
        private readonly Dictionary<string, string> _headers;
        private readonly Dictionary<string, string> _replacedValues;
        private readonly ExcelReport excelReport;
        private readonly List<Row> rows;

        public WritingObjectsInExcel(
            Dictionary<string, string> headers,
            Dictionary<string, string> replacedValues,
            string path = null)
        {
            values = new List<string>();
            _headers = headers;
            _replacedValues = replacedValues;
            excelReport = path == null ? new ExcelReport() : new ExcelReport(path);
            rows = new List<Row>();
        }

        public void CreateSheetHeader()
        {
            rows.Add(excelReport.CreateRow(_headers.Values.ToArray(), true));
        }

        public void CreateSheetData(object[] bookings)
        {
            foreach (var booking in bookings)
            {
                FillTheRow(booking, rows);
            }
        }

        public void SaveDocument(string title)
        {
            excelReport.CreateExcelDocument(
                new Sheet[]
                {
                    excelReport.CreateSheet(rows.ToArray(), title)
                }
            );
        }

        private void FillTheRow(object booking, List<Row> rows)
        {
            var properties = booking.GetType().GetProperties();
            foreach (var header in _headers.Keys)
            {
                var property = properties.FirstOrDefault(p => p.Name == header);
                if (property != null)
                {
                    AddValue(property, booking);
                }
            }
            rows.Add(excelReport.CreateRow(values.ToArray(), true));
            values.Clear();
        }

        private void AddValue(PropertyInfo property, object obj)
        {
            var value = property.GetValue(obj);
            if (value != null)
            {
                var replacedValueKey = _replacedValues.Keys.FirstOrDefault(v => v == value.ToString());
                if (replacedValueKey == null)
                {
                    values.Add(value.ToString());
                }
                else
                {
                    values.Add(_replacedValues.GetValueOrDefault(replacedValueKey));
                }
            }
            else
            {
                values.Add(string.Empty);
            }
        }
    }
}

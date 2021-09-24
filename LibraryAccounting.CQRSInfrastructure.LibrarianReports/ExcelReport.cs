using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;

namespace LibraryAccounting.CQRSInfrastructure.LibrarianReports
{
    public class ExcelReport
    {
        private readonly string _path = Directory.GetCurrentDirectory() + "/Librarian report.xlsx";

        private SpreadsheetDocument xl;
        private WorkbookPart wbp;
        private Workbook wb;
        private FileVersion fv;
        private Sheets _sheets;
        private int currentColumn = 65;
        private uint currentRow = 1;
        private bool isInitialized;

        public List<Exception> Exceptions { get; private set; }
        public bool Success { get; private set; }

        public ExcelReport()
        {
            Initial();
        }

        public ExcelReport(string path)
        {
            _path = path;
            Initial();
        }

        private char CurrentColumn
        {
            get
            {
                if (currentColumn > 90)
                    currentColumn = 65;
                return (char)currentColumn++;
            }
        }

        private UInt32Value CurrentRow
        {
            get
            {
                return new UInt32Value(currentRow++);
            }
        }

        private void ResetColumn()
        {
            currentColumn = 65;
        }

        private void ResetRow()
        {
            currentRow = 1;
        }

        private void Initial()
        {
            if (isInitialized == false)
            {
                xl = SpreadsheetDocument.Create(_path, SpreadsheetDocumentType.Workbook);
                wbp = xl.AddWorkbookPart();
                wb = new Workbook();
                fv = new FileVersion()
                {
                    ApplicationName = "Microsoft Office Excel"
                };
                _sheets = new Sheets();

                Exceptions = new List<Exception>();
                Success = true;
                isInitialized = true;
            }
        }

        public Row CreateRow(string[] cells, bool resetColumn = false, bool resetRow = false)
        {
            Row row = new Row()
            {
                RowIndex = CurrentRow
            };
            foreach (var cellText in cells)
            {
                Cell cell = new Cell
                {
                    CellReference = CurrentColumn + currentRow.ToString(),
                    DataType = CellValues.String,
                    CellValue = new CellValue(cellText)
                };
                row.Append(cell);
            }
            if (resetColumn)
            {
                ResetColumn();
            }
            if (resetRow)
            {
                ResetRow();
            }
            return row;
        }

        public Sheet CreateSheet(Row[] rows, string name)
        {
            try
            {
                WorksheetPart wsp = wbp.AddNewPart<WorksheetPart>();
                Worksheet ws = new Worksheet();
                SheetData sd = new SheetData();
                sd.Append(rows);
                ws.Append(sd);
                wsp.Worksheet = ws;
                wsp.Worksheet.Save();
                Sheet sheet = new Sheet
                {
                    Name = name,
                    SheetId = 3,
                    Id = wbp.GetIdOfPart(wsp)
                };
                return sheet;
            }
            catch (Exception e)
            {
                Exceptions.Add(e);
                Success = false;
                return null;
            }
        }

        public void CreateExcelDocument(Sheet[] sheets)
        {
            try
            {
                _sheets.Append(sheets);
                wb.Append(fv);
                wb.Append(_sheets);

                xl.WorkbookPart.Workbook = wb;
                Save();
            }
            catch (Exception e)
            {
                Exceptions.Add(e);
                Success = false;
            }
        }

        private void Save()
        {
            xl.WorkbookPart.Workbook.Save();
            xl.Close();
        }
    }
}
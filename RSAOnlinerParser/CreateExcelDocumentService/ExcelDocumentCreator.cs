using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Office.Interop.Excel;
using Preferences;
using Preferences.Interfaces;
using Preferences.Models;

namespace CreateExcelDocumentService
{
    public class ExcelDocumentCreator : ICreateExcelDocumentService
    {
        public void Create(IEnumerable<SoughtItem> items)
        {
            var workBook = CreateBook(items);
            SaveToFile(workBook);
        }

        private static void SaveToFile(Workbook workBook)
        {
            var path = "";
            var name = "";
            try
            {
                path = CreateExcelDocPreferences.CreatedDocumentDirectory;
                name = CreateExcelDocPreferences.CreatedDocumentName;
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                workBook.SaveAs(path + name);
            }
            catch (Exception)
            {
                throw new Exception($"Can't save Excel to file {path + name}");
            }
        }

        private static Workbook CreateBook(IEnumerable<SoughtItem> items)
        {
            Workbook workBook;
            Worksheet workSheet;
            try
            {
                if (items == null) throw new ArgumentNullException();
                var excelApp = new Application {Visible = true};
                workBook = excelApp.Workbooks.Add();
                workSheet = (Worksheet) workBook.Worksheets.Item[1];
                workSheet.Name = CreateExcelDocPreferences.WorkSheetName;
            }
            catch (Exception)
            {
                throw new Exception("Can't create Excel document");
            }

            var rowCounter = 1;
            foreach (var soughtItem in items)
            {
                workSheet.Cells[rowCounter, 1] = soughtItem.Name;
                workSheet.Cells[rowCounter, 2] = soughtItem.Price;
                workSheet.Cells[rowCounter, 3] = soughtItem.Link;
                rowCounter++;
            }
            return workBook;
        }
    }
}
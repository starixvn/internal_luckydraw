using LuckyDraw.Model;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Documents;

namespace LuckyDraw.Export
{
    public class ResultExporter : IResultExporter
    {
        private readonly List<string> headers;
        protected IWorkbook workbook;
        protected ISheet sheet;
        private const string DefaultSheetName = "Sheet1";

        public ResultExporter()
        {
            headers = new List<string>
            {
                "Prize Index", "Prize Name", "Round Name", "Random"
            };
        }

        public void Export(List<ViewResult> data, string filePath)
        {            
            this.workbook = new XSSFWorkbook();
            this.sheet = this.workbook.CreateSheet(DefaultSheetName);

            CreateHeader();
            WriteData(data);
            SaveFile(filePath);
        }

        private void SaveFile(string filePath)
        {
            FileStream xfile = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            this.workbook.Write(xfile);
            xfile.Close();
        }

        private void CreateHeader()
        {
            var headerStyle = CreateHeaderStyle();
            var header = this.sheet.CreateRow(0);
            for(var i = 0; i < this.headers.Count; i++)
            {
                CreateHeaderCell(header, i, this.headers[i], headerStyle);
            }           
        }

        private ICellStyle CreateHeaderStyle()
        {
            var headerStyle = this.workbook.CreateCellStyle();
            var headerFont = this.workbook.CreateFont();
            headerFont.IsBold = true;
            headerFont.FontHeightInPoints = 12;
            headerFont.Color = HSSFColor.White.Index;

            headerStyle.FillForegroundColor = HSSFColor.BlueGrey.Index;
            headerStyle.FillPattern = FillPattern.SolidForeground;
            headerStyle.SetFont(headerFont);
            headerStyle.Alignment = HorizontalAlignment.Center;

            return headerStyle;
        }

        private void  CreateHeaderCell(IRow header, int index, string cellName, ICellStyle cellStyle)
        {
            var cell = header.CreateCell(index);
            cell.SetCellValue(cellName);
            cell.CellStyle = cellStyle;
            this.sheet.AutoSizeColumn(index);
        }

        private void WriteData(List<ViewResult> data)
        {            
            var cellStyle = this.workbook.CreateCellStyle();
            cellStyle.Alignment = HorizontalAlignment.Center;

            for (int i = 0; i < data.Count; i++)
            {
                var sheetRow = this.sheet.CreateRow(i + 1);
                var item = data[i];
                CreateCellData(0, Convert.ToString(item.PrizeIndex), sheetRow, cellStyle);
                CreateCellData(1, Convert.ToString(item.PrizeName), sheetRow, cellStyle);
                CreateCellData(2, Convert.ToString(item.RoundName), sheetRow, cellStyle);
                CreateCellData(3, Convert.ToString(item.Random), sheetRow, cellStyle);
            }
        }

        private void CreateCellData(int index, string cellvalue, IRow sheetRow, ICellStyle cellStyle)
        {
            ICell Row1 = sheetRow.CreateCell(index);
            Row1.CellStyle = cellStyle;

            if (string.IsNullOrWhiteSpace(cellvalue))
            {
                Row1.SetCellValue(string.Empty);
            }
            else
            {
                Row1.SetCellValue(cellvalue);
            }
        }
    }
}

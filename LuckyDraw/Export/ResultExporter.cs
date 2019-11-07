using LuckyDraw.Model;
using LuckyDraw.Utilities;
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
        private readonly IUtils utils;
        private readonly List<string> headers;
        protected IWorkbook workbook;
        protected ISheet sheet;
        private const string DefaultSheetName = "Sheet1";
        private const int FontSize = 12;
        private const int PrizeIndexCol = 0;
        private const int PrizeNameCol = 1;
        private const int RoundNameCol = 2;
        private const int RandomCol = 3;

        public ResultExporter():this(null)
        {            
        }

        public ResultExporter(IUtils utils)
        {
            this.utils = utils ?? new Utils();
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
            this.utils.SaveExportFile(this.workbook, filePath);            
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
            headerFont.FontHeightInPoints = FontSize;
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
                CreateCellData(PrizeIndexCol, Convert.ToString(item.PrizeIndex), sheetRow, cellStyle);
                CreateCellData(PrizeNameCol, Convert.ToString(item.PrizeName), sheetRow, cellStyle);
                CreateCellData(RoundNameCol, Convert.ToString(item.RoundName), sheetRow, cellStyle);
                CreateCellData(RandomCol, Convert.ToString(item.Random), sheetRow, cellStyle);
            }
        }

        private static void CreateCellData(int index, string cellvalue, IRow sheetRow, ICellStyle cellStyle)
        {
            ICell Row1 = sheetRow.CreateCell(index);
            Row1.CellStyle = cellStyle;
            Row1.SetCellValue(string.IsNullOrWhiteSpace(cellvalue) ? string.Empty : cellvalue);            
        }
    }
}

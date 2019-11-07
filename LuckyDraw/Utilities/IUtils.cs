using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace LuckyDraw.Utilities
{
    public interface IUtils
    {
        BitmapImage PrizeImage(string imageName, int imageSize);
        string BaseDirectory();
        void SaveImageFile(string originPath, string destinationPath);
        long GetFileSize(string filePath);
        void SaveExportFile(IWorkbook workbook, string filePath);
    }
}

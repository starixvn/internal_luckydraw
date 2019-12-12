using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace LuckyDraw.Utilities
{
    public interface IUtils
    {
        BitmapImage PrizeImage(string imageName);
        string BaseDirectory();
        SoundPlayer GetSoundPlayer(string fileName);
        void SaveImageFile(string originPath, string destinationPath);
        long GetFileSize(string filePath);
        void SaveExportFile(IWorkbook workbook, string filePath);
    }
}

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
        BitmapImage PrizeImage(string imageName);
        string BaseDirectory();
        void SaveFile(string originPath, string destinationPath);
        long GetFileSize(string filePath);
    }
}

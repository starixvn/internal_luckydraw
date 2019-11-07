using LuckyDraw.Constants;
using NPOI.SS.UserModel;
using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace LuckyDraw.Utilities
{
    public class Utils : IUtils
    {
        public BitmapImage PrizeImage(string imageName, int imageSize)
        {
            var imagePath = String.Format("{0}{1}", BaseDirectory(), imageName);
            return File.Exists(imagePath) ? BitmapFromUri(new Uri(imagePath), imageSize) : null;
        }

        public string BaseDirectory()
        {
            return String.Format("{0}\\AppData\\Images\\", AppDomain.CurrentDomain.BaseDirectory);
        }

        public void SaveImageFile(string originPath, string destinationPath)
        {
            var image = new BitmapImage(new Uri(originPath));
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            using (var fileStream = new FileStream(destinationPath, FileMode.Create))
            {
                encoder.Save(fileStream);
            }
        }

        public long GetFileSize(string filePath)
        {
            return (new FileInfo(filePath)).Length;
        }

        private static BitmapImage BitmapFromUri(Uri source, int imageSize)
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = source;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            bitmap.DecodePixelHeight = imageSize;
            bitmap.DecodePixelWidth = imageSize;
            bitmap.EndInit();

            return bitmap;
        }

        public void SaveExportFile(IWorkbook workbook, string filePath)
        {
            FileStream xfile = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            workbook.Write(xfile);
            xfile.Close();
        }
    }
}

using LuckyDraw.Constants;
using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace LuckyDraw.Utilities
{
    public static class Utils
    {
        public static BitmapImage BitmapFromUri(Uri source)
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = source;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            bitmap.DecodePixelHeight = Consts.ImageHeight;
            bitmap.DecodePixelWidth = Consts.ImageWidth;
            bitmap.EndInit();

            return bitmap;
        }

        public static string BaseDirectory()
        {
            return String.Format("{0}\\AppData\\Images\\", AppDomain.CurrentDomain.BaseDirectory);
        }

        public static void SaveFile(string originPath, string destinationPath)
        {
            var image = new BitmapImage(new Uri(originPath));
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            using (var fileStream = new FileStream(destinationPath, FileMode.Create))
            {
                encoder.Save(fileStream);
            }
        }

        public static long GetFileSize(string filePath)
        {
            return (new FileInfo(filePath)).Length;
        }
    }
}

using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace LuckyDraw.Model
{
    public class ViewPrize
    {      
        public int Id { get; set; }
        public string PrizeName { get; set; }
        public string RoundName { get; set; }
        public int RoundIndex { get; set; }
        public int PrizeIndex { get; set; }

        public BitmapImage TaskImg
        {
            get
            {
                var path = String.Format("{0}\\AppData\\Images\\{1}.png", AppDomain.CurrentDomain.BaseDirectory, Id.ToString());                
                BitmapImage image = null;

                if (File.Exists(path))
                {
                    image = BitmapFromUri(new Uri(path));
                }

                return image;
            }
        }

        private static BitmapImage BitmapFromUri(Uri source)
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = source;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.CreateOptions = BitmapCreateOptions.IgnoreImageCache;

            bitmap.DecodePixelHeight = Constants.ImageHeight;
            bitmap.DecodePixelWidth = Constants.ImageWidth;
            bitmap.EndInit();
            return bitmap;
        }
    }
}

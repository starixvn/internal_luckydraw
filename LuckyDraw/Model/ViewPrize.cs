using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
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
                var directoryPath = AppDomain.CurrentDomain.BaseDirectory + @"\AppData\Images";
                var path = directoryPath + @"\" + Id.ToString() + @".png";


                BitmapImage image = null;

                if (File.Exists(path))
                {
                    image = BitmapFromUri(new Uri(path));
                }


                return image;
            }
        }

        private BitmapImage BitmapFromUri(Uri source)
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = source;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.CreateOptions = BitmapCreateOptions.IgnoreImageCache;

            bitmap.DecodePixelHeight = 50;
            bitmap.DecodePixelWidth = 50;
            bitmap.EndInit();
            return bitmap;
        }
    }
}

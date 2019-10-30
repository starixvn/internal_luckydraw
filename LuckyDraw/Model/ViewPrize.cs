using LuckyDraw.Utilities;
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
                var imagePath = String.Format("{0}{1}.png", Utils.BaseDirectory(), Id.ToString());                
                return File.Exists(imagePath) ? Utils.BitmapFromUri(new Uri(imagePath)) : null;
            }
        }
    }
}

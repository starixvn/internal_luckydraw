using LuckyDraw.Constants;
using LuckyDraw.Utilities;
using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace LuckyDraw.Model
{
    public class ViewPrize
    {
        private readonly IUtils utils;
        public ViewPrize()
        {
            utils = new Utils();
        }

        public int Id { get; set; }
        public string PrizeName { get; set; }
        public string RoundName { get; set; }
        public int RoundIndex { get; set; }
        public int PrizeIndex { get; set; }

        public BitmapImage PrizeImage
        {
            get
            {
                return utils.PrizeImage(String.Format("{0}.png", Id.ToString()));
            }
        }
    }
}

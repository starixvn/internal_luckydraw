using LuckyDraw.Constants;
using LuckyDraw.Utilities;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace LuckyDraw.Views
{
    /// <summary>
    /// Interaction logic for Item.xaml
    /// </summary>
    public partial class Item : UserControl
    {
        private readonly IUtils utils;

        public Item()
        {
            InitializeComponent();
            this.utils = new Utils();
        }      

        public void LoadItemData(Result itemData)
        {
            this.textPrize.Text = itemData.PrizeName;
            this.textRandom.Text = itemData.Random == string.Empty ? "000" : itemData.Random;
            var image = utils.PrizeImage(itemData.PrizeId.ToString() + ".png", Consts.HomeImageSize);
            this.img.ImageSource = image ?? utils.PrizeImage("prize.png", Consts.HomeImageSize);
        }

        public async Task RunRandom(string random, int randomTime)
        {
            const int two = 2;
            await AnimateRandom(Consts.RangeThreeDigits, "000", string.Empty, this.textRandom, randomTime);
            await AnimateRandom(Consts.RangeTwoDigits, "00", random.Substring(0, 1), this.textRandom, randomTime);
            await AnimateRandom(Consts.RangeOneDigit, "0", random.Substring(0, two), this.textRandom, randomTime);
            this.textRandom.Text = random;
        }

        [SuppressMessage("csharpsquid", "S2325:Methods and properties that don't access instance data can be static to prevent any misunderstanding about the contract of the method.")]
        private async Task AnimateRandom(int range, string format, string subRandom, TextBlock l, int randomTime)
        {
            Random rnd = new Random();
            var startTime = DateTime.Now;
            for (var i = 0; i < Consts.MaxAnimationLoops; i++)
            {
                int random = rnd.Next(1, range);
                await Task.Delay(Consts.AnimationDelay);
                l.Text = subRandom + random.ToString(format);

                if ((DateTime.Now - startTime).TotalMilliseconds > randomTime)
                {
                    return;
                }
            }
        }

        public void BlurItem()
        {
            var opacity = 0.4;
            this.textPrize.Opacity = opacity;
            this.textRandom.Opacity = opacity;
            this.img.Opacity = opacity;
            this.border1.Opacity = opacity;
            this.border2.Opacity = opacity;
            this.border3.Opacity = opacity;
            this.border4.Opacity = opacity;
        }
    }
}

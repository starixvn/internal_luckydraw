using LuckyDraw.Constants;
using LuckyDraw.Utilities;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Media;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

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

            if (this.textRandom.Text != "000")
            {
                this.textRandom.FontSize = 34;                
            }

            var image = utils.PrizeImage(itemData.PrizeId.ToString() + ".png");
            this.img.Source = image ?? utils.PrizeImage("prize.png");
        }

        public async Task RunRandom(string random, int randomTime)
        {
            await AnimateStarting();

            var player1 = this.utils.GetSoundPlayer("spin.wav");
            player1.Play();            

            const int two = 2;
            await AnimateRandom(Consts.RangeThreeDigits, "000", string.Empty, this.textRandom, randomTime, Consts.AnimationDelay1);
            await AnimateRandom(Consts.RangeTwoDigits, "00", random.Substring(0, 1), this.textRandom, randomTime, Consts.AnimationDelay2);
            await AnimateRandom(Consts.RangeOneDigit, "0", random.Substring(0, two), this.textRandom, randomTime, Consts.AnimationDelay3);
            this.textRandom.Text = random;
            await Task.Delay(200);

            player1.Stop();
            var player2 = this.utils.GetSoundPlayer("ding.wav");
            player2.Play();

            await AnimateFinishing();
        }

        private async Task AnimateStarting()
        {
            await Task.Delay(500);
            for (var i = 0; i < 8; i++)
            {
                var margin = this.textRandom.Margin;
                margin.Left += 1;
                margin.Right += 1;
                this.textRandom.Margin = margin;
                await Task.Delay(40);
            }
            await Task.Delay(100);
            for (var i = 0; i < 8; i++)
            {
                var margin = this.textRandom.Margin;
                margin.Left -= 1;
                margin.Right -= 1;
                this.textRandom.Margin = margin;
                this.textRandom.FontSize += 1;
                await Task.Delay(40);
            }
            await Task.Delay(100);
        }

        private async Task AnimateFinishing()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.Start();
            await Task.Delay(500);
            timer.Stop();
        }

        // The timer's Tick event.
        private bool BlinkOn = false;
        private void timer_Tick(object sender, EventArgs e)
        {
            var converter = new System.Windows.Media.BrushConverter();
            if (BlinkOn)
            {
                this.textRandom.Foreground = (Brush)converter.ConvertFromString("#ffd33b");
            }
            else {                 
                this.textRandom.Foreground = Brushes.LawnGreen;
            }
            BlinkOn = !BlinkOn;
        }

        [SuppressMessage("csharpsquid", "S2325:Methods and properties that don't access instance data can be static to prevent any misunderstanding about the contract of the method.")]
        private async Task AnimateRandom(int range, string format, string subRandom, TextBlock l, int randomTime, int delay)
        {
            Random rnd = new Random();
            var startTime = DateTime.Now;
            for (var i = 0; i < Consts.MaxAnimationLoops; i++)
            {
                int random = rnd.Next(1, range);
                await Task.Delay(delay);
                l.Text = subRandom + random.ToString(format);

                if ((DateTime.Now - startTime).TotalMilliseconds > randomTime)
                {
                    return;
                }
            }
        }

        public void BlurItem()
        {
            const double opacity = 0.4;
            this.textPrize.Opacity = opacity;
            this.textRandom.Opacity = opacity;
            this.img.Opacity = opacity;
            this.arrowLeft.Opacity = opacity;
            this.arrowRight.Opacity = opacity;            
            this.canvasImage.Opacity = opacity;
        }
    }
}

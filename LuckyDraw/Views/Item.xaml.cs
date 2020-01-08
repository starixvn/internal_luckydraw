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
            this.textPrize.Text = itemData.PrizeName.ToUpper();
            this.textRandom.Text = itemData.Random == string.Empty ? "000" : itemData.Random;
            const int fontSize = 73;

            if (this.textRandom.Text != "000")
            {
                var converter = new System.Windows.Media.BrushConverter();
                this.textRandom.Foreground = (Brush)converter.ConvertFromString("#ffd33b");
                this.textRandom.FontSize = fontSize;
            }

            var image = utils.PrizeImage(itemData.PrizeId.ToString() + ".png");
            this.img.Source = image ?? utils.PrizeImage("prize.png");
        }

        public async Task RunRandom(string random, int randomTime)
        {           
            await AnimateStarting();
            var converter = new System.Windows.Media.BrushConverter();
            this.textRandom.Foreground = (Brush)converter.ConvertFromString("#ffd33b");                

            const int two = 2;
            var delay1 = await AnimateRandom(Consts.RangeThreeDigits, "000", string.Empty, this.textRandom, randomTime, Consts.AnimationDelay1);
            var delay2 = await AnimateRandom(Consts.RangeTwoDigits, "00", random.Substring(0, 1), this.textRandom, randomTime, delay1);
            await AnimateRandom(Consts.RangeOneDigit, "0", random.Substring(0, two), this.textRandom, randomTime, delay2);
            this.textRandom.Text = random;

            var player2 = this.utils.GetSoundPlayer("ding.mp3");
            player2.Play();
            const int delay = 200;
            await Task.Delay(delay);

            await AnimateFinishing();
        }

        private async Task AnimateStarting()
        {
            const int delay1 = 500;
            const int loop = 10;
            await Task.Delay(delay1);
            for (var i = 0; i < loop; i++)
            {
                var margin = this.textRandom.Margin;
                margin.Left += 1;
                margin.Right += 1;
                this.textRandom.Margin = margin;
                const int delay4 = 5;
                await Task.Delay(delay4);
            }
            const int delay2 = 300;
            await Task.Delay(delay2);
            const double deltaMargin = 0.85;
            for (var i = 0; i < loop; i++)
            {                
                var margin = this.textRandom.Margin;
                margin.Left -= deltaMargin;
                margin.Right -= deltaMargin;
                this.textRandom.Margin = margin;               

                this.textRandom.FontSize += 1;
                const int delay5 = 5;
                await Task.Delay(delay5);
            }
            const int delay3 = 500;
            await Task.Delay(delay3);
        }

        private async Task AnimateFinishing()
        {
            const int delay1 = 100;
            const int delay2 = 500;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, delay1);
            timer.Start();
            await Task.Delay(delay2);
            timer.Stop();
        }

        private bool BlinkOn;
        private void timer_Tick(object sender, EventArgs e)
        {
            var converter = new System.Windows.Media.BrushConverter();
            if (BlinkOn)
            {
                this.textRandom.Foreground = (Brush)converter.ConvertFromString("#ffd33b");
            }
            else {                 
                this.textRandom.Foreground = Brushes.Orange;
            }
            BlinkOn = !BlinkOn;
        }

        [SuppressMessage("csharpsquid", "S2325:Methods and properties that don't access instance data can be static to prevent any misunderstanding about the contract of the method.")]
        private async Task<int> AnimateRandom(int range, string format, string subRandom, TextBlock l, int randomTime, int delay)
        {
            const int maxDelay = 160;
            const int two = 2;
            Random rnd = new Random();
            var startTime = DateTime.Now;
            for (var i = 0; i < Consts.MaxAnimationLoops; i++)
            {
                if(i % two == 0 && delay < maxDelay)
                {
                    delay += 1;
                }                

                int random = rnd.Next(1, range);
                
               
                l.Text = subRandom + random.ToString(format);
                await Task.Delay(delay);

                if ((DateTime.Now - startTime).TotalMilliseconds > randomTime)
                {
                    return delay;
                }
            }

            return delay;
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

        public void UpdateRandom(string random)
        {
            const int fontSize = 73;
            this.textRandom.Text = random;
            var converter = new System.Windows.Media.BrushConverter();
            this.textRandom.Foreground = (Brush)converter.ConvertFromString("#ffd33b");
            this.textRandom.FontSize = fontSize;

            this.arrowLeft.Width = 10;
            this.arrowLeft.Height = 30;
            this.arrowRight.Width = 10;
            this.arrowRight.Height = 30;
        }
    }
}

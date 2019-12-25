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
            const int fontSize = 34;

            if (this.textRandom.Text != "000")
            {
                var converter = new System.Windows.Media.BrushConverter();
                this.textRandom.Foreground = (Brush)converter.ConvertFromString("#ffd33b");
                this.textRandom.FontSize = fontSize;

                this.arrowLeft.Width = 6;
                this.arrowLeft.Height = 18;
                this.arrowRight.Width = 6;
                this.arrowRight.Height = 18;

                var margin1 = this.arrowLeft.Margin;
                margin1.Top += 1;
                this.arrowLeft.Margin = margin1;

                var margin2 = this.arrowRight.Margin;
                margin2.Top += 1;
                this.arrowRight.Margin = margin2;
            }

            var image = utils.PrizeImage(itemData.PrizeId.ToString() + ".png");
            this.img.Source = image ?? utils.PrizeImage("prize.png");
        }

        public async Task RunRandom(string random, int randomTime)
        {           
            await AnimateStarting();
            var converter = new System.Windows.Media.BrushConverter();
            this.textRandom.Foreground = (Brush)converter.ConvertFromString("#ffd33b");

            var player0 = this.utils.GetSoundPlayer("spin.mp3");
            player0.Play();            

            const int two = 2;
            var delay1 = await AnimateRandom(Consts.RangeThreeDigits, "000", string.Empty, this.textRandom, randomTime, Consts.AnimationDelay1);
            var delay2 = await AnimateRandom(Consts.RangeTwoDigits, "00", random.Substring(0, 1), this.textRandom, randomTime, delay1);
            await AnimateRandom(Consts.RangeOneDigit, "0", random.Substring(0, two), this.textRandom, randomTime, delay2);
            this.textRandom.Text = random;

            player0.Stop();
            var player2 = this.utils.GetSoundPlayer("ding.mp3");
            player2.Play();
            const int delay = 200;
            await Task.Delay(delay);

            await AnimateFinishing();
        }

        private async Task AnimateStarting()
        {
            const int delay1 = 500;
            const int loop = 8;
            await Task.Delay(delay1);
            for (var i = 0; i < loop; i++)
            {
                var margin = this.textRandom.Margin;
                margin.Left += 1;
                margin.Right += 1;
                this.textRandom.Margin = margin;
                const int delay4 = 40;

                this.arrowLeft.Width += 0.25;
                this.arrowLeft.Height += 0.75;
                this.arrowRight.Width += 0.25;
                this.arrowRight.Height += 0.75;

                if (i == loop - 1)
                {
                    var margin1 = this.arrowLeft.Margin;
                    margin1.Top += 1;
                    this.arrowLeft.Margin = margin1;

                    var margin2 = this.arrowRight.Margin;
                    margin2.Top += 1;
                    this.arrowRight.Margin = margin2;
                }

                await Task.Delay(delay4);
            }
            const int delay2 = 100;
            await Task.Delay(delay2);
            for (var i = 0; i < loop; i++)
            {                
                var margin = this.textRandom.Margin;
                margin.Left -= 0.85;
                margin.Right -= 0.85;
                this.textRandom.Margin = margin;               

                this.textRandom.FontSize += 1;
                const int delay5 = 40;
                await Task.Delay(delay5);
            }
            const int delay3 = 100;
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
    }
}

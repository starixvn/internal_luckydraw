using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using YearEndParty.Core;
using YearEndParty.Presentation.Controls;

namespace YearEndParty.Presentation
{
    public partial class PrizeList : Form
    {
        //maximum rows & columns
        private const int cashPrizeColumn = 3;
        private const int cashPrizeRow = 3;
        private const int majorPrizeColumn = 4;
        private const int majorPrizeRow = 2;
        private const int grandPrizeColumn = 4;
        private const int grandPrizeRow = 3;

        //width & height of image
        private const int WidthImage = 1900;
        private const int HeightImage = 1140;

        //width & height of screen
        private int WidthScreen = 0;
        private int HeightScreen = 0;

        public delegate void TalkBackDelegate(string message);
        public TalkBackDelegate TalkBack;

        public bool IsClosed { get; set; }
        public PrizeTypes PrizeType { get; set; }
        public int Multiply { get; set; }

        private WinnerDetails prizeDetail { get; set; }
        private SuperGrandWinnerDetails supergrandDetail { get; set; }
        private SuperGrandWinnerSpecialDetails supergrandSpecialDetail { get; set; }

        private bodb_luckyEntities context = new bodb_luckyEntities();

        public PrizeList()
        {
            DoubleBuffered = true;
            InitializeComponent();
        }

        /// <summary>
        /// this function is "form load finished --> show form"
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        private IList<PrizeTile> _prizeTiles;
        private int _currentPrize;

        private void PrizeList_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)26: //ctrl+z to back
                    context = new bodb_luckyEntities();
                    var setting1 = context.PrizeSettings.First(i => i.PrizeType == (int)PrizeType);
                    if (Multiply == 3)
                    {
                        if (setting1.Quantity > setting1.CurrentPrize * 3)
                        {
                            setting1.CurrentPrize++;
                            context.SaveChanges();
                        }
                    }
                    else
                    {
                        if (setting1.Quantity > setting1.CurrentPrize)
                        {
                            setting1.CurrentPrize += 3;
                            context.SaveChanges();
                        }
                    }
                    break;

                case (char)25: //ctrl+y to next
                    context = new bodb_luckyEntities();
                    var setting2 = context.PrizeSettings.First(i => i.PrizeType == (int)PrizeType);
                    if (Multiply == 3)
                    {
                        if (0 < setting2.CurrentPrize)
                        {
                            setting2.CurrentPrize--;
                            context.SaveChanges();
                        }
                    }
                    else
                    {
                        if (0 < setting2.CurrentPrize)
                        {
                            setting2.CurrentPrize -= 3;
                            context.SaveChanges();
                        }
                    }
                    
                    break;
                case (char)27: //Esc
                    IsClosed = true;
                    Close();
                    break;
                case (char)115: //s
                    var list = new LuckyList { PrizeType = (int)PrizeType };
                    list.Show();
                    break;
                case (char)13: //enter
                    {
                        context = new bodb_luckyEntities();
                        var setting = context.PrizeSettings.First(i => i.PrizeType == (int)PrizeType);
                        if (setting.CurrentPrize == 0) return;
                        switch (Multiply)
                        {
                            case 3:
                                pictureBox1.Visible = true;
                                
                                prizeDetail = new WinnerDetails
                                {
                                    PrizeIndex = setting.CurrentPrize,
                                    PrizeType = PrizeType,
                                    TalkBack = TalkBackToClient
                                };
                                prizeDetail.ShowDialog();

                                var next = setting.CurrentPrize - 1;
                                _prizeTiles[next].SetBackground(next + 1, true);
                                pictureBox1.Visible = false;
                                break;
                            case 1:
                                pictureBox1.Visible = true;

                                if (setting.CurrentPrize > 3)
                                {
                                    supergrandDetail = new SuperGrandWinnerDetails
                                    {
                                        PrizeIndex = setting.CurrentPrize,
                                        TalkBack = TalkBackToClient
                                    };

                                    supergrandDetail.ShowDialog();
                                    var settingTemp = context.PrizeSettings.FirstOrDefault(i => i.PrizeType == (int)PrizeTypes.SuperGrand);

                                    for (var i = settingTemp.CurrentPrize - 3; i < settingTemp.Quantity; i++)
                                    {
                                        _prizeTiles[i].SetBackground(i + 1, true);
                                    }
                                }
                                else
                                {
                                    supergrandSpecialDetail = new SuperGrandWinnerSpecialDetails
                                    {
                                        PrizeIndex = setting.CurrentPrize,
                                        TalkBack = TalkBackToClient
                                    };
                                    supergrandSpecialDetail.ShowDialog();

                                    for (var i = setting.CurrentPrize; i < setting.Quantity; i++)
                                    {
                                        _prizeTiles[i].SetBackground(i + 1, true);
                                    }
                                    _prizeTiles[0].SetBackground(1, true);
                                    _prizeTiles[1].SetBackground(2, true);
                                    _prizeTiles[2].SetBackground(3, true);
                                }
                                
                                pictureBox1.Visible = false;
                                break;
                        }
                    }
                    break;
            }
        }

        private void PrizeList_Load(object sender, System.EventArgs e)
        {
            WidthScreen = Screen.PrimaryScreen.Bounds.Width;
            HeightScreen = Screen.PrimaryScreen.Bounds.Height;

            IsClosed = false;
            Visible = false;

            var prizes = context.Prizes.Where(i => i.PrizeType == (int)PrizeType).ToList();

            var setting = context.PrizeSettings.First(i => i.PrizeType == (int)PrizeType);
            var total = setting.Quantity / Multiply;
            _currentPrize = setting.CurrentPrize;

            // Load Background
            switch (PrizeType)
            {
                case PrizeTypes.Cash:
                    BackgroundImage = Properties.Resources.Cash_1900x1140;
                    RenderListCashPrize(total, prizes);
                    break;
                case PrizeTypes.Major:
                    BackgroundImage = Properties.Resources.Major_1900x1140;
                    RenderListMajorPrize(total, prizes);
                    break;
                case PrizeTypes.Grand:
                    BackgroundImage = Properties.Resources.Grand_1900x1140;
                    RenderListGrandPrize(total, prizes);
                    break;
                case PrizeTypes.SuperGrand:
                    BackgroundImage = Properties.Resources.Super_Grand_1900x1140;
                    RenderSuperGrandPrize(total, prizes);
                    break;
            }

            Visible = true;
        }

        public void Barcode(string barcode)
        {
            if (prizeDetail != null && !prizeDetail.IsClosed)
                prizeDetail.Barcode(barcode);
            else if (supergrandDetail != null && !supergrandDetail.IsClosed)
                supergrandDetail.Barcode(barcode);
            else if (supergrandSpecialDetail != null && !supergrandSpecialDetail.IsClosed)
                supergrandSpecialDetail.Barcode(barcode);
        }

        private void TalkBackToClient(string message)
        {
            TalkBack(message);
        }

        private void RenderListCashPrize(int total, List<Prizes> prizes)
        {
            // Top & Left of the first item
            var marginTop = 300 * HeightScreen / HeightImage;
            var marginLeft = 413 * WidthScreen / WidthImage;

            // width & Height of one item
            var height = 174 * HeightScreen / HeightImage;
            var width = 365 * WidthScreen / WidthImage;

            // space width & height
            var spaceX = 6;
            var spaceY = 5;

            _prizeTiles = new List<PrizeTile>(total);

            for (var i = 0; i < cashPrizeRow; i++)
            {
                for (var j = 0; j < cashPrizeColumn; j++)
                {
                    var index = (i * cashPrizeColumn + j);
                    if (index < total)
                    {
                        var tile = new PrizeTile(index + 1, PrizeType, index + 1 > _currentPrize)
                        {
                            Location = new Point(
                                marginLeft + j * width + j * spaceX * WidthScreen / WidthImage,
                                marginTop + i * height + i * spaceY * HeightScreen / HeightImage
                                )
                        };
                        tile.Width = width;
                        tile.Height = height;

                        Controls.Add(tile);
                        _prizeTiles.Add(tile);
                    }
                }
            }
        }

        private void RenderListMajorPrize(int total, List<Prizes> prizes)
        {
            // Top & Left of the first item
            var marginTop = 496 * HeightScreen / HeightImage;
            var marginLeft = 226 * WidthScreen / WidthImage;

            // width & Height of one item
            var height = 176 * HeightScreen / HeightImage;
            var width = 365 * WidthScreen / WidthImage;

            // space width & height
            var spaceX = 6;
            var spaceY = 5;

            _prizeTiles = new List<PrizeTile>(total);

            for (var i = 0; i < majorPrizeRow; i++)
            {
                for (var j = 0; j < majorPrizeColumn; j++)
                {
                    var index = (i * majorPrizeColumn + j);
                    if (index < total)
                    {
                        int color;
                        if ((i % 2) == 0) color = j;
                        else color = majorPrizeColumn - j - 1;

                        var tile = new PrizeTile(index + 1, PrizeType, index + 1 > _currentPrize)
                        {
                            Location = new Point(
                                marginLeft + j * width + j * spaceX * WidthScreen / WidthImage,
                                marginTop + i * height + i * spaceY * HeightScreen / HeightImage
                                )
                        };
                        tile.Width = width;
                        tile.Height = height;

                        Controls.Add(tile);
                        _prizeTiles.Add(tile);
                    }
                }
            }
        }

        private void RenderListGrandPrize(int total, List<Prizes> prizes)
        {
            // Top & Left of the first item
            var marginTop = 326 * HeightScreen / HeightImage;
            var marginLeft = 228 * WidthScreen / WidthImage;

            // width & Height of one item
            var height = 174 * HeightScreen / HeightImage;
            var width = 362 * WidthScreen / WidthImage;

            // space width & height
            var spaceX = 6;
            var spaceY = 5;

            _prizeTiles = new List<PrizeTile>(total);
            var index = 0;
            for (var i = 0; i < grandPrizeRow; i++)
            {
                for (var j = 0; j < grandPrizeColumn; j++)
                {
                    if ((i == 0 && j != 0 && j != 3) || i != 0)
                    {
                        if (index < total)
                        {
                            int color;
                            if ((i % 2) == 0) color = j;
                            else color = grandPrizeColumn - j - 1;

                            var tile = new PrizeTile(index + 1, PrizeType, index + 1 > _currentPrize)
                            {
                                Location = new Point(
                                    marginLeft + j * width + j * spaceX * WidthScreen / WidthImage,
                                    marginTop + i * height + i * spaceY * HeightScreen / HeightImage
                                    )
                            };
                            tile.Width = width;
                            tile.Height = height;

                            Controls.Add(tile);
                            _prizeTiles.Add(tile);
                        }

                        index++;
                    }
                }
            }
        }

        private void RenderSuperGrandPrize(int total, List<Prizes> prizes)
        {
            // Top & Left of the first item
            var marginTop = 298 * HeightScreen / HeightImage;
            var marginLeft = 406 * WidthScreen / WidthImage;

            // width & Height of one item
            var height = 175 * HeightScreen / HeightImage;
            var width = 368 * WidthScreen / WidthImage;

            // space width & height
            var spaceX = 6;
            var spaceY = 5;

            _prizeTiles = new List<PrizeTile>(total);

            for (var i = 0; i < cashPrizeRow; i++)
            {
                for (var j = 0; j < cashPrizeColumn; j++)
                {
                    var index = (i * cashPrizeColumn + j);
                    if (index < total)
                    {
                        var tile = new PrizeTile(index + 1, PrizeType, index + 1 > _currentPrize)
                        {
                            Location = new Point(
                                marginLeft + j * width + j * spaceX * WidthScreen / WidthImage,
                                marginTop + i * height + i * spaceY * HeightScreen / HeightImage
                                )
                        };
                        tile.Width = width;
                        tile.Height = height;

                        Controls.Add(tile);
                        _prizeTiles.Add(tile);
                    }
                }
            }
        }
    }
}
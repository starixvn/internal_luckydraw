using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Objects;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using log4net;
using YearEndParty.Core;
using System.Globalization;

namespace YearEndParty.Presentation
{
    public partial class AdhocPrize : Form
    {
        //rows and colums
        private const int Row = 2; 
        private const int Col = 4;
        private const int MaxInScreen = 8;

        //width & height of image
        private const int WidthImage = 1408;
        private const int HeightImage = 640;

        //width & height of screen
        private int WidthScreen = 0;
        private int HeightScreen = 0;

        public delegate void TalkBackDelegate(string message);
        public TalkBackDelegate TalkBack;
        private static readonly ILog Log = LogManager.GetLogger("Cheat");

        public bool IsClosed { get; set; }
        private int _prizeId;
        /// <summary>
        /// track winners' barcodes
        /// </summary>
        private IList<string> _winners = new List<string>();
        private bodb_luckyEntities _context;

        private IList<PictureBox> pictures = new List<PictureBox>();
        private IList<Label> nicks = new List<Label>();
        private IList<Microsoft.VisualBasic.PowerPacks.LineShape> lines = new List<Microsoft.VisualBasic.PowerPacks.LineShape>();
        private IList<Label> barcodes = new List<Label>();
        private IList<Label> names = new List<Label>();
        private IList<Label> depts = new List<Label>();

        private int _prizedTotal;
        private int _quantity;

        public AdhocPrize()
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

        private void AdhocPrize_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)27:
                    IsClosed = true;
                    Close();
                    break;
                case (char)49:
                    RemoveWinner(0);
                    break;
                case (char)50:
                    RemoveWinner(1);
                    break;
                case (char)51:
                    RemoveWinner(2);
                    break;
                case (char)52:
                    RemoveWinner(3);
                    break;
                case (char)53:
                    RemoveWinner(4);
                    break;
                case (char)54:
                    RemoveWinner(5);
                    break;
                case (char)55:
                    RemoveWinner(6);
                    break;
                case (char)56:
                    RemoveWinner(7);
                    break;
                //case (char)57:
                //    RemoveWinner(8);
                //    break;
                //case (char)48:
                //    RemoveWinner(9);
                    //break;
                case (char)115:
                    var list = new LuckyList { PrizeType = (int)PrizeTypes.AdHoc };
                    list.Show();
                    break;
            }
        }

        public void Barcode(string barcode)
        {
            _context.PrizeSettings.MergeOption = MergeOption.OverwriteChanges;
            var setting = _context.PrizeSettings.First(i => i.PrizeType == (int)PrizeTypes.AdHoc);
            //there are no more price for staffs
            if (_prizedTotal >= setting.Quantity)
            {
                TalkBack("Full" + ", barcode: " + barcode);
                return;
            }

            if (_winners.Any(i => i == barcode))
            {
                //cheat? heh... write log
                Log.Fatal("AdHoc | Barcode duplicated, ID = " + barcode);
                TalkBack("Barcode duplicated" + ", barcode: " + barcode);
                return;
            }

            var staff = _context.Staffs.First(i => i.StaffCode == barcode);
            var dept = _context.Departments.First(i => i.DepartmentId == staff.DepartmentId);

            bool won;
            if (!setting.DuplicateAllow)
            {
                won = (from i in _context.Winners
                       join j in _context.Prizes on i.PrizeId equals j.PrizeId
                       join k in _context.PrizeSettings on j.PrizeType equals k.PrizeType
                       where i.StaffId == staff.StaffId &&
                             k.PrizeType >= 1 && k.PrizeType <= 5
                       select i).Any();

            }
            else
            {
                //duplicate allow
                won = _context.Winners.Any(i => i.PrizeId == _prizeId && i.StaffId == staff.StaffId);
            }

            if (won)
            {
                //cheat? heh... write log
                Log.Fatal("AdHoc | Barcode duplicated, ID = " + barcode);
                TalkBack("Barcode duplicated" + ", barcode: " + barcode);
                return;
            }
            var color = GetColor(barcode.Substring(0, 2));

            labelName.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(staff.FullName.ToLower()); ;
            labelNick.Text = string.IsNullOrWhiteSpace(staff.Nick) ? staff.FirstName.ToUpper() : staff.Nick.ToUpper();
            labelDept.Text = dept.ShortName.ToUpper();
            lineMain.Visible = true;
            lineMain.BorderColor = color;
            labelDept.ForeColor = Color.FromArgb(255, 208, 150, 23);

            pictureBoxWinner.Image = Utilities.ScaleImage(staff.Avatar, pictureBoxWinner.Width, pictureBoxWinner.Height);


            var index = -1;
            for (var i = 0; i < MaxInScreen; i++)
            {
                if (string.IsNullOrWhiteSpace(barcodes[i].Text))
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                ScrollWinners();
                index = MaxInScreen - 1;
            }

            pictures[index].Image = Utilities.ScaleImage(staff.Avatar, pictures[index].Width, pictures[index].Height);
            barcodes[index].Text = staff.StaffCode;
            barcodes[index].BackColor = color;
            names[index].Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(staff.FullName.ToLower());
            nicks[index].Text = staff.Nick.ToUpper();
            lines[index].BorderColor = color;
            depts[index].Text = dept.ShortName.ToUpper();
            depts[index++].ForeColor = Color.FromArgb(255, 208, 150, 23);

            //save to db
            var winner = new Winners
            {
                PrizeId = _prizeId,
                StaffId = staff.StaffId,
                CreatedDate = DateTime.Now
            };
            _context.Winners.AddObject(winner);
            _winners.Add(barcode);
            _context.SaveChanges();
            _prizedTotal++;

           // lableStatus.Text = string.Format("{0}|{1}", _prizedTotal, _quantity);
            lableStatus1.Text = _prizedTotal.ToString().PadLeft(2, '0');
            lableStatus2.Text = _quantity.ToString();
        }

        private void AdhocPrize_Load(object sender, EventArgs e)
        {
            // UI is followed of image which designer provider
            // With Image in LuckyDraw 2012 has 2 rows. with 1 row has 4 winners --> screen is only show 8 winners
            WidthScreen = Screen.PrimaryScreen.Bounds.Width;
            HeightScreen = Screen.PrimaryScreen.Bounds.Height;

            var nickSize = int.Parse(ConfigurationManager.AppSettings["Adhoc_NickSize"]);
            var nameSize = int.Parse(ConfigurationManager.AppSettings["Adhoc_NameSize"]);
            var deptSize = int.Parse(ConfigurationManager.AppSettings["Adhoc_DeptSize"]);
            var statusSize = int.Parse(ConfigurationManager.AppSettings["Adhoc_StatusSize"]);

            labelNick.Font = new Font(labelNick.Font.Name, nickSize, labelNick.Font.Style);
            labelName.Font = new Font(labelName.Font.Name, nameSize, labelName.Font.Style);
            labelDept.Font = new Font(labelDept.Font.Name, deptSize, labelDept.Font.Style);
            lableStatus1.Font = new Font(lableStatus1.Font.Name, statusSize, lableStatus1.Font.Style);
            lableStatus2.Font = new Font(lableStatus2.Font.Name, statusSize, lableStatus2.Font.Style);

            nickSize = int.Parse(ConfigurationManager.AppSettings["AdhocList_NickSize"]);
            nameSize = int.Parse(ConfigurationManager.AppSettings["AdhocList_NameSize"]);
            deptSize = int.Parse(ConfigurationManager.AppSettings["AdhocList_DeptSize"]);
            
            //top & left of Main winner follow image
            var top1 = 75;
            var left1 = 150;
            //width & height of Main winner follow image
            var height1 = 258;
            var width1 = 245;
            //top & left of list winners follow image
            var left2 = 460;
            var top2 = 75;
            //width & height of list winners follow image
            var height2 = 128;
            var width2 = 122;
            //width space & height space of list winners follow image
            var spaceX = 34;
            var spaceY = 138;

            Visible = false;
            IsClosed = false;

            #region Main winner
            //top & left of Main winners
            var top = top1 * HeightScreen / HeightImage;
            var left = left1 * WidthScreen / WidthImage;

            //image
            pictureBoxWinner.Left = left;
            pictureBoxWinner.Top = top;
            pictureBoxWinner.Height = height1 * HeightScreen / HeightImage;
            pictureBoxWinner.Width = width1 * WidthScreen / WidthImage;

            //border = 3
            pictureBox1.Top = pictureBoxWinner.Top - 3 ; //3: for border = 3
            pictureBox1.Left = pictureBoxWinner.Left - 3 ;
            pictureBox1.Width = pictureBoxWinner.Width + 6; //6: for border = 3 
            pictureBox1.Height = pictureBoxWinner.Height + 6;

            //Nick
            labelNick.Left = left;
            labelNick.Top = top + pictureBoxWinner.Height + 10 * HeightScreen / HeightImage; // 10: space
            labelNick.Width = pictureBoxWinner.Width;
            labelNick.Height = 50 * HeightScreen / HeightImage; // 50: height of Nick

            //Line
            lineMain.BorderColor = Color.Gray;
            lineMain.BorderWidth = 3 * HeightScreen / HeightImage;
            lineMain.X1 = left;
            lineMain.Y1 = labelNick.Top + labelNick.Height + 1 * HeightScreen / HeightImage;
            lineMain.X2 = left + pictureBoxWinner.Width;
            lineMain.Y2 = labelNick.Top + labelNick.Height + 1 * HeightScreen / HeightImage;

            //Name
            labelName.Left = left;
            labelName.Top = labelNick.Top + labelNick.Height + 10 * HeightScreen / HeightImage; // 10: space
            labelName.Width = pictureBoxWinner.Width;
            labelName.Height = 60 * HeightScreen / HeightImage; // 60: height of Name
            
            //Department
            labelDept.Left = left;
            labelDept.Top = labelName.Top + labelName.Height;
            labelDept.Width = pictureBoxWinner.Width;
            labelDept.Height = 44 * HeightScreen / HeightImage; // 44: height of Dept

            //Status Index of Adhoc
            pnl_Status.Left = left;
            pnl_Status.Top = labelDept.Top + labelDept.Height + 5 * HeightScreen / HeightImage; // 5: space
            pnl_Status.Width = pictureBoxWinner.Width;

            lableStatus1.Width = pictureBoxWinner.Width / 2 - 4;

            lineStatus.X1 = lableStatus1.Right + 1;
            lineStatus.Y1 = lableStatus1.Top + 10;
            lineStatus.X2 = lableStatus1.Right + 1;
            lineStatus.Y2 = lableStatus1.Bottom -10;

            lableStatus2.Left = pictureBoxWinner.Width / 2 + 9;
            lableStatus2.Width = pictureBoxWinner.Width / 2 - 3;

            #endregion

            #region list winners

            #region load all control

            left = left2 * WidthScreen / WidthImage;
            for (var r = 0; r < Row; r++)
            {
                top = top2 * HeightScreen / HeightImage + r * height2 * HeightScreen / HeightImage + (spaceY * HeightScreen / HeightImage) * r;
                for (var c = 0; c < Col; c++)
                {
                    var pictureBackground = new PictureBox
                    {
                        BackgroundImage = Properties.Resources.smallsquare,
                        BackColor = Color.Transparent,
                        Location = new Point
                        {
                            X = left + c * (width2 + spaceX) * WidthScreen / WidthImage  - 3,
                            Y = top -3
                        },
                        Size = new Size((width2 * WidthScreen / WidthImage + 6), (height2 * HeightScreen / HeightImage + 6))
                    };

                    var picture = new PictureBox
                                      {
                                          //Width = 120,
                                          //Height = 130,
                                          BackgroundImageLayout = ImageLayout.Stretch,
                                          BackgroundImage = Properties.Resources.unknown_big,
                                          BackColor = Color.Transparent,
                                          Location = new Point
                                                         {
                                                             X = left + c * (width2 + spaceX) * WidthScreen / WidthImage,
                                                             Y = top
                                                         },
                                          Size = new Size((width2 * WidthScreen / WidthImage), (height2 * HeightScreen / HeightImage))
                                      };

                    //barcode: visible
                    var barcode = new Label
                    {
                        BackColor = Color.Transparent,
                        ForeColor = Color.Yellow,
                        Padding = new Padding(0),
                        AutoSize = false,
                        Margin = new Padding(0),
                        TextAlign = ContentAlignment.TopLeft,
                        Location = new Point
                        {
                            //X = left + c * (width2 + spaceX),
                            X = picture.Left,
                            Y = top + height2 + 2 //2: space
                        },
                        Font = new Font("Tahoma", nickSize, FontStyle.Regular, GraphicsUnit.Point, 0)
                    };

                    //nick
                    var nick = new Label
                    {
                        BackColor = Color.Transparent,
                        ForeColor = Color.Yellow,
                        Padding = new Padding(0),
                        AutoSize = false,
                        Margin = new Padding(0),
                        TextAlign = ContentAlignment.TopLeft,
                        Location = new Point
                        {
                            X = picture.Left,
                            Y = pictureBackground.Bottom + 10 * HeightScreen / HeightImage //10: space
                        },
                        Size = new Size(picture.Width, (20 * HeightScreen / HeightImage)),
                        Font = new Font("Tahoma", nickSize, FontStyle.Bold, GraphicsUnit.Point, 0)
                    };
                    
                    //line
                    var line = new Microsoft.VisualBasic.PowerPacks.LineShape
                    {
                        BorderColor = Color.Gray,
                        BorderWidth = 2 * HeightScreen / HeightImage,
                        X1 = picture.Left,
                        Y1 = nick.Bottom + 9 * HeightScreen / HeightImage,
                        X2 = picture.Left + picture.Width,
                        Y2 = nick.Bottom + 9 * HeightScreen / HeightImage,
                    };

                    //name
                    var name = new Label
                    {
                        BackColor = Color.Transparent,
                        ForeColor = Color.Yellow,
                        Padding = new Padding(0),
                        AutoSize = false,
                        Margin = new Padding(0),
                        TextAlign = ContentAlignment.TopLeft,
                        Location = new Point
                        {
                            X = picture.Left,
                            Y = nick.Bottom + 15 * HeightScreen / HeightImage
                        },
                        Size = new Size(picture.Width, (40 * HeightScreen / HeightImage)),
                        Font = new Font("Tahoma", nameSize, FontStyle.Regular, GraphicsUnit.Point, 0)
                    };

                    //department
                    var dept = new Label
                    {
                        BackColor = Color.Transparent,
                        ForeColor = Color.Yellow,
                        Padding = new Padding(0),
                        AutoSize = false,
                        Margin = new Padding(0),
                        TextAlign = ContentAlignment.MiddleLeft,
                        Location = new Point
                        {
                            X = picture.Left,
                            Y = name.Bottom + 15 * HeightScreen / HeightImage
                        },
                        Size = new Size(picture.Width, (20 * HeightScreen / HeightImage)),
                        Font = new Font("Tahoma", deptSize, FontStyle.Bold, GraphicsUnit.Point, 0)
                    };

                    pictures.Add(picture);
                    nicks.Add(nick);
                    lines.Add(line);
                    barcodes.Add(barcode);
                    names.Add(name);
                    depts.Add(dept);
                    Controls.Add(picture);
                    Controls.Add(pictureBackground);
                    
                    Controls.Add(nick);
                    this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            line});
                    Controls.Add(name);
                    Controls.Add(dept);
                }
            }

            #endregion

            #region Fill all winners

            _context = new bodb_luckyEntities();
            var setting = _context.PrizeSettings.First(i => i.PrizeType == (int)PrizeTypes.AdHoc);
            _quantity = setting.Quantity;

            _prizeId = _context.Prizes.First(i => i.PrizeType == (int)PrizeTypes.AdHoc).PrizeId;

            var winners = _context.Winners.Where(i => i.PrizeId == _prizeId).OrderBy(i => i.CreatedDate).ToList();
            _prizedTotal = winners.Count;

            //Fill Status
            lableStatus1.Text = _prizedTotal.ToString().PadLeft(2, '0');
            lableStatus2.Text = _quantity.ToString();

            var index = 0;
            var staffs = (from i in _context.Staffs
                          join j in _context.Winners
                              on i.StaffId equals j.StaffId
                          where j.PrizeId == _prizeId
                          select i).ToList();
            var departments = _context.Departments.ToList();

            foreach (var winner in winners)
            {
                var staff = staffs.FirstOrDefault(i => i.StaffId == winner.StaffId);
                var dept = departments.First(i => i.DepartmentId == staff.DepartmentId);
                if (index < MaxInScreen)
                {
                    var color = GetColor(staff.StaffCode.Substring(0, 2));

                    pictures[index].Image = Utilities.ScaleImage(staff.Avatar, pictures[index].Width, pictures[index].Height);
                    barcodes[index].Text = staff.StaffCode;
                    barcodes[index].BackColor = color;
                    names[index].Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(staff.FullName.ToLower());
                    nicks[index].Text = staff.Nick.ToUpper();
                    lines[index].BorderColor = color;
                    //names[index].ForeColor = color;
                    depts[index].Text = dept.ShortName.ToUpper();
                    depts[index++].ForeColor = Color.FromArgb(255, 208, 150, 23);
                }
                _winners.Add(staff.StaffCode);
            }

            #endregion

            #endregion

            Visible = true;
        }

        private static Color GetColor(string grade)
        {
            switch (grade)
            {
                case "10":
                    return Color.FromArgb(254, 126, 209);
                case "11":
                    return Color.FromArgb(179, 255, 176);
                case "12":
                    return Color.FromArgb(155, 156, 255);
                default:
                    return Color.White;
            }
        }

        private void ScrollWinners()
        {
            for (var i = 0; i < MaxInScreen -1 ; i++)
            {
                pictures[i].Image = pictures[i + 1].Image;
                nicks[i].Text = nicks[i + 1].Text;
                names[i].Text = names[i + 1].Text;
                depts[i].Text = depts[i + 1].Text;
                lines[i].BorderColor = lines[i + 1].BorderColor;
                barcodes[i].Text = barcodes[i + 1].Text;
            }
        }

        private void RemoveWinner(int index)
        {
            if (_winners.Count == 0 || index >= MaxInScreen || string.IsNullOrWhiteSpace(barcodes[index].Text)) return;

            var result = MessageBox.Show(
                "Are you sure you want to remove " + names[index].Text + "  - " + depts[index].Text + " from winner list?",
                "Year End Party 2012",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
                );
            if (result == DialogResult.No)
            {
                return;
            }

            var barcode = barcodes[index].Text;

            pictures[index].Image = null;
            barcodes[index].Text = string.Empty;

            var staffId = _context.Staffs.First(i => i.StaffCode == barcode).StaffId;

            var winner = _context.Winners.First(i => i.StaffId == staffId && i.PrizeId == _prizeId);
            _context.Winners.DeleteObject(winner);
            _context.SaveChanges();
            _winners.Remove(barcode);
            
            FillOut();
            _prizedTotal--;
            lableStatus1.Text = _prizedTotal.ToString().PadLeft(2, '0');
            lableStatus2.Text = _quantity.ToString();
        }

        private void FillOut()
        {
            var empty = barcodes.Any(i => string.IsNullOrWhiteSpace(i.Text)) && _prizedTotal > MaxInScreen;
            if (!empty) return;

            //find staff in winner list but not show on screen
            var barcode = _winners.FirstOrDefault(i => barcodes.All(j => j.Text != i));
            if (string.IsNullOrWhiteSpace(barcode)) return;

            var staff = _context.Staffs.First(i => i.StaffCode == barcode);
            var dept = _context.Departments.First(i => i.DepartmentId == staff.DepartmentId);

            //find location
            var index = 0;
            for (var i = 0; i < MaxInScreen; i++)
            {
                if (string.IsNullOrWhiteSpace(barcodes[i].Text))
                {
                    index = i;
                    break;
                }
            }

            var color = GetColor(barcode.Substring(0, 2));
            barcodes[index].Text = staff.StaffCode;
            //barcodes[index].BackColor = color;
            names[index].Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(staff.FullName.ToLower());
            nicks[index].Text = staff.Nick.ToUpper();
            lines[index].BorderColor = color;
            depts[index].Text = dept.ShortName.ToUpper();
            pictures[index].Image = Utilities.ScaleImage(staff.Avatar, pictures[index].Width, pictures[index].Height);
        }

    }
}
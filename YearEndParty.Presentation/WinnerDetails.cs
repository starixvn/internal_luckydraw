using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using log4net;
using YearEndParty.Core;
using System.Globalization;
using System.Drawing;
using System.IO;
using System.Configuration;

namespace YearEndParty.Presentation
{
    public partial class WinnerDetails : Form
    {
        public delegate void TalkBackDelegate(string message);
        public TalkBackDelegate TalkBack;

        private const int WidthImage = 1350;
        private const int HeightImage = 810;

        //width & height of screen
        private int WidthScreen = 0;
        private int HeightScreen = 0;

        public bool IsClosed { get; set; }
        public int PrizeIndex { get; set; }
        public int Multiply { get; set; }
        public PrizeTypes PrizeType { get; set; }

        private int _prizeId;
        private Dictionary<int, string> _winners;
        private bool _nextPrize;
        private static readonly ILog Log = LogManager.GetLogger("Cheat");
        private bodb_luckyEntities _context;

        public WinnerDetails()
        {
            DoubleBuffered = true;
            InitializeComponent();
            
            log4net.Config.XmlConfigurator.Configure();

        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        private void PrizeDetails_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)13: //enter
                case (char)27: //ESC
                    IsClosed = true;
                    Close();
                    break;
                case (char)49: //1
                    RemoveWinner(1); 
                    break;
                case (char)50: //2
                    RemoveWinner(2);
                    break;
                case (char)51: //3
                    RemoveWinner(3);
                    break;
            }
        }

        private void ResizeControl()
        {
            // Constant below is follow position of control in images which design team provide
            // Example: 82, 2, 790, 634, ...
            var topPrizeImage = 82 * HeightScreen / HeightImage;
            var leftPrizeImage = 2 * WidthScreen / WidthImage;
            var widthPrizeImage = 790 * WidthScreen / WidthImage;
            var heightPrizeImage = 634 * HeightScreen / HeightImage;

            var topPanelPink = 71 * HeightScreen / HeightImage;
            var topPanelGreen = 303 * HeightScreen / HeightImage;
            var topPanelBlue = 535 * HeightScreen / HeightImage;
            var leftPanel = 657 * WidthScreen / WidthImage;
            var widthPanel = 570 * WidthScreen / WidthImage;
            var heightPanel = 189 * HeightScreen / HeightImage;

            var leftItem = 3 * WidthScreen / WidthImage;
            var widthItem = 402 * WidthScreen / WidthImage;
            var topNick = 4 * HeightScreen / HeightImage;
            var heightNick = 40 * HeightScreen / HeightImage;

            var topName = 52 * HeightScreen / HeightImage;
            var heightName = 72 * HeightScreen / HeightImage;

            var topDepartment = 146 * HeightScreen / HeightImage;
            var heightDepartment = 40 * HeightScreen / HeightImage;

            var topImage = 13 * HeightScreen / HeightImage;
            var leftImage = 413 * WidthScreen / WidthImage;
            var widthImage = 151 * WidthScreen / WidthImage;
            var heightImage = 166 * HeightScreen / HeightImage;

            var topBorder = 9 * HeightScreen / HeightImage;
            var leftBorder = 409 * WidthScreen / WidthImage;
            var widthBorder = 158 * WidthScreen / WidthImage;
            var heightBorder = 175 * HeightScreen / HeightImage;

            pnlPrize.Location = new Point(leftPrizeImage, topPrizeImage);
            pnlPrize.Height = heightPrizeImage;
            pnlPrize.Width = widthPrizeImage;

            pnl_Pink.Location = new Point(leftPanel, topPanelPink);
            pnl_Pink.Height = heightPanel;
            pnl_Pink.Width = widthPanel;

            pnl_Green.Location = new Point(leftPanel, topPanelGreen);
            pnl_Green.Height = heightPanel;
            pnl_Green.Width = widthPanel;

            pnl_Blue.Location = new Point(leftPanel, topPanelBlue);
            pnl_Blue.Height = heightPanel;
            pnl_Blue.Width = widthPanel;

            labelNick1.Location = new Point(leftItem, topNick);
            labelNick1.Height = heightNick;
            labelNick1.Width = widthItem;

            labelNick2.Location = new Point(leftItem, topNick);
            labelNick2.Height = heightNick;
            labelNick2.Width = widthItem;

            labelNick3.Location = new Point(leftItem, topNick);
            labelNick3.Height = heightNick;
            labelNick3.Width = widthItem;

            labelName1.Location = new Point(leftItem, topName);
            labelName1.Height = heightName;
            labelName1.Width = widthItem;

            labelName2.Location = new Point(leftItem, topName);
            labelName2.Height = heightName;
            labelName2.Width = widthItem;

            labelName3.Location = new Point(leftItem, topName);
            labelName3.Height = heightName;
            labelName3.Width = widthItem;

            labelDept1.Location = new Point(leftItem, topDepartment);
            labelDept1.Height = heightDepartment;
            labelDept1.Width = widthItem;

            labelDept2.Location = new Point(leftItem, topDepartment);
            labelDept2.Height = heightDepartment;
            labelDept2.Width = widthItem;

            labelDept3.Location = new Point(leftItem, topDepartment);
            labelDept3.Height = heightDepartment;
            labelDept3.Width = widthItem;

            pictureBoxWinner1.Location = new Point(leftImage, topImage);
            pictureBoxWinner1.Height = heightImage;
            pictureBoxWinner1.Width = widthImage;

            pictureBoxWinner2.Location = new Point(leftImage, topImage);
            pictureBoxWinner2.Height = heightImage;
            pictureBoxWinner2.Width = widthImage;

            pictureBoxWinner3.Location = new Point(leftImage, topImage);
            pictureBoxWinner3.Height = heightImage;
            pictureBoxWinner3.Width = widthImage;

            pictureBox1.Location = new Point(leftBorder, topBorder);
            pictureBox1.Height = heightBorder;
            pictureBox1.Width = widthBorder;
            pictureBox2.Location = new Point(leftBorder, topBorder);
            pictureBox2.Height = heightBorder;
            pictureBox2.Width = widthBorder;
            pictureBox3.Location = new Point(leftBorder, topBorder);
            pictureBox3.Height = heightBorder;
            pictureBox3.Width = widthBorder;
        }

        private void PrizeDetails_Load(object sender, EventArgs e)
        {
            WidthScreen = Screen.PrimaryScreen.Bounds.Width;
            HeightScreen = Screen.PrimaryScreen.Bounds.Height;
            ResizeControl();

            var nickSize = int.Parse(ConfigurationManager.AppSettings["Detail_NickSize"]);
            var nameSize = int.Parse(ConfigurationManager.AppSettings["Detail_NameSize"]);
            var deptSize = int.Parse(ConfigurationManager.AppSettings["Detail_DeptSize"]);
            var numberSize = int.Parse(ConfigurationManager.AppSettings["Detail_PrizeNumber"]);

            labelNick1.Font = new Font(labelNick1.Font.Name, nickSize, labelNick1.Font.Style);
            labelNick2.Font = new Font(labelNick2.Font.Name, nickSize, labelNick2.Font.Style);
            labelNick3.Font = new Font(labelNick3.Font.Name, nickSize, labelNick3.Font.Style);

            labelNumber1.Font = new Font(labelNumber1.Font.Name, numberSize, labelNumber1.Font.Style);
            labelNumber2.Font = new Font(labelNumber2.Font.Name, numberSize, labelNumber2.Font.Style);

            labelName1.Font = new Font(labelName1.Font.Name, nameSize, labelName1.Font.Style);
            labelName2.Font = new Font(labelName2.Font.Name, nameSize, labelName2.Font.Style);
            labelName3.Font = new Font(labelName3.Font.Name, nameSize, labelName3.Font.Style);

            labelDept1.Font = new Font(labelDept1.Font.Name, deptSize, labelDept1.Font.Style);
            labelDept2.Font = new Font(labelDept2.Font.Name, deptSize, labelDept2.Font.Style);
            labelDept3.Font = new Font(labelDept3.Font.Name, deptSize, labelDept3.Font.Style);

            _context = new bodb_luckyEntities();
            IsClosed = false;
            _winners = new Dictionary<int, string>(3);
            _nextPrize = false;

            var prize = (from i in _context.Prizes
                         join k in _context.PrizeSettings on i.PrizeType equals k.PrizeType
                         where i.PrizeType == (int)PrizeType
                         orderby i.PriceIndex ascending
                         select new { i.PrizeId, i.Image, i.Name, i.PriceIndex, i.PriceProgram, i.PrizeType, 
                             TypeName= k.Name }).Skip(PrizeIndex - 1).Take(1).First();

            _prizeId = prize.PrizeId;

            Text = prize.Name;
            
            pnlPrize.BackgroundImage = Utilities.BytesToImage(prize.Image);
            //BackgroundImage = Utilities.ScaleImage(prize.Image, this.Width, this.Height);
            labelNumber1.Text = prize.TypeName + " (" + prize.PriceProgram + ")";
            labelNumber2.Text = labelNumber1.Text;
            labelNumber2.Left = panel1.Width / 2 + labelNumber2.Width / 2;

            timer.Start();

            //get winner
            var prizedWinners = _context.Winners.Where(i => i.PrizeId == _prizeId).ToList();
            if (prizedWinners.Count <= 0) return;

            foreach (var winner in prizedWinners)
            {
                var staff = _context.Staffs.FirstOrDefault(i => i.StaffId == winner.StaffId);
                var dept = _context.Departments.First(i => i.DepartmentId == staff.DepartmentId);
                FillWinner(staff, dept);
                //record the winner list
                _winners.Add(winner.StaffId, staff.StaffCode);
            }
        }

        public void Barcode(string barcode)
        {
            if (_winners.Count > 2)
            {
                TalkBack("Full" + ", barcode: " + barcode);
                return;
            }
            if (_winners.Any(i => i.Value.StartsWith(barcode.Substring(0, 2))))
            {
                TalkBack("Wrong group" + ", barcode: " + barcode);
                return;
            }

            var staff = _context.Staffs.FirstOrDefault(i => i.StaffCode == barcode);
            if (staff == null) return;

            //duplicate check
            if (_winners.Any(i => i.Key == staff.StaffId))
            {
                TalkBack("Barcode duplicated" + ", barcode: " + barcode);
                return;
            }

            //cant't lucky twice times, except ad-hoc
            var won = (from i in _context.Winners
                       join j in _context.Prizes on i.PrizeId equals j.PrizeId
                       join k in _context.PrizeSettings on j.PrizeType equals k.PrizeType
                       where i.StaffId == staff.StaffId &&
                             k.PrizeType != 1 && k.XAdHoc == false
                       select i).Any();

            if (won)
            {
                //cheat? heh... write log
                Log.Fatal("Barcode duplicated, ID = " + barcode);
                TalkBack("Barcode duplicated" + ", barcode: " + barcode);
                return;
            }

            var dept = _context.Departments.First(i => i.DepartmentId == staff.DepartmentId);

            FillWinner(staff, dept);
            _winners.Add(staff.StaffId, staff.StaffCode);
            SaveToDatabase(staff.StaffId);
        }

        private void SaveToDatabase(int staffId)
        {
            var winner = new Winners
            {
                PrizeId = _prizeId,
                StaffId = staffId,
                CreatedDate = DateTime.Now
            };
            _context.Winners.AddObject(winner);

            if (_winners.Count > 2)
            {
                var setting = _context.PrizeSettings.FirstOrDefault(i => i.PrizeType == (int)PrizeType);
                setting.CurrentPrize = setting.CurrentPrize - 1;
                _nextPrize = true;
            }
            _context.SaveChanges();
        }

        private void FillWinner(Staffs staff, Departments dept)
        {
            var avatar = Utilities.ScaleImage(staff.Avatar, pictureBoxWinner1.Width, pictureBoxWinner1.Height);
            string fullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(staff.FullName.ToLower());
            var nick = string.IsNullOrWhiteSpace(staff.Nick) ? staff.FirstName.ToUpper() : staff.Nick.ToUpper();
            var grade = staff.StaffCode.Substring(0, 2);
            switch (grade)
            {
                case "10":
                    labelWinner1.Text = staff.StaffCode;
                    labelName1.Text = fullName;
                    labelNick1.Text = nick;
                    labelDept1.Text = dept.ShortName.ToUpper();
                    pictureBoxWinner1.Image = avatar;
                    pictureBox1.Visible = true;
                    break;
                case "11":
                    labelWinner2.Text = staff.StaffCode;
                    labelName2.Text = fullName;
                    labelNick2.Text = nick;
                    labelDept2.Text = dept.ShortName.ToUpper();
                    pictureBoxWinner2.Image = avatar;
                    pictureBox2.Visible = true;
                    break;
                case "12":
                    labelWinner3.Text = staff.StaffCode;
                    labelName3.Text = fullName;
                    labelNick3.Text = nick;
                    labelDept3.Text = dept.ShortName.ToUpper();
                    pictureBoxWinner3.Image = avatar;
                    pictureBox3.Visible = true;
                    break;

            }
        }
        private void RemoveWinner(int index)
        {
            var barcode = string.Empty;
            var fullName = string.Empty;
            var nick = string.Empty;

            switch (index)
            {
                case 1:
                    barcode = labelWinner1.Text;
                    fullName = labelName1.Text;
                    nick = labelNick1.Text;
                    pictureBox1.Visible = false;
                    break;
                case 2:
                    barcode = labelWinner2.Text;
                    fullName = labelName2.Text;
                    nick = labelNick2.Text;
                    pictureBox2.Visible = false;
                    break;
                case 3:
                    barcode = labelWinner3.Text;
                    fullName = labelName3.Text;
                    nick = labelNick3.Text;
                    pictureBox3.Visible = false;
                    break;
            }

            if (string.IsNullOrWhiteSpace(barcode)) return;

            var result = MessageBox.Show(
                "Are you sure you want to remove " + fullName + " (" + nick + ") from winner list?",
                "Year End Party 2012",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
                );
            if (result == DialogResult.No)
            {
                return;
            }

            var staffId = _context.Staffs.First(i => i.StaffCode == barcode).StaffId;

            //delete from database
            var staff = _context.Winners.First(i => i.StaffId == staffId);
            _context.Winners.DeleteObject(staff);

            _winners.Remove(staff.StaffId);

            if (_nextPrize)
            {
                var setting = _context.PrizeSettings.FirstOrDefault(i => i.PrizeType == (int)PrizeType);
                setting.CurrentPrize = setting.CurrentPrize + 1;
                _nextPrize = false;
            }
            _context.SaveChanges();

            switch (index)
            {
                case 1:
                    labelWinner1.Text = string.Empty;
                    pictureBoxWinner1.Image = null;
                    break;
                case 2:
                    labelWinner2.Text = string.Empty;
                    pictureBoxWinner2.Image = null;
                    break;
                case 3:
                    labelWinner3.Text = string.Empty;
                    pictureBoxWinner3.Image = null;
                    break;
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (labelNumber1.Left >= panel1.Width)
            {
                labelNumber1.Left = -labelNumber1.Width;
            }
            else
            {
                labelNumber1.Left += 1;
            }
            if (labelNumber2.Left >= panel1.Width)
            {
                labelNumber2.Left = 0 - labelNumber2.Width;
            }
            else
            {
                labelNumber2.Left += 1;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using log4net;
using YearEndParty.Core;
using System.Globalization;
using System.Configuration;
using System.Drawing;

namespace YearEndParty.Presentation
{
    public partial class SuperGrandWinnerDetails : Form
    {
        public delegate void TalkBackDelegate(string message);
        public TalkBackDelegate TalkBack;

        public bool IsClosed { get; set; }
        public int PrizeIndex { get; set; }
        public int Multiply { get; set; }
        private static readonly ILog Log = LogManager.GetLogger("Cheat");

        private Dictionary<int, string> _savedWinners = new Dictionary<int, string>(3);
        private IList<int> _prizedWinners = new List<int>(3);
        private IList<int> _prizeIds = new List<int>(3);

        private const int WidthImage = 1536;
        private const int HeightImage = 750;

        //width & height of screen
        private int WidthScreen = 0;
        private int HeightScreen = 0;

        private int _prizedIndex = 3;

        private const int PrizeType = (int)PrizeTypes.SuperGrand;
        private bodb_luckyEntities _context;

        private int _startPrize = 9;

        private bool isAllowDuplicateGroup = false;

        public SuperGrandWinnerDetails()
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
                case (char)4: //press ctrl+D to remove winners
                    RemoveWinners();
                    break;
                case (char)13:
                case (char)27:
                    IsClosed = true;
                    Close();
                    break;
            }
        }

        private void ResizeControl()
        {
            // number  pnlNumber2
            var heightNumber = 40 * HeightScreen / HeightImage;
            var widthNumber = 30 * WidthScreen / WidthImage;

            var topNumber1 = 115 * HeightScreen / HeightImage;
            var leftNumber1 = 285 * WidthScreen / WidthImage;

            var topNumber2 = 301 * HeightScreen / HeightImage;
            var leftNumber2 = 285 * WidthScreen / WidthImage;

            var topNumber3 = 487 * HeightScreen / HeightImage;
            var leftNumber3 = 285 * WidthScreen / WidthImage;

            // panel
            var heightPanel = 160 * HeightScreen / HeightImage;
            var widthPanel = 500 * WidthScreen / WidthImage;

            var topPanel1 = 140 * HeightScreen / HeightImage;
            var leftPanel1 = 310 * WidthScreen / WidthImage;

            var topPanel2 = 326 * HeightScreen / HeightImage;
            var leftPanel2 = 310 * WidthScreen / WidthImage;

            var topPanel3 = 512 * HeightScreen / HeightImage;
            var leftPanel3 = 310 * WidthScreen / WidthImage;

            var topNick = 4 * HeightScreen / HeightImage;
            var heightNick = 40 * HeightScreen / HeightImage;
            var topName = 45 * HeightScreen / HeightImage;
            var heightName = 64 * HeightScreen / HeightImage;
            var topDepartment = 110 * HeightScreen / HeightImage;
            var heightDepartment = 35 * HeightScreen / HeightImage;

            // image
            var topBoderImage = 1 * HeightScreen / HeightImage;
            var leftBoderImage = 1 * WidthScreen / WidthImage;
            var widthBoderImage = 108 * WidthScreen / WidthImage;
            var heightBoderImage = 148 * HeightScreen / HeightImage;

            // info
            var leftItem = 112 * WidthScreen / WidthImage;
            var widthItem = 355 * WidthScreen / WidthImage;
            var topImage = 5 * HeightScreen / HeightImage;
            var leftImage = 5 * WidthScreen / WidthImage;
            var heightImage = 140 * HeightScreen / HeightImage;
            var widthImage = 101 * WidthScreen / WidthImage;

            pictureBoxNumber1.Location = new Point(leftNumber1, topNumber1);
            pictureBoxNumber1.Height = heightNumber;
            pictureBoxNumber1.Width = widthNumber;

            pictureBoxNumber2.Location = new Point(leftNumber2, topNumber2);
            pictureBoxNumber2.Height = heightNumber;
            pictureBoxNumber2.Width = widthNumber;

            pictureBoxNumber3.Location = new Point(leftNumber3, topNumber3);
            pictureBoxNumber3.Height = heightNumber;
            pictureBoxNumber3.Width = widthNumber;

            pnlWinner1.Location = new Point(leftPanel1, topPanel1);
            pnlWinner1.Height = heightPanel;
            pnlWinner1.Width = widthPanel;

            pnlWinner2.Location = new Point(leftPanel2, topPanel2);
            pnlWinner2.Height = heightPanel;
            pnlWinner2.Width = widthPanel;

            pnlWinner3.Location = new Point(leftPanel3, topPanel3);
            pnlWinner3.Height = heightPanel;
            pnlWinner3.Width = widthPanel;

            bgImage1.Location = new Point(leftBoderImage, topBoderImage);
            bgImage1.Height = heightBoderImage;
            bgImage1.Width = widthBoderImage;

            bgImage2.Location = new Point(leftBoderImage, topBoderImage);
            bgImage2.Height = heightBoderImage;
            bgImage2.Width = widthBoderImage;

            bgImage3.Location = new Point(leftBoderImage, topBoderImage);
            bgImage3.Height = heightBoderImage;
            bgImage3.Width = widthBoderImage;

            imageLucky1.Location = new Point(leftImage, topImage);
            imageLucky1.Height = heightImage;
            imageLucky1.Width = widthImage;

            imageLucky2.Location = new Point(leftImage, topImage);
            imageLucky2.Height = heightImage;
            imageLucky2.Width = widthImage;

            imageLucky3.Location = new Point(leftImage, topImage);
            imageLucky3.Height = heightImage;
            imageLucky3.Width = widthImage;

            nickLucky1.Location = new Point(leftItem, topNick);
            nickLucky1.Height = heightNick;
            nickLucky1.Width = widthItem;

            nickLucky2.Location = new Point(leftItem, topNick);
            nickLucky2.Height = heightNick;
            nickLucky2.Width = widthItem;

            nickLucky3.Location = new Point(leftItem, topNick);
            nickLucky3.Height = heightNick;
            nickLucky3.Width = widthItem;

            nameLucky1.Location = new Point(leftItem, topName);
            nameLucky1.Height = heightName;
            nameLucky1.Width = widthItem;

            nameLucky2.Location = new Point(leftItem, topName);
            nameLucky2.Height = heightName;
            nameLucky2.Width = widthItem;

            nameLucky3.Location = new Point(leftItem, topName);
            nameLucky3.Height = heightName;
            nameLucky3.Width = widthItem;

            deptLucky1.Location = new Point(leftItem, topDepartment);
            deptLucky1.Height = heightDepartment;
            deptLucky1.Width = widthItem;

            deptLucky2.Location = new Point(leftItem, topDepartment);
            deptLucky2.Height = heightDepartment;
            deptLucky2.Width = widthItem;

            deptLucky3.Location = new Point(leftItem, topDepartment);
            deptLucky3.Height = heightDepartment;
            deptLucky3.Width = widthItem;

            var nickSize = int.Parse(ConfigurationManager.AppSettings["Super_NickSize"]);
            var nameSize = int.Parse(ConfigurationManager.AppSettings["Super_NameSize"]);
            var deptSize = int.Parse(ConfigurationManager.AppSettings["Super_DeptSize"]);
            isAllowDuplicateGroup = bool.Parse(ConfigurationManager.AppSettings["AllowDuplicateGroup"]);

            nickLucky1.Font = new Font(nickLucky1.Font.Name, nickSize, nickLucky1.Font.Style);
            nickLucky2.Font = new Font(nickLucky2.Font.Name, nickSize, nickLucky2.Font.Style);
            nickLucky3.Font = new Font(nickLucky3.Font.Name, nickSize, nickLucky3.Font.Style);

            nameLucky1.Font = new Font(nameLucky1.Font.Name, nameSize, nameLucky1.Font.Style);
            nameLucky2.Font = new Font(nameLucky2.Font.Name, nameSize, nameLucky2.Font.Style);
            nameLucky3.Font = new Font(nameLucky3.Font.Name, nameSize, nameLucky3.Font.Style);

            deptLucky1.Font = new Font(deptLucky1.Font.Name, deptSize, deptLucky1.Font.Style);
            deptLucky2.Font = new Font(deptLucky2.Font.Name, deptSize, deptLucky2.Font.Style);
            deptLucky3.Font = new Font(deptLucky3.Font.Name, deptSize, deptLucky3.Font.Style);
        }

        private void PrizeDetails_Load(object sender, EventArgs e)
        {
            WidthScreen = Screen.PrimaryScreen.Bounds.Width;
            HeightScreen = Screen.PrimaryScreen.Bounds.Height;
            
            ResizeControl();

            var beginPrize = (PrizeIndex - 1) / 3;
            IsClosed = false;
            _context = new bodb_luckyEntities();
            var prizes = (from i in _context.Prizes
                          where i.PrizeType == PrizeType
                          orderby i.PriceIndex ascending
                          select i).ToList().Skip((beginPrize) * 3).Take(3);

            var superGrandPrize = _context.PrizeSettings.Where(t => t.PrizeType == (int)PrizeType).FirstOrDefault();
            _startPrize = superGrandPrize.CurrentPrize;
            // render background
            RenderBackGround(PrizeIndex);
            for (var i = 0; i < prizes.Count(); i++)
            {
                _prizeIds.Add(prizes.ElementAt(i).PrizeId);
            }

            //get winner
            var loadWinners = (from i in _context.Winners
                               orderby i.CreatedDate descending
                               select i).ToList();

            //winners of three prizes
            loadWinners = loadWinners
                .Where(i =>
                       (prizes.Any(j => j.PrizeId == i.PrizeId)) ||
                       i.PrizeId == -1
                ).ToList();


            for (var i = 0; i < _prizeIds.Count; i++)
            {
                var winner = loadWinners.Where(t => t.PrizeId == _prizeIds[i]).FirstOrDefault();
                
                if (winner != null)
                {
                    var staff = _context.Staffs.FirstOrDefault(t => t.StaffId == winner.StaffId);
                    var dept = _context.Departments.First(t => t.DepartmentId == staff.DepartmentId);

                    var prize = _context.Prizes.FirstOrDefault(t => t.PrizeId == winner.PrizeId);
                    var prizeIndex = prize == null ? -1 : prize.PriceIndex;

                    FillWinner(staff, dept, i, prizeIndex);
                    _savedWinners.Add(winner.StaffId, staff.StaffCode);

                    _prizedIndex--;
                    _prizedWinners.Add(staff.StaffId);
                }
            }
        }

        public void Barcode(string barcode)
        {
            var staff = _context.Staffs.FirstOrDefault(i => i.StaffCode == barcode);
            if (staff == null) return;

            if (_savedWinners.Count > 2)
            {
                TalkBack("Full" + ", barcode: " + barcode);
                return;
            }

            //AllowDuplicateGroup
            if (!isAllowDuplicateGroup)
            {
                if (_savedWinners.Any(i => i.Value.StartsWith(barcode.Substring(0, 2))))
                {
                    TalkBack("Wrong group" + ", barcode: " + barcode);
                    return;
                }
            }

            //cant't lucky two times, except ad-hoc
            var won = (from i in _context.Winners
                       //join j in _context.Prizes on i.PrizeId equals j.PrizeId
                       //join k in _context.PrizeSettings on j.PrizeType equals k.PrizeType
                       where i.StaffId == staff.StaffId
                       select i).Any();
            if (won)
            {
                //cheat? heh...write log
                Log.Fatal("Barcode duplicated, ID = " + barcode);
                TalkBack("Barcode duplicated" + ", barcode: " + barcode);
                return;
            }

            var dept = _context.Departments.First(i => i.DepartmentId == staff.DepartmentId);

            var superGrandPrize = _context.PrizeSettings.Where(t => t.PrizeType == (int)PrizeTypes.SuperGrand).FirstOrDefault();
            var prizeCurrent = (superGrandPrize.CurrentPrize + 2) % 3;

            FillWinner(staff, dept, prizeCurrent, superGrandPrize.CurrentPrize);
            if (superGrandPrize.CurrentPrize > 3)
            {
                SaveToDatabase(staff.StaffId, prizeCurrent);
            }
            else
            {
                SaveToDatabase(staff.StaffId, -1);
            }

            _savedWinners.Add(staff.StaffId, staff.StaffCode);
        }

        private void RenderBackGround(int index)
        {
            switch (index)
            {
                case 9:
                case 8:
                case 7:
                    this.BackgroundImage = Properties.Resources.supergrandprize789;
                    break;
                case 6:
                case 5:
                case 4:
                    this.BackgroundImage = Properties.Resources.supergrandprize456;
                    break;
                case 3:
                case 2:
                case 1:
                    this.BackgroundImage = Properties.Resources.supergrandprize123;
                    break;
                default:
                    this.BackgroundImage = Properties.Resources.supergrandprize123;
                    break;
            }
        }

        private void FillWinner(Staffs staff, Departments dept, int prizeCurrent, int prizeIndex)
        {
            var avatar = Utilities.ScaleImage(staff.Avatar, imageLucky1.Width, imageLucky1.Height);
            var nick = string.IsNullOrWhiteSpace(staff.Nick) ? staff.FirstName.ToUpper() : staff.Nick.ToUpper();
            string fullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(staff.FullName.ToLower());

            var grade = staff.StaffCode.Substring(0, 2);
            var color = GetColorByGroup(grade);

            var prizeNumber = Properties.Resources._4;

            switch (prizeIndex)
            {
                case 1:
                    prizeNumber = Properties.Resources._1;
                    break;
                case 2:
                    prizeNumber = Properties.Resources._2;
                    break;
                case 3:
                    prizeNumber = Properties.Resources._3;
                    break;
                case 4:
                    prizeNumber = Properties.Resources._4;
                    break;
                case 5:
                    prizeNumber = Properties.Resources._5;
                    break;
                case 6:
                    prizeNumber = Properties.Resources._6;
                    break;
                case 7:
                    prizeNumber = Properties.Resources._7;
                    break;
                case 8:
                    prizeNumber = Properties.Resources._8;
                    break;
                case 9:
                    prizeNumber = Properties.Resources._9;
                    break;
                default:
                    prizeNumber = null;
                    break;
            }

            switch (prizeCurrent)
            {
                case 0:
                    nickLucky1.Text = nick;
                    nameLucky1.Text = fullName;
                    deptLucky1.Text = dept.ShortName.ToUpper();
                    imageLucky1.Image = avatar;
                    imageLucky1.Visible = true;

                    nickLucky1.ForeColor = color;
                    nameLucky1.ForeColor = color;
                    deptLucky1.ForeColor = color;

                    pictureBoxNumber1.BackgroundImage = prizeNumber;
                    pictureBoxNumber1.Visible = true;
                    break;
                case 1:
                    nickLucky2.Text = nick;
                    nameLucky2.Text = fullName;
                    deptLucky2.Text = dept.ShortName.ToUpper();
                    imageLucky2.Image = avatar;
                    imageLucky2.Visible = true;

                    nickLucky2.ForeColor = color;
                    nameLucky2.ForeColor = color;
                    deptLucky2.ForeColor = color;
                    pictureBoxNumber2.BackgroundImage = prizeNumber;
                    pictureBoxNumber2.Visible = true;
                    break;
                case 2:
                    nickLucky3.Text = nick;
                    nameLucky3.Text = fullName;
                    deptLucky3.Text = dept.ShortName.ToUpper();
                    imageLucky3.Image = avatar;
                    imageLucky3.Visible = true;

                    nickLucky3.ForeColor = color;
                    nameLucky3.ForeColor = color;
                    deptLucky3.ForeColor = color;
                    pictureBoxNumber3.BackgroundImage = prizeNumber;
                    pictureBoxNumber3.Visible = true;
                    break;

            }
        }

        private Color GetColorByGroup(string grade)
        {
            var color = Color.FromArgb(242, 109, 124);
            switch (grade)
            {
                case "10":
                    color = Color.FromArgb(242, 109, 124);
                    break;
                case "11":
                    color = Color.FromArgb(167, 239, 54);
                    break;
                case "12":
                    color = Color.FromArgb(136, 188, 225);
                    break;
                default:
                    color = Color.White;
                    break;
            }

            return color;
        }

        private void SaveToDatabase(int staffId, int prizeCurrent)
        {
            if (prizeCurrent != -1)
            {
                var prizeId = _prizeIds[prizeCurrent];
                var winner = new Winners
                {
                    PrizeId = prizeId,
                    StaffId = staffId,
                    CreatedDate = DateTime.Now
                };

                _context.Winners.AddObject(winner);
                var setting = _context.PrizeSettings.FirstOrDefault(i => i.PrizeType == (int)PrizeTypes.SuperGrand);
                if (setting.CurrentPrize > 0)
                {
                    setting.CurrentPrize = setting.CurrentPrize - 1;
                }
            }

            try
            {
                _context.SaveChanges();
            }
            catch
            {
                return;
            }
        }

        private void Remove(int index)
        {
            switch (index)
            {
                case 1:
                    pictureBoxNumber1.Image = null;
                    imageLucky1.Image = null;
                    imageLucky1.Visible = false;
                    break;
                case 2:
                    pictureBoxNumber2.Image = null;
                    imageLucky2.Image = null;
                    imageLucky2.Visible = false;
                    break;
                case 3:
                    pictureBoxNumber3.Image = null;
                    imageLucky3.Image = null;
                    imageLucky3.Visible = false;
                    break;
            }
        }

        private void RemoveWinners()
        {
            if (_savedWinners.Count == 0) return;

            var result = MessageBox.Show(
                "Are you sure you want to remove all winners?",
                "Staff Annual Party",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
                );
            if (result == DialogResult.No || _savedWinners.Count == 0)
            {
                return;
            }

            var winners = _context.Winners.Where(i => _savedWinners.Keys.Contains(i.StaffId) && (i.PrizeId == -1 || _prizeIds.Contains(i.PrizeId)));
            foreach (var item in winners)
            {
                _context.Winners.DeleteObject(item);
            }

            var setting = _context.PrizeSettings.FirstOrDefault(i => i.PrizeType == (int)PrizeTypes.SuperGrand);
            setting.CurrentPrize = ((_startPrize-1)/3 + 1) * 3;

            _context.SaveChanges();

            Remove(1); Remove(2); Remove(3);

            _savedWinners = new Dictionary<int, string>(3);
            _prizedWinners = new List<int>(3);
            _prizedIndex = 3;
        }
    }
}

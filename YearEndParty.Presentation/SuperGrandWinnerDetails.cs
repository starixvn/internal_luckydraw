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

        private const int WidthImage = 1900;
        private const int HeightImage = 1140;

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
                //case (char)5: //press ctrl+E to close
                //    ResetPrize();
                //    break;
                case (char)13:
                case (char)27:
                    IsClosed = true;
                    Close();
                    break;
            }
        }

        private void ResizeControl()
        {
            // number
            var heightNumber = 70 * HeightScreen / HeightImage;
            var widthNumber = 50 * WidthScreen / WidthImage;

            var topNumber3 = 250 * HeightScreen / HeightImage;
            var leftNumber3 = 415 * WidthScreen / WidthImage;

            var topNumber2 = 250 * HeightScreen / HeightImage;
            var leftNumber2 = 660 * WidthScreen / WidthImage;

            var topNumber1 = 250 * HeightScreen / HeightImage;
            var leftNumber1 = 905 * WidthScreen / WidthImage;

            // panel
            var heightPanel = 470 * HeightScreen / HeightImage;
            var widthPanel = 240 * WidthScreen / WidthImage;

            var topPanel3 = 344 * HeightScreen / HeightImage;
            var leftPanel3 = 330 * WidthScreen / WidthImage;

            var topPanel2 = 344 * HeightScreen / HeightImage;
            var leftPanel2 = 575 * WidthScreen / WidthImage;

            var topPanel1 = 344 * HeightScreen / HeightImage;
            var leftPanel1 = 820 * WidthScreen / WidthImage;

            // info
            var leftItem = 3 * WidthScreen / WidthImage;
            var widthItem = 235 * WidthScreen / WidthImage;

            var widthImage = 200 * WidthScreen / WidthImage;
            var leftImage = 12 * WidthScreen / WidthImage;
            // image
            var topImage = 3 * HeightScreen / HeightImage;
            var heightImage = 220 * HeightScreen / HeightImage;
            
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

            pictureBoxWinner1.Location = new Point(leftImage, topImage);
            pictureBoxWinner1.Height = heightImage;
            pictureBoxWinner1.Width = widthImage;

            pictureBoxWinner2.Location = new Point(leftImage, topImage);
            pictureBoxWinner2.Height = heightImage;
            pictureBoxWinner2.Width = widthImage;

            pictureBoxWinner3.Location = new Point(leftImage, topImage);
            pictureBoxWinner3.Height = heightImage;
            pictureBoxWinner3.Width = widthImage;

            // Nick
            var topNick = pictureBoxWinner1.Top + pictureBoxWinner1.Height + 11 * HeightScreen / HeightImage;
            var heightNick = 40 * HeightScreen / HeightImage;

            labelNick1.Location = new Point(leftItem, topNick);
            labelNick1.Height = heightNick;
            labelNick1.Width = widthItem;

            labelNick2.Location = new Point(leftItem, topNick);
            labelNick2.Height = heightNick;
            labelNick2.Width = widthItem;

            labelNick3.Location = new Point(leftItem, topNick);
            labelNick3.Height = heightNick;
            labelNick3.Width = widthItem;

            // Name
            var topName = labelNick1.Top + labelNick1.Height + 11 * HeightScreen / HeightImage;
            var heightName = 120 * HeightScreen / HeightImage;

            labelName1.Location = new Point(leftItem, topName);
            labelName1.Height = heightName;
            labelName1.Width = widthItem;

            labelName2.Location = new Point(leftItem, topName);
            labelName2.Height = heightName;
            labelName2.Width = widthItem;

            labelName3.Location = new Point(leftItem, topName);
            labelName3.Height = heightName;
            labelName3.Width = widthItem;

            // Department
            var topDepartment = labelName1.Top + labelName1.Height + 11 * HeightScreen / HeightImage;
            var heightDepartment = 50 * HeightScreen / HeightImage;

            labelDept1.Location = new Point(leftItem, topDepartment);
            labelDept1.Height = heightDepartment;
            labelDept1.Width = widthItem;

            labelDept2.Location = new Point(leftItem, topDepartment);
            labelDept2.Height = heightDepartment;
            labelDept2.Width = widthItem;

            labelDept3.Location = new Point(leftItem, topDepartment);
            labelDept3.Height = heightDepartment;
            labelDept3.Width = widthItem;

            var nickSize = int.Parse(ConfigurationManager.AppSettings["Super_NickSize"]);
            var nameSize = int.Parse(ConfigurationManager.AppSettings["Super_NameSize"]);
            var deptSize = int.Parse(ConfigurationManager.AppSettings["Super_DeptSize"]);
            isAllowDuplicateGroup = bool.Parse(ConfigurationManager.AppSettings["AllowDuplicateGroup"]);
            
            labelNick1.Font = new Font(labelNick1.Font.Name, nickSize, labelNick1.Font.Style);
            labelNick2.Font = new Font(labelNick2.Font.Name, nickSize, labelNick2.Font.Style);
            labelNick3.Font = new Font(labelNick3.Font.Name, nickSize, labelNick3.Font.Style);

            labelName1.Font = new Font(labelName1.Font.Name, nameSize, labelName1.Font.Style);
            labelName2.Font = new Font(labelName2.Font.Name, nameSize, labelName2.Font.Style);
            labelName3.Font = new Font(labelName3.Font.Name, nameSize, labelName3.Font.Style);

            labelDept1.Font = new Font(labelDept1.Font.Name, deptSize, labelDept1.Font.Style);
            labelDept2.Font = new Font(labelDept2.Font.Name, deptSize, labelDept2.Font.Style);
            labelDept3.Font = new Font(labelDept3.Font.Name, deptSize, labelDept3.Font.Style);
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

                    FillWinner(staff, dept, i);

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

            FillWinner(staff, dept, prizeCurrent);
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
                    this.BackgroundImage = Properties.Resources.Winner_supergrand_1900x1140_7_8_9;
                    this.pictureBoxNumber3.BackgroundImage = Properties.Resources._9;
                    this.pictureBoxNumber2.BackgroundImage = Properties.Resources._8;
                    this.pictureBoxNumber1.BackgroundImage = Properties.Resources._7;
                    break;
                case 6:
                case 5:
                case 4:
                    this.BackgroundImage = Properties.Resources.Winner_supergrand_1900x1140_4_5_6;
                    this.pictureBoxNumber3.BackgroundImage = Properties.Resources._6;
                    this.pictureBoxNumber2.BackgroundImage = Properties.Resources._5;
                    this.pictureBoxNumber1.BackgroundImage = Properties.Resources._4;
                    break;
                case 3:
                case 2:
                case 1:
                    this.BackgroundImage = Properties.Resources.Winner_supergrand_1900x1140_1_2_3;
                    break;
                default:
                    this.BackgroundImage = Properties.Resources.Winner_supergrand_1900x1140_7_8_9;
                    break;
            }
        }

        private void FillWinner(Staffs staff, Departments dept, int prizeCurrent)
        {
            var avatar = Utilities.ScaleImage(staff.Avatar, pictureBoxWinner1.Width, pictureBoxWinner1.Height);
            var nick = string.IsNullOrWhiteSpace(staff.Nick) ? staff.FirstName.ToUpper() : staff.Nick.ToUpper();
            string fullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(staff.FullName.ToLower());

            var grade = staff.StaffCode.Substring(0, 2);
            var color = GetColorByGroup(grade);

            switch (prizeCurrent)
            {
                case 0:
                    labelWinner1.Text = staff.StaffCode;
                    labelName1.Text = fullName;
                    labelNick1.Text = nick;
                    labelDept1.Text = dept.ShortName.ToUpper();
                    pictureBoxWinner1.Image = avatar;

                    labelNick1.ForeColor = color;
                    labelName1.ForeColor = color;
                    labelDept1.ForeColor = color;
                    pictureBoxNumber1.Visible = true;
                    break;
                case 1:
                    labelWinner2.Text = staff.StaffCode;
                    labelName2.Text = fullName;
                    labelNick2.Text = nick;
                    labelDept2.Text = dept.ShortName.ToUpper();
                    pictureBoxWinner2.Image = avatar;

                    labelNick2.ForeColor = color;
                    labelName2.ForeColor = color;
                    labelDept2.ForeColor = color;
                    pictureBoxNumber2.Visible = true;
                    break;
                case 2:
                    labelWinner3.Text = staff.StaffCode;
                    labelName3.Text = fullName;
                    labelNick3.Text = nick;
                    labelDept3.Text = dept.ShortName.ToUpper();
                    pictureBoxWinner3.Image = avatar;

                    labelNick3.ForeColor = color;
                    labelName3.ForeColor = color;
                    labelDept3.ForeColor = color;
                    pictureBoxNumber3.Visible = true;
                    break;

            }
        }

        private Color GetColorByGroup(string grade)
        {
            var color = Color.FromArgb(254, 126, 209);
            switch (grade)
            {
                case "10":
                    color = Color.FromArgb(254, 126, 209);
                    break;
                case "11":
                    color = Color.FromArgb(179, 255, 176);
                    break;
                case "12":
                    color = Color.FromArgb(155, 156, 255);
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
                    labelWinner1.Text = string.Empty;
                    pictureBoxNumber1.Image = null;
                    pictureBoxWinner1.Image = null;
                    break;
                case 2:
                    labelWinner2.Text = string.Empty;
                    pictureBoxNumber2.Image = null;
                    pictureBoxWinner2.Image = null;
                    break;
                case 3:
                    labelWinner3.Text = string.Empty;
                    pictureBoxNumber3.Image = null;
                    pictureBoxWinner3.Image = null;
                    break;
            }
        }

        private void RemoveWinners()
        {
            if (_savedWinners.Count == 0) return;

            var result = MessageBox.Show(
                "Are you sure you want to remove all winners?",
                "Annual Party 2014",
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

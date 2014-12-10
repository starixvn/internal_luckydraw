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
    public partial class SuperGrandWinnerSpecialDetails : Form
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

        private int _focus = 4;
        private bool isDeleted = false;
        private bool isFinished = false;

        private bool isAllowDuplicateGroup = false;

        public SuperGrandWinnerSpecialDetails()
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
                case (char)5: //press ctrl+E to close
                    ResetPrizeWinners();
                    break;
                case (char)13:
                case (char)27:
                    IsClosed = true;
                    Close();
                    break;
                case (char)49: //1
                    ChoosePrize(1);
                    break;
                case (char)50: //2
                    ChoosePrize(2);
                    break;
                case (char)51: //3
                    ChoosePrize(3);
                    break;
                case (char)52: //4
                    ChoosePrize(4);
                    break;
                case (char)53: //5
                    ChoosePrize(5);
                    break;
                case (char)54: //6
                    ChoosePrize(6);
                    break;
                case (char)55: //7
                    ChoosePrize(7);
                    break;
                case (char)56: //8
                    ChoosePrize(8);
                    break;
                case (char)57: //9
                    ChoosePrize(9);
                    break;
            }
        }

        private void ResizeControl()
        {
            // number  pnlNumber2
            var heightNumber = 40 * HeightScreen / HeightImage;
            var widthNumber = 30 * WidthScreen / WidthImage;

            var topNumber3 = 115 * HeightScreen / HeightImage;
            var leftNumber3 = 285 * WidthScreen / WidthImage;

            var topNumber2 = 301 * HeightScreen / HeightImage;
            var leftNumber2 = 285 * WidthScreen / WidthImage;

            var topNumber1 = 487 * HeightScreen / HeightImage;
            var leftNumber1 = 285 * WidthScreen / WidthImage;

            // panel
            var heightPanel = 160 * HeightScreen / HeightImage;
            var widthPanel = 500 * WidthScreen / WidthImage;

            var topPanel3 = 140 * HeightScreen / HeightImage;
            var leftPanel3 = 310 * WidthScreen / WidthImage;

            var topPanel2 = 326 * HeightScreen / HeightImage;
            var leftPanel2 = 310 * WidthScreen / WidthImage;

            var topPanel1 = 512 * HeightScreen / HeightImage;
            var leftPanel1 = 310 * WidthScreen / WidthImage;

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

            // render background
            RenderBackGround(PrizeIndex);
            for (var i = 0; i < prizes.Count(); i++)
            {
                //FillImage(prizes.ElementAt(i), i + 1);
                _prizeIds.Add(prizes.ElementAt(i).PrizeId);
            }

            //get winner
            var loadAllWinners = (from i in _context.Winners
                               orderby i.CreatedDate descending
                               select i).ToList();

            //winners of three prizes
            var winners = loadAllWinners
                .Where(i =>
                       (prizes.Any(j => (j.PrizeId == i.PrizeId || i.PrizeId == -1))) 
                ).ToList();

            var index = 1;
            foreach (var winner in winners)
            {
                var staff = _context.Staffs.FirstOrDefault(i => i.StaffId == winner.StaffId);
                var dept = _context.Departments.First(i => i.DepartmentId == staff.DepartmentId);

                var prize = _context.Prizes.FirstOrDefault(t => t.PrizeId == winner.PrizeId);
                var prizeIndex = prize == null ? -1 : prize.PriceIndex;

                FillWinner(staff, dept, index, prizeIndex);
                //record the winner list
                
                _savedWinners.Add(winner.StaffId, staff.StaffCode);

                //fill prize seal
                var prizeId = loadAllWinners.First(i => i.StaffId == staff.StaffId).PrizeId;
                if (prizeId == -1)
                {
                    index++;
                    continue;
                }

                _prizedIndex--;
                _prizedWinners.Add(staff.StaffId);
                index++;
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

            // AllowDuplicateGroup
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
                       where i.StaffId == staff.StaffId select i).Any();
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

            var winners = _context.Winners.Where(t=>t.PrizeId == -1);

            FillWinner(staff, dept, (winners.Count() % 3 + 1), -1);
            
            SaveToDatabase(staff.StaffId, -1);

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

        private void FillWinner(Staffs staff, Departments dept, int position, int prizeIndex)
        {
            var avatar = Utilities.ScaleImage(staff.Avatar, imageLucky1.Width, imageLucky1.Height);
            var nick = string.IsNullOrWhiteSpace(staff.Nick) ? staff.FirstName.ToUpper() : staff.Nick.ToUpper();
            string fullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(staff.FullName.ToLower());
            var prizeNumber = Properties.Resources._1;

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
                default:
                    prizeNumber = null;
                    break;
            }

            var grade = staff.StaffCode.Substring(0, 2);
            var color = GetColorByGroup(grade);
            switch (position)
            {
                case 1:
                    codeLucky1.Text = staff.StaffCode;
                    nameLucky1.Text = fullName;
                    nickLucky1.Text = nick;
                    deptLucky1.Text = dept.ShortName.ToUpper();
                    imageLucky1.Image = avatar;
                    pictureBoxNumber1.BackgroundImage = prizeNumber;

                    nickLucky1.ForeColor = color;
                    nameLucky1.ForeColor = color;
                    deptLucky1.ForeColor = color;
                    imageLucky1.Visible = true;
                    break;
                case 2:
                    codeLucky2.Text = staff.StaffCode;
                    nameLucky2.Text = fullName;
                    nickLucky2.Text = nick;
                    deptLucky2.Text = dept.ShortName.ToUpper();
                    imageLucky2.Image = avatar;
                    pictureBoxNumber2.BackgroundImage = prizeNumber;

                    nickLucky2.ForeColor = color;
                    nameLucky2.ForeColor = color;
                    deptLucky2.ForeColor = color;
                    imageLucky2.Visible = true;
                    break;
                case 3:
                    codeLucky3.Text = staff.StaffCode;
                    nameLucky3.Text = fullName;
                    nickLucky3.Text = nick;
                    deptLucky3.Text = dept.ShortName.ToUpper();
                    imageLucky3.Image = avatar;
                    pictureBoxNumber3.BackgroundImage = prizeNumber;

                    nickLucky3.ForeColor = color;
                    nameLucky3.ForeColor = color;
                    deptLucky3.ForeColor = color;
                    imageLucky3.Visible = true;
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
                if (setting.CurrentPrize > 0 && !isDeleted)
                {
                    setting.CurrentPrize = setting.CurrentPrize - 1;
                }
            }
            else
            {
                var winnerOutStanding = new Winners
                {
                    PrizeId = -1,
                    StaffId = staffId,
                    CreatedDate = DateTime.Now
                };

                _context.Winners.AddObject(winnerOutStanding);
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

        private void UpdateWinnerPrize(int staffId, int prizeId)
        {
            var winner = _context.Winners.First(i => i.StaffId == staffId);
            _context.Winners.DeleteObject(winner);
            _context.SaveChanges();

            var newWinner = new Winners
            {
                PrizeId = prizeId,
                StaffId = staffId,
                CreatedDate = DateTime.Now
            };
            _context.Winners.AddObject(newWinner);

            
            var setting = _context.PrizeSettings.FirstOrDefault(i => i.PrizeType == (int)PrizeTypes.SuperGrand);
            if (setting.CurrentPrize > 0 && !isDeleted)
            {
                setting.CurrentPrize = setting.CurrentPrize - 1;
                isFinished = true;
            }
            

            _context.SaveChanges();
            _prizedWinners.Add(staffId);
        }

        private void Remove(int index)
        {
            switch (index)
            {
                case 1:
                    codeLucky1.Text = string.Empty;
                    pictureBoxNumber1.BackgroundImage = null;
                    imageLucky1.Image = null;
                    imageLucky1.Visible = false;
                    break;
                case 2:
                    codeLucky2.Text = string.Empty;
                    pictureBoxNumber2.BackgroundImage = null;
                    imageLucky2.Image = null;
                    imageLucky2.Visible = false;
                    break;
                case 3:
                    codeLucky3.Text = string.Empty;
                    pictureBoxNumber3.BackgroundImage = null;
                    imageLucky3.Image = null;
                    imageLucky3.Visible = false;
                    break;
            }
        }

        private void ResetPrizeWinners()
        {
            if (_savedWinners.Count == 0) return;

            var result = MessageBox.Show(
                "Are you sure you want to reset all prizes?",
                "Staff Annual Party",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
                );
            if (result == DialogResult.No || _savedWinners.Count == 0)
            {
                return;
            }

            pictureBoxNumber1.BackgroundImage = null;
            pictureBoxNumber2.BackgroundImage = null;
            pictureBoxNumber3.BackgroundImage = null;

            var winners = _context.Winners.Where(i => _savedWinners.Keys.Contains(i.StaffId) && (i.PrizeId == -1 || _prizeIds.Contains(i.PrizeId)));

            //remove prizeId = -1
            foreach (var item in winners)
            {
                _context.Winners.DeleteObject(item);
            }
            _context.SaveChanges();

            //re-insert
            foreach (var item in _savedWinners)
            {
                var entity = new Winners
                {
                    StaffId = item.Key,
                    PrizeId = -1,
                    CreatedDate = DateTime.Now
                };
                _context.Winners.AddObject(entity);
            }

            _context.SaveChanges();
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
            setting.CurrentPrize = ((setting.CurrentPrize - 1) / 3 + 1) * 3;

            _context.SaveChanges();

            Remove(1); Remove(2); Remove(3);
            pictureBoxNumber1.BackgroundImage = null;
            pictureBoxNumber2.BackgroundImage = null;
            pictureBoxNumber3.BackgroundImage = null;

            _savedWinners = new Dictionary<int, string>(3);
            _prizedWinners = new List<int>(3);
            _prizedIndex = 3;

        }

        private void RemoveWinner(int index)
        {
            var barcode = string.Empty;
            var name = string.Empty;
            var nick = string.Empty;

            switch (index)
            {
                case 1:
                    barcode = codeLucky1.Text;
                    name = nameLucky1.Text;
                    nick = nickLucky1.Text;
                    break;
                case 2:
                    barcode = codeLucky2.Text;
                    name = nameLucky2.Text;
                    nick = nickLucky2.Text;
                    break;
                case 3:
                    barcode = codeLucky3.Text;
                    name = nameLucky3.Text;
                    nick = nickLucky3.Text;
                    break;
            }

            if (string.IsNullOrWhiteSpace(barcode))
            {
                return;
            }

            var result = MessageBox.Show(
                "Are you sure you want to remove " + name + " (" + nick + ") from lucky list?",
                "Staff Annual party",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
                );
            if (result == DialogResult.No || _savedWinners.Count == 0)
            {
                return;
            }

            Remove(index);
            var staffId = _savedWinners.First(i => i.Value == barcode).Key;

            if (_prizedWinners.Count == 3 && _prizedIndex == 0)
            {
                var setting = _context.PrizeSettings.FirstOrDefault(i => i.PrizeType == (int)PrizeTypes.SuperGrand);
                if (setting.CurrentPrize * 3 < setting.Quantity)
                {
                    if (isFinished)
                    {
                        setting.CurrentPrize = setting.CurrentPrize + 1;
                        isFinished = false;
                    }
                    isDeleted = true;
                }

            }

            _savedWinners.Remove(staffId);

            //delete from database
            var entity = _context.Winners.First(i => i.StaffId == staffId &&
                                                    (i.PrizeId == -1 || _prizeIds.Contains(i.PrizeId)));
            _context.Winners.DeleteObject(entity);
            _context.SaveChanges();
        }

        private void SpecialDetails_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case 38:
                    if (_focus < 3)
                    {
                        _focus++;
                        SetFocus();
                    }
                    break;
                case 40:
                    if (_focus > 1)
                    {
                        _focus--;
                        SetFocus();
                    }
                    break;
                case 46:
                    DeleteSpecial();
                    break;
                default:
                    break;
            }
        }

        private void DeleteSpecial()
        {
            if (_focus > 0 && _focus < 4)
            {
                RemoveWinner(_focus);
            }
        }

        private void SetFocus()
        {
            switch (_focus)
            {
                case 1:
                    bgImage1.BackgroundImage = null;
                    bgImage2.BackgroundImage = YearEndParty.Presentation.Properties.Resources.winner_pic;
                    bgImage3.BackgroundImage = YearEndParty.Presentation.Properties.Resources.winner_pic;
                    bgImage1.BackColor = Color.LightSteelBlue;
                    bgImage2.BackColor = Color.Transparent;
                    bgImage3.BackColor = Color.Transparent;
                    break;
                case 2:
                    bgImage1.BackgroundImage = YearEndParty.Presentation.Properties.Resources.winner_pic;
                    bgImage2.BackgroundImage = null;
                    bgImage3.BackgroundImage = YearEndParty.Presentation.Properties.Resources.winner_pic;
                    bgImage1.BackColor = Color.Transparent;
                    bgImage2.BackColor = Color.LightSteelBlue;
                    bgImage3.BackColor = Color.Transparent;
                    break;
                case 3:
                    bgImage1.BackgroundImage = YearEndParty.Presentation.Properties.Resources.winner_pic;
                    bgImage2.BackgroundImage = YearEndParty.Presentation.Properties.Resources.winner_pic;
                    bgImage3.BackgroundImage = null;
                    bgImage1.BackColor = Color.Transparent;
                    bgImage2.BackColor = Color.Transparent;
                    bgImage3.BackColor = Color.LightSteelBlue;
                    break;
                default:
                    bgImage1.BackColor = Color.Transparent;
                    bgImage2.BackColor = Color.Transparent;
                    bgImage2.BackColor = Color.White;
                    break;
            }
        }

        private void ChoosePrize(int prize)
        {
            if (_savedWinners.Count >= 3)
            {
                if (prize > 0 && prize < 4)
                {
                    int prizeCurrent = _prizeIds[prize-1];
                    //chon giai da dc chon roi
                    var won = (from i in _context.Winners
                               where i.PrizeId == prizeCurrent
                               select i).Any();
                    if (won)
                    {
                        return;
                    }

                    var staffID = string.Empty;;
                    var nick = string.Empty;
                    var name = string.Empty;
                    var department = string.Empty;
                    var numberImage = Properties.Resources._1;

                    switch (prize)
                    {
                        case 1:
                            numberImage = Properties.Resources._1;
                            break;
                        case 2:
                            numberImage = Properties.Resources._2;
                            break;
                        case 3:
                            numberImage = Properties.Resources._3;
                            break;
                    }

                    switch (_focus)
                    {
                        case 1:
                            staffID = codeLucky1.Text;
                            nick = nickLucky1.Text;
                            name = nameLucky1.Text;
                            department = deptLucky1.Text;

                            pictureBoxNumber1.BackgroundImage = numberImage;

                            break;

                        case 2:
                            staffID = codeLucky2.Text;
                            nick = nickLucky2.Text;
                            name = nameLucky2.Text;
                            department = deptLucky2.Text;
                            pictureBoxNumber2.BackgroundImage = numberImage;
                            break;
                        case 3:
                            staffID = codeLucky3.Text;
                            nick = nickLucky3.Text;
                            name = nameLucky3.Text;
                            department = deptLucky3.Text;
                            pictureBoxNumber3.BackgroundImage = numberImage;
                            break;
                    }

                    if (!string.IsNullOrEmpty(staffID))
                    {
                        var staff = _context.Staffs.FirstOrDefault(i => i.StaffCode == staffID);
                        if (staff == null) return;

                        _prizedIndex--;
                        UpdateWinnerPrize(staff.StaffId, _prizeIds[prize - 1]);
                    }
                }
            }
        }
    }
}

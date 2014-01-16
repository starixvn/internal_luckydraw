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
                //case (char)45:
                //    RemoveWinners();
                //    break;
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
                //case (char)47:
                //    ResetPrize();
                //    break;
            }
        }

        private void PrizeDetails_Load(object sender, EventArgs e)
        {
            var nickSize = int.Parse(ConfigurationManager.AppSettings["Detail_NickSize"]);
            var nameSize = int.Parse(ConfigurationManager.AppSettings["Detail_NameSize"]);
            var deptSize = int.Parse(ConfigurationManager.AppSettings["Detail_DeptSize"]);
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

            var beginPrize = (PrizeIndex - 1) / 3;


            IsClosed = false;
            _context = new bodb_luckyEntities();
            var prizes = (from i in _context.Prizes
                          where i.PrizeType == PrizeType
                          orderby i.PriceIndex ascending
                          select i).ToList().Skip((beginPrize) * 3).Take(3);

            //var prize = (from i in _context.Prizes
            //             where i.PrizeType == (int)PrizeType
            //             orderby i.PriceIndex ascending
            //             select i).Skip((PrizeIndex - 1) * 3).Take(1).First();

            var superGrandPrize = _context.PrizeSettings.Where(t => t.PrizeType == (int)PrizeType).FirstOrDefault();

            //var prize = (from i in _context.Prizes
            //             where i.PrizeType == (int)PrizeType
            //             orderby i.PriceIndex ascending
            //             select i).First();

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
                       (prizes.Any(j => j.PrizeId == i.PrizeId)) 
                ).ToList();


            var loadWinners = loadAllWinners
                .Where(i =>i.PrizeId == -1).ToList();

            for (var i = 0; i < _prizeIds.Count; i++)
            {
                var winner = winners.Where(t => t.PrizeId == _prizeIds[i]).FirstOrDefault();

                if (winner != null)
                {
                    var staff = _context.Staffs.FirstOrDefault(t => t.StaffId == winner.StaffId);
                    var dept = _context.Departments.First(t => t.DepartmentId == staff.DepartmentId);

                    FillPrizeWinner(staff, dept, i);

                    _savedWinners.Add(winner.StaffId, staff.StaffCode);

                    _prizedIndex--;
                    _prizedWinners.Add(staff.StaffId);
                }
            }

            var index = 1;
            foreach (var winner in loadWinners)
            {
                var staff = _context.Staffs.FirstOrDefault(i => i.StaffId == winner.StaffId);
                var dept = _context.Departments.First(i => i.DepartmentId == staff.DepartmentId);

                FillWinner(staff, dept, index - 1);
                //record the winner list

                _savedWinners.Add(winner.StaffId, staff.StaffCode);

                //fill prize seal
                var prizeId = loadWinners.First(i => i.StaffId == staff.StaffId).PrizeId;
                if (prizeId == -1)
                {
                    index++;
                    continue;
                }

                //int indexArray = 0;
                //for (indexArray = 0; indexArray < _prizeIds.Count; indexArray++)
                //{
                //    if (_prizeIds[indexArray] == prizeId)
                //        break;
                //}

                ////prized, fill the prize rank
                ////var number = (PrizeIndex - 1) * 3 + 4 - index;
                //var number = (PrizeIndex - 1) * 3 + 1 + indexArray;
                //var grade = staff.StaffCode.Substring(0, 2);
                //switch (grade)
                //{
                //    case "10":
                //        pictureBoxNumber1.ImageLocation = "numbers/" + number + ".png";
                //        break;
                //    case "11":
                //        pictureBoxNumber2.ImageLocation = "numbers/" + number + ".png";
                //        break;
                //    case "12":
                //        pictureBoxNumber3.ImageLocation = "numbers/" + number + ".png";
                //        break;
                //}

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
                       join j in _context.Prizes on i.PrizeId equals j.PrizeId
                       join k in _context.PrizeSettings on j.PrizeType equals k.PrizeType
                       where i.StaffId == staff.StaffId &&
                             k.PrizeType != 1 && k.XAdHoc == false
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

            var winners = _context.Winners.Where(t=>t.PrizeId == -1);

            FillWinner(staff, dept, (winners.Count() % 3));
            
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
                    this.BackgroundImage = Properties.Resources.SuperGrand_winners7_8_9;
                    break;
                case 6:
                case 5:
                case 4:
                    this.BackgroundImage = Properties.Resources.SuperGrand_winners_4_5_6;
                    break;
                case 3:
                case 2:
                case 1:
                    this.BackgroundImage = Properties.Resources.SuperGrand_winners1_2_3;
                    break;
                default:
                    this.BackgroundImage = Properties.Resources.SuperGrand_winners7_8_9;
                    break;
            }
        }

        private void FillWinner(Staffs staff, Departments dept, int prizeCurrent)
        {
            var avatar = Utilities.ScaleImage(staff.Avatar, imageLucky1.Width, imageLucky1.Height);
            var nick = string.IsNullOrWhiteSpace(staff.Nick) ? staff.FirstName.ToUpper() : staff.Nick.ToUpper();
            string fullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(staff.FullName.ToLower());

          //  var grade = staff.StaffCode.Substring(0, 2);

            switch (prizeCurrent)
            {
                case 0:
                    codeLucky1.Text = staff.StaffCode;
                    nameLucky1.Text = fullName;
                    nickLucky1.Text = nick;
                    deptLucky1.Text = dept.ShortName.ToUpper();
                    imageLucky1.Image = avatar;
                    break;
                case 1:
                    codeLucky2.Text = staff.StaffCode;
                    nameLucky2.Text = fullName;
                    nickLucky2.Text = nick;
                    deptLucky2.Text = dept.ShortName.ToUpper();
                    imageLucky2.Image = avatar;
                    break;
                case 2:
                    codeLucky3.Text = staff.StaffCode;
                    nameLucky3.Text = fullName;
                    nickLucky3.Text = nick;
                    deptLucky3.Text = dept.ShortName.ToUpper();
                    imageLucky3.Image = avatar;
                    break;

            }
        }

        private void FillPrizeWinner(Staffs staff, Departments dept, int prizeCurrent)
        {
            var avatar = Utilities.ScaleImage(staff.Avatar, imageLucky1.Width, imageLucky1.Height);
            var nick = string.IsNullOrWhiteSpace(staff.Nick) ? staff.FirstName.ToUpper() : staff.Nick.ToUpper();
            string fullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(staff.FullName.ToLower());

            //  var grade = staff.StaffCode.Substring(0, 2);

            switch (prizeCurrent)
            {
                case 0:
                    codeWinner1.Text = staff.StaffCode;
                    nameWinner1.Text = fullName;
                    nickWinner1.Text = nick;
                    deptWinner1.Text = dept.ShortName.ToUpper();
                    imageWinner1.Image = avatar;
                    break;
                case 1:
                    codeWinner2.Text = staff.StaffCode;
                    nameWinner2.Text = fullName;
                    nickWinner2.Text = nick;
                    deptWinner2.Text = dept.ShortName.ToUpper();
                    imageWinner2.Image = avatar;
                    break;
                case 2:
                    codeWinner3.Text = staff.StaffCode;
                    nameWinner3.Text = fullName;
                    nickWinner3.Text = nick;
                    deptWinner3.Text = dept.ShortName.ToUpper();
                    imageWinner3.Image = avatar;
                    break;

            }
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
                    pictureBoxNumber1.Image = null;
                    imageLucky1.Image = null;
                    break;
                case 2:
                    codeLucky2.Text = string.Empty;
                    pictureBoxNumber2.Image = null;
                    imageLucky2.Image = null;
                    break;
                case 3:
                    codeLucky3.Text = string.Empty;
                    pictureBoxNumber3.Image = null;
                    imageLucky3.Image = null;
                    break;
            }
        }

        //private void ResetPrize()
        //{
        //    if (_prizedWinners.Count == 0) return;

        //    var result = MessageBox.Show(
        //        "Are you sure you want to reset prizes?",
        //        "Year End Party 2012",
        //        MessageBoxButtons.YesNo,
        //        MessageBoxIcon.Question
        //        );
        //    if (result == DialogResult.No)
        //    {
        //        return;
        //    }

        //    //remove prizeId = -1
        //    var winners = _context.Winners.Where(i => _savedWinners.Keys.Contains(i.StaffId) && (i.PrizeId == -1 || _prizeIds.Contains(i.PrizeId)));
        //    foreach (var item in winners)
        //    {
        //        _context.Winners.DeleteObject(item);
        //    }
        //    _context.SaveChanges();

        //    //re-insert
        //    foreach (var item in _savedWinners)
        //    {
        //        var entity = new Winners
        //        {
        //            StaffId = item.Key,
        //            PrizeId = -1,
        //            CreatedDate = DateTime.Now
        //        };
        //        _context.Winners.AddObject(entity);
        //    }
        //    _context.SaveChanges();

        //    //update image
        //    pictureBoxNumber1.Image = null;
        //    pictureBoxNumber2.Image = null;
        //    pictureBoxNumber3.Image = null;

        //    if (_prizedWinners.Count == 3 && _prizedIndex == 0)
        //    {
        //        var setting = _context.PrizeSettings.FirstOrDefault(i => i.PrizeType == (int)PrizeTypes.SuperGrand);
        //        if (setting.CurrentPrize * 3 < setting.Quantity)
        //        {
        //            if (isFinished)
        //            {
        //                setting.CurrentPrize = setting.CurrentPrize + 1;
        //                _context.SaveChanges();
        //                isFinished = false;
        //            }
        //        }
        //    }

        //    _prizedIndex = 3;
        //    _prizedWinners = new List<int>(3);
        //}

        private void ResetPrizeWinners()
        {
            if (_savedWinners.Count == 0) return;

            codeLucky1.Text = codeWinner1.Text;
            nickLucky1.Text = nickWinner1.Text;
            nameLucky1.Text = nameWinner1.Text;
            deptLucky1.Text = deptWinner1.Text;
            imageLucky1.Image = imageWinner1.Image;

            codeLucky2.Text = codeWinner2.Text;
            nickLucky2.Text = nickWinner2.Text;
            nameLucky2.Text = nameWinner2.Text;
            deptLucky2.Text = deptWinner2.Text;
            imageLucky2.Image = imageWinner2.Image;

            codeLucky3.Text = codeWinner3.Text;
            nickLucky3.Text = nickWinner3.Text;
            nameLucky3.Text = nameWinner3.Text;
            deptLucky3.Text = deptWinner3.Text;
            imageLucky3.Image = imageWinner3.Image;

            imageWinner1.Image = null;
            imageWinner2.Image = null;
            imageWinner3.Image = null;

            

            var winners = _context.Winners.Where(i => _savedWinners.Keys.Contains(i.StaffId) && (i.PrizeId == -1 || _prizeIds.Contains(i.PrizeId)));

            //remove prizeId = -1
            //var winners = _context.Winners.Where(i => _savedWinners.Keys.Contains(i.StaffId) && (i.PrizeId == -1 || _prizeIds.Contains(i.PrizeId)));
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
            setting.CurrentPrize = ((setting.CurrentPrize - 1) / 3 + 1) * 3;

            //if (_prizedWinners.Count == 3 && _prizedIndex == 0)
            //{
            //   // var setting = _context.PrizeSettings.FirstOrDefault(i => i.PrizeType == (int)PrizeTypes.SuperGrand);
            //    if (setting.CurrentPrize * 3 < setting.Quantity)
            //    {
            //        if (isFinished)
            //        {
            //            setting.CurrentPrize = setting.CurrentPrize + 1;
            //            isFinished = false;
            //        }

            //        isDeleted = true;
            //    }
            //}

            _context.SaveChanges();

            Remove(1); Remove(2); Remove(3);
            imageWinner1.Image = null;
            imageWinner2.Image = null;
            imageWinner3.Image = null;

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
                "Year End Party 2012",
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
                //case 37:
                //    if (_focus > 1)
                //    {
                //        _focus--;
                //        SetFocus();
                //    }
                //    break;
                //case 39:
                //    if (_focus < 3)
                //    {
                //        _focus++;
                //        SetFocus();
                //    }

                case 38:
                    if (_focus > 1)
                    {
                        _focus--;
                        SetFocus();
                    }
                    break;
                case 40:
                    if (_focus < 3)
                    {
                        _focus++;
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
                    pictureBox4.Visible = true;
                    pictureBox5.Visible = false;
                    pictureBox6.Visible = false;
                    break;
                case 2:
                    pictureBox4.Visible = false;
                    pictureBox5.Visible = true;
                    pictureBox6.Visible = false;
                    break;
                case 3:
                    pictureBox4.Visible = false;
                    pictureBox5.Visible = false;
                    pictureBox6.Visible = true;
                    break;
                default:
                    pictureBox4.Visible = false;
                    pictureBox5.Visible = false;
                    pictureBox6.Visible = true;
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

                    switch (_focus)
                    {
                        case 1:
                            staffID = codeLucky1.Text;
                            nick = nickLucky1.Text;
                            name = nameLucky1.Text;
                            department = deptLucky1.Text;
                            break;

                        case 2:
                            staffID = codeLucky2.Text;
                            nick = nickLucky2.Text;
                            name = nameLucky2.Text;
                            department = deptLucky2.Text;
                            break;
                        case 3:
                            staffID = codeLucky3.Text;
                            nick = nickLucky3.Text;
                            name = nameLucky3.Text;
                            department = deptLucky3.Text;
                            break;
                    }

                    if (!string.IsNullOrEmpty(staffID))
                    {
                        var staff = _context.Staffs.FirstOrDefault(i => i.StaffCode == staffID);
                        if (staff == null) return;

                        var avatar = Utilities.ScaleImage(staff.Avatar, imageWinner1.Width, imageWinner1.Height);

                        _prizedIndex--;
                        UpdateWinnerPrize(staff.StaffId, _prizeIds[prize - 1]);

                        switch (prize)
                        {
                            case 1:
                                //pictureBoxNumber1.ImageLocation = "Numbers/" + prize + ".png";
                                codeWinner1.Text = staffID;
                                nickWinner1.Text = nick;
                                nameWinner1.Text = name;
                                deptWinner1.Text = department;
                                imageWinner1.Image = avatar;
                                break;
                            case 2:
                                codeWinner2.Text = staffID;
                                nickWinner2.Text = nick;
                                nameWinner2.Text = name;
                                deptWinner2.Text = department;
                                imageWinner2.Image = avatar;
                                //pictureBoxNumber2.ImageLocation = "Numbers/" + prize + ".png";
                                break;
                            case 3:
                                codeWinner3.Text = staffID;
                                nickWinner3.Text = nick;
                                nameWinner3.Text = name;
                                deptWinner3.Text = department;
                                imageWinner3.Image = avatar;
                                //pictureBoxNumber3.ImageLocation = "Numbers/" + prize + ".png";
                                break;
                        }
                    }

                }
            }
        }

    }
}

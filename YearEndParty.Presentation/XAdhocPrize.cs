using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Objects;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using log4net;
using YearEndParty.Core;

namespace YearEndParty.Presentation
{
    public partial class XAdhocPrize : Form
    {

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
        private IList<Label> barcodes = new List<Label>();
        private IList<Label> names = new List<Label>();
        private IList<Label> depts = new List<Label>();

        private int _prizedTotal;
        private int _prizeType = -1;
        private int _quantity;

        public XAdhocPrize()
        {
            DoubleBuffered = true;
            InitializeComponent();
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
                case (char)57:
                    RemoveWinner(8);
                    break;
                case (char)48:
                    RemoveWinner(9);
                    break;
            }
        }

        public void Barcode(string barcode)
        {
            if (_prizeType == -1) return;

            _context.PrizeSettings.MergeOption = MergeOption.OverwriteChanges;
            var setting = _context.PrizeSettings.First(i => i.PrizeType == _prizeType);

            //there are no more price for staffs
            if (_prizedTotal >= setting.Quantity)
            {
                TalkBack("Full");
                return;
            }

            if (_winners.Any(i => i == barcode))
            {
                //cheat? heh... write log
                Log.Fatal("XAdHoc | Barcode duplicated, ID = " + barcode);
                TalkBack("Barcode duplicated");
                return;
            }

            var staff = _context.Staffs.First(i => i.StaffCode == barcode);
            var dept = _context.Departments.First(i => i.DepartmentId == staff.DepartmentId);

            bool won;
            if (!setting.DuplicateAllow)
            {
                var name = (from i in _context.Winners
                            join j in _context.Prizes on i.PrizeId equals j.PrizeId
                            join k in _context.PrizeSettings on j.PrizeType equals k.PrizeType
                            where i.StaffId == staff.StaffId && k.PrizeType > 1 && k.PrizeType < 5
                            select k.Name).First();

                if (!string.IsNullOrWhiteSpace(name))
                {
                    //cheat? heh... write log
                    Log.Fatal("XAdHoc | Duplicate from " + name + ", ID = " + barcode);
                    TalkBack("Duplicate from " + name);
                    return;
                }
            }

            if (!setting.DuplicateFromAdHoc)
            {
                won = (from i in _context.Winners
                       join j in _context.Prizes on i.PrizeId equals j.PrizeId
                       join k in _context.PrizeSettings on j.PrizeType equals k.PrizeType
                       where i.StaffId == staff.StaffId && k.PrizeType == 1
                       select i).Any();
                if (won)
                {
                    //cheat? heh... write log
                    Log.Fatal("XAdHoc | Duplicate from AdHoc, ID = " + barcode);
                    TalkBack("Duplicate from AdHoc");
                    return;
                }

            }

            //duplicate in this prize
            won = _context.Winners.Any(i => i.PrizeId == _prizeId && i.StaffId == staff.StaffId);
            if (won)
            {
                //cheat? heh... write log
                Log.Fatal("XAdHoc | Barcode duplicated, ID = " + barcode);
                TalkBack("Barcode duplicated");
                return;
            }

            labelNick.Text = barcode;
            labelName.Text = staff.FullName;
            labelNick.Text = string.IsNullOrWhiteSpace(staff.Nick) ? staff.FirstName : staff.Nick;
            labelDept.Text = dept.ShortName;

            pictureBoxWinner.Image = Utilities.ScaleImage(staff.Avatar, 240, 260);


            var index = -1;
            for (var i = 0; i < 10; i++)
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
                index = 0;
            }

            var color = GetColor(barcode.Substring(0, 2));
            pictures[index].Image = Utilities.ScaleImage(staff.Avatar, 120, 130);
            barcodes[index].Text = staff.StaffCode;
            barcodes[index].BackColor = color;
            names[index].Text = staff.FullName;
            names[index].ForeColor = color;
            depts[index].Text = dept.ShortName;
            depts[index].ForeColor = color;

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

            lableStatus.Text = string.Format("{0}/{1}", _prizedTotal, _quantity);
        }

        private void AdhocPrize_Load(object sender, EventArgs e)
        {
            Visible = false;

            IsClosed = false;
            var left = (Width - 990) / 2;
            var top = (Height - 440) / 2 + int.Parse(ConfigurationManager.AppSettings["AdHoc.Top"]);

            AdHocList.Top = top - 50;
            AdHocList.Left = 760;

            pictureBoxWinner.Left = left;
            pictureBoxWinner.Top = top;

            labelName.Left = left;
            labelName.Top = top + pictureBoxWinner.Height + 15;

            labelNick.Left = left;
            labelNick.Top = labelName.Top + labelName.Height;

            labelDept.Left = left;
            labelDept.Top = labelNick.Top + labelNick.Height;

            lableStatus.Left = pictureBoxWinner.Left + (pictureBoxWinner.Width - lableStatus.Width) / 2;
            lableStatus.Top = labelDept.Top + 40;

            left = left + 240 + 30;

            const int row = 2;
            const int col = 5;
            for (var r = 0; r < row; r++)
            {
                top = r * 240 + top;
                for (var c = 0; c < col; c++)
                {
                    var picture = new PictureBox
                                      {
                                          //BackgroundImage = Properties.Resources.anonymous,
                                          BackColor = Color.White,
                                          Location = new Point
                                                         {
                                                             X = left + c * 150,
                                                             Y = top
                                                         },
                                          Size = new Size(120, 130)
                                      };

                    var barcode = new Label
                    {
                        BackColor = Color.Transparent,
                        ForeColor = Color.White,
                        Padding = new Padding(0),
                        AutoSize = false,
                        Margin = new Padding(0),
                        TextAlign = ContentAlignment.TopLeft,
                        Location = new Point
                        {
                            X = left + c * 150,
                            Y = top + 130
                        },
                        Size = new Size(120, 20),
                        Font = new Font("Tahoma", 11F, FontStyle.Regular, GraphicsUnit.Point, 0)
                    };

                    var name = new Label
                    {
                        BackColor = Color.Transparent,
                        ForeColor = Color.White,
                        Padding = new Padding(0),
                        AutoSize = false,
                        Margin = new Padding(0),
                        TextAlign = ContentAlignment.TopLeft,
                        Location = new Point
                        {
                            X = left + c * 150,
                            Y = top + 165
                        },
                        Size = new Size(120, 40),
                        Font = new Font("Tahoma", 11F, FontStyle.Regular, GraphicsUnit.Point, 0)
                    };

                    var dept = new Label
                    {
                        BackColor = Color.Transparent,
                        ForeColor = Color.White,
                        Padding = new Padding(0),
                        AutoSize = false,
                        Margin = new Padding(0),
                        TextAlign = ContentAlignment.MiddleLeft,
                        Location = new Point
                        {
                            X = left + c * 150,
                            Y = top + 205
                        },
                        Size = new Size(120, 20),
                        Font = new Font("Tahoma", 11F, FontStyle.Regular, GraphicsUnit.Point, 0)
                    };

                    pictures.Add(picture);
                    barcodes.Add(barcode);
                    names.Add(name);
                    depts.Add(dept);
                    Controls.Add(picture);
                    Controls.Add(barcode);
                    Controls.Add(name);
                    Controls.Add(dept);
                }
            }

            _context = new bodb_luckyEntities();

            var adhocs = (from i in _context.PrizeSettings
                          where i.XAdHoc
                          select new NameValue
                                     {
                                         Name = i.Name,
                                         Value = i.PrizeType
                                     }).ToList();
            foreach (var adhoc in adhocs)
            {
                AdHocList.Items.Add(adhoc);
            }

            if (AdHocList.SelectedValue != null)
                _prizeType = int.Parse(AdHocList.SelectedValue.ToString());

            if (_prizeType != -1) LoadWinners();

            Visible = true;
        }

        private void LoadWinners()
        {
            //reset state
            pictureBoxWinner.Image = null;
            labelName.Text = string.Empty;
            labelNick.Text = string.Empty;
            labelDept.Text = string.Empty;
            _winners = new List<string>();


            for (var i = 0; i < 10; i++)
            {
                pictures[i].Image = null;
                names[i].Text = string.Empty;
                depts[i].Text = string.Empty;
                barcodes[i].Text = string.Empty;
                barcodes[i].BackColor = Color.Transparent;
            }

            //var x = AdHocList.SelectedItem;

            var setting = _context.PrizeSettings.First(i => i.PrizeType == _prizeType);
            _quantity = setting.Quantity;

            _prizeId = _context.Prizes.First(i => i.PrizeType == _prizeType).PrizeId;

            var winners = _context.Winners.Where(i => i.PrizeId == _prizeId).OrderBy(i => i.CreatedDate).ToList();
            _prizedTotal = winners.Count;

            lableStatus.Text = string.Format("{0}/{1}", _prizedTotal, _quantity);

            var index = 0;
            var staffs = (from i in _context.Staffs
                          join j in _context.Winners
                              on i.StaffId equals j.StaffId
                          where j.PrizeId == _prizeId
                          select i).ToList();
            var departments = _context.Departments.ToList();

            foreach (var winner in winners)
            {
                if (index == 10) break;
                var staff = staffs.FirstOrDefault(i => i.StaffId == winner.StaffId);
                var dept = departments.First(i => i.DepartmentId == staff.DepartmentId);

                _winners.Add(staff.StaffCode);

                var color = GetColor(staff.StaffCode.Substring(0, 2));

                pictures[index].Image = Utilities.ScaleImage(staff.Avatar, 120, 130);
                barcodes[index].Text = staff.StaffCode;
                barcodes[index].BackColor = color;
                names[index].Text = staff.FullName;
                names[index].ForeColor = color;
                depts[index].Text = dept.ShortName;
                depts[index++].ForeColor = color;
            }
        }

        private static Color GetColor(string grade)
        {
            switch (grade)
            {
                case "10":
                    return Color.Blue;
                case "11":
                    return Color.Green;
                case "12":
                    return Color.Orange;
                default:
                    return Color.White;
            }
        }
        private void ScrollWinners()
        {
            for (var i = 9; i > 0; i--)
            {
                pictures[i].Image = pictures[i - 1].Image;

                barcodes[i].Text = barcodes[i - 1].Text;
                barcodes[i].BackColor = barcodes[i - 1].BackColor;

                names[i].Text = names[i - 1].Text;
                names[i].ForeColor = names[i - 1].ForeColor;

                depts[i].Text = depts[i - 1].Text;
                depts[i].ForeColor = depts[i - 1].ForeColor;
            }
        }

        private void RemoveWinner(int index)
        {
            if (_winners.Count == 0 || string.IsNullOrWhiteSpace(names[index].Text)) return;

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
            barcodes[index].BackColor = Color.Transparent;
            names[index].Text = string.Empty;
            depts[index].Text = string.Empty;

            var staffId = _context.Staffs.First(i => i.StaffCode == barcode).StaffId;

            var winner = _context.Winners.First(i => i.StaffId == staffId && i.PrizeId == _prizeId);
            _context.Winners.DeleteObject(winner);
            _context.SaveChanges();
            _winners.Remove(barcode);
            _prizedTotal--;

            lableStatus.Text = string.Format("{0}/{1}", _prizedTotal, _quantity);

        }

        private void AdHocList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AdHocList.SelectedItem != null)
            {
                var prize = AdHocList.SelectedItem as NameValue;
                _prizeType = prize.Value;
            }

            if (_prizeType != -1) LoadWinners();
        }
    }
}
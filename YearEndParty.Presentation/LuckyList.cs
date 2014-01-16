using System.Linq;
using System.Windows.Forms;
using YearEndParty.Core;
using YearEndParty.Presentation.Controls;
using System.Drawing;
using System.Collections;
using System.Configuration;

namespace YearEndParty.Presentation
{
    public partial class LuckyList : Form
    {
        //width & height of image
        private const int WidthImage = 1408;
        private const int HeightImage = 640;

        //width & height of screen
        private int WidthScreen = 0;
        private int HeightScreen = 0;

        public int PrizeType { get; set; }
        private int prizeNameSize;
        

        public LuckyList()
        {
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

        private void LuckyList_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)27)
            {
                Close();
            }
        }

        private void LuckyList_Load(object sender, System.EventArgs e)
        {
            WidthScreen = Screen.PrimaryScreen.Bounds.Width;
            HeightScreen = Screen.PrimaryScreen.Bounds.Height;

            prizeNameSize = int.Parse(ConfigurationManager.AppSettings["listWinner_PrizeNameSize"]);
            
            switch (PrizeType)
            {
                case (int)PrizeTypes.Cash:
                    //BackgroundImage = Properties.Resources.cashwinnerlist_bg;
                    ShowWinners();
                    break;
                case (int)PrizeTypes.Major:
                   // BackgroundImage = Properties.Resources.majorwinnerlist_bg;
                    ShowWinners();
                    break;
                case (int)PrizeTypes.Grand:
                    //BackgroundImage = Properties.Resources.grandwinnerlist_bg;
                    ShowWinners();
                    break;
                case (int)PrizeTypes.SuperGrand:
                    //BackgroundImage = Properties.Resources.winnerlistsuper_version2_bg;
                    ShowSupperWinners();
                    break;
                case (int)PrizeTypes.AdHoc:
                   // BackgroundImage = Properties.Resources.adhocwinnerlist_bg;
                    ShowAdhocWinners();
                    break;
            }
        }

        /// <summary>
        /// This function is followed of image which designer provider
        /// With Image in LuckyDraw 2012 has 3 column for Cash, Major, Grand
        /// </summary>
        private void ShowWinners()
        {
            #region Load 3 columns with position & size
            // 3 columns
            // top & left of first columns
            var left = 84;
            var top = 180;
            //width & height of column
            var width = 380;
            var height = 400;
            //space of between two columns
            var space = 70;

            //position & size of column 1
            panel_column1.Top = top * HeightScreen / HeightImage;
            panel_column1.Left = left * WidthScreen / WidthImage;
            panel_column1.Width = width * WidthScreen / WidthImage;
            panel_column1.Height = height * HeightScreen / HeightImage;

            //position & size of column 2
            panel_column2.Top = top * HeightScreen / HeightImage;
            panel_column2.Left = (left + width + space) * WidthScreen / WidthImage;
            panel_column2.Width = panel_column1.Width;
            panel_column2.Height = panel_column1.Height;

            //position & size of column 3
            panel_column3.Top = top * HeightScreen / HeightImage;
            panel_column3.Left = (left + 2 * width + 2 * space) * WidthScreen / WidthImage;
            panel_column3.Width = panel_column1.Width;
            panel_column3.Height = panel_column1.Height;

            #endregion

            #region Get Winners
            var context = new bodb_luckyEntities();
            var winners = (from i in context.Staffs
                           join j in context.Winners on i.StaffId equals j.StaffId
                           join k in context.Prizes on j.PrizeId equals k.PrizeId
                           join c in context.Departments on i.DepartmentId equals c.DepartmentId
                           where k.PrizeType == PrizeType
                           orderby k.PriceIndex ascending
                           select new
                           {
                               i.FullName,
                               Department = c.ShortName,
                               Prize = k.Name,
                               k.PrizeId,
                               k.PrizeType
                           }).ToList();

            //var setting = context.PrizeSettings.First(i => i.PrizeType == PrizeType);
            #endregion

            #region Fill Winners
            //save top position load
            var topWinner = 0;
            //save Prize Name --> to determine when pass different prize name
            var tempPrize = string.Empty;
            //save PrizeID
            var tempPrizeId = -1;
            //save current column
            var currentColumn = 0;

            var count1 = 1;
            var count2 = 1;

            //15 maximum is 15 rows
            var maxRow = 15; 
            foreach (var item in winners)
            {
                if (tempPrizeId != -1)
                {
                    if (item.Prize == tempPrize)
                    {
                        if (item.PrizeId == tempPrizeId)
                        {
                            topWinner = topWinner + panel_column1.Height / maxRow + 1; //1: space 
                        }
                        else
                        {
                            count1++;
                            topWinner = topWinner + panel_column1.Height / maxRow + 8; //8: space
                        }
                    }
                    else
                    {
                        count2++;
                        count1++;
                        topWinner = topWinner + panel_column1.Height / maxRow + 15; //15: space

                        //conditions to go another prize name
                        if (!(tempPrizeId == -1 || count2 > 3 || (count2 == 3 && count1 == 4) || (count2 == 2 && count1 == 5)))
                        {
                            //Label prize
                            Label labelPrize = new Label();
                            labelPrize.Width = panel_column1.Width;
                            labelPrize.Height = panel_column1.Height / maxRow;
                            labelPrize.Top = topWinner;
                            labelPrize.Font = new System.Drawing.Font("Arial", prizeNameSize, System.Drawing.FontStyle.Bold);
                            labelPrize.ForeColor = Color.White;
                            labelPrize.Text = item.Prize;

                            //Draw line
                            var topLine = topWinner + panel_column1.Height / maxRow - 5 * HeightScreen / HeightImage; //5: space
                            var line = new Microsoft.VisualBasic.PowerPacks.LineShape
                            {
                                BorderColor = Color.White,
                                BorderWidth = 1,
                                X1 = 5, //padding left: 5
                                Y1 = topLine,
                                X2 = panel_column1.Width - 10, //padding right: 5 (10-5)
                                Y2 = topLine,
                            };

                            if (currentColumn == 1)
                            {
                                panel_column1.Controls.Add(labelPrize);
                                this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] { line });
                            }
                            else
                            {
                                if (currentColumn == 2)
                                {
                                    panel_column2.Controls.Add(labelPrize);
                                    this.shapeContainer2.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] { line });
                                }
                                else
                                {
                                    panel_column3.Controls.Add(labelPrize);
                                    this.shapeContainer3.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] { line });
                                }
                            }

                            topWinner = topWinner + panel_column1.Height / maxRow;
                        }

                    }
                }

                //conditions to go another column
                if (tempPrizeId == -1 || count2 > 3 || (count2 == 3 && count1 == 4) || (count2 == 2 && count1 == 5))
                {
                    currentColumn++;
                    count1 = 1;
                    count2 = 1;
                    topWinner = 0;

                    //Label prize
                    Label labelPrize = new Label();
                    labelPrize.Width = panel_column1.Width;
                    labelPrize.Height = panel_column1.Height / maxRow;
                    labelPrize.Top = topWinner;
                    labelPrize.Font = new System.Drawing.Font("Arial", prizeNameSize, System.Drawing.FontStyle.Bold);
                    labelPrize.ForeColor = Color.White;
                    labelPrize.Text = item.Prize;

                    //Draw line
                    var topLine = topWinner + panel_column1.Height / maxRow - 5 * HeightScreen / HeightImage;
                    var line = new Microsoft.VisualBasic.PowerPacks.LineShape
                    {
                        BorderColor = Color.White,
                        BorderWidth = 1,
                        X1 = 5,
                        Y1 = topLine,
                        X2 = panel_column1.Width - 10,
                        Y2 = topLine,
                    };

                    if (currentColumn == 1)
                    {
                        panel_column1.Controls.Add(labelPrize);
                        this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] { line });
                    }
                    else
                    {
                        if (currentColumn == 2)
                        {
                            panel_column2.Controls.Add(labelPrize);
                            this.shapeContainer2.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] { line });
                        }
                        else
                        {
                            panel_column3.Controls.Add(labelPrize);
                            this.shapeContainer3.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] { line });
                        }
                    }

                    topWinner = topWinner + panel_column1.Height / maxRow;
                }

                //fill winners
                WinnerTile uc1 = new WinnerTile(item.FullName, item.Department);
                uc1.Width = panel_column1.Width;
                //Height of 1 row = height of column / 15
                uc1.Height = panel_column1.Height / maxRow; 
                uc1.Top = topWinner;

                if (currentColumn == 1)
                {
                    panel_column1.Controls.Add(uc1);
                }
                else
                {
                    if (currentColumn == 2)
                    {
                        panel_column2.Controls.Add(uc1);
                    }
                    else
                    {
                        panel_column3.Controls.Add(uc1);
                    }
                }

                //count++;
                tempPrize = item.Prize;
                tempPrizeId = item.PrizeId;
            }

            #endregion
        }

        /// <summary>
        /// This function is followed of image which designer provider
        /// With Image in LuckyDraw 2012 has 2 column for Adhoc
        /// </summary>
        private void ShowAdhocWinners()
        {
            #region Load 2 columns with position & size
            // 2 columns
            // top & left of first columns
            var left = 240;
            var top = 180;
            //width & height of 1 column
            var width = 452;
            var height = 384;
            //space of between two columns
            var space = 80;

            //position & size of column 1
            panel_column1.Top = top * HeightScreen / HeightImage;
            panel_column1.Left = left * WidthScreen / WidthImage;
            panel_column1.Width = width * WidthScreen / WidthImage;
            panel_column1.Height = height * HeightScreen / HeightImage;

            //position & size of column 2
            panel_column2.Top = top * HeightScreen / HeightImage;
            panel_column2.Left = (left + width + space) * WidthScreen / WidthImage;
            panel_column2.Width = panel_column1.Width;
            panel_column2.Height = panel_column1.Height;

            panel_column3.Visible = false;

            #endregion

            #region Get Winners
            var context = new bodb_luckyEntities();
            var winners = (from i in context.Staffs
                           join j in context.Winners on i.StaffId equals j.StaffId
                           join k in context.Prizes on j.PrizeId equals k.PrizeId
                           join c in context.Departments on i.DepartmentId equals c.DepartmentId
                           where k.PrizeType == PrizeType
                           orderby k.PriceIndex ascending
                           select new
                           {
                               i.FullName,
                               Department = c.ShortName,
                               Prize = k.Name,
                               k.PrizeId,
                               k.PrizeType
                           }).ToList();

            //var setting = context.PrizeSettings.First(i => i.PrizeType == PrizeType);
            #endregion

            #region Fill Winners
            //save top position load
            var topWinner = 0;
            int count = 1;
            //save current column
            var currentColumn = 0;
            //12 maximum is 12 rows
            var maxRow = 12; 

            foreach (var item in winners)
            {
                WinnerTile uc1 = new WinnerTile(item.FullName, item.Department);
                uc1.Width = panel_column1.Width;
                uc1.Height = panel_column1.Height / maxRow;
                uc1.Top = topWinner;

                currentColumn = (count - 1) / maxRow + 1;

                if (currentColumn == 1)
                {
                    panel_column1.Controls.Add(uc1);
                }
                else
                {
                    if (currentColumn == 2)
                    {
                        panel_column2.Controls.Add(uc1);
                    }
                    else
                    {
                        panel_column3.Controls.Add(uc1);
                    }
                }

                count++;

                topWinner = topWinner + panel_column1.Height / maxRow;
                if ((count - 1) % maxRow == 0)
                {
                    topWinner = 0;
                }
            }

            #endregion
        }

        /// <summary>
        /// This function is followed of image which designer provider
        /// With Image in LuckyDraw 2012 has 2 column for Super grand
        /// </summary>
        private void ShowSupperWinners()
        {
            #region Load 2 columns with position & size
            // 2 columns
            // top & left of first columns
            var left = 240;
            var top = 180;
            //width & height of 1 column
            var width = 452;
            var height = 384;
            //space of between two columns
            var space = 80;

            //position & size of column 1
            panel_column1.Top = top * HeightScreen / HeightImage;
            panel_column1.Left = left * WidthScreen / WidthImage;
            panel_column1.Width = width * WidthScreen / WidthImage;
            panel_column1.Height = height * HeightScreen / HeightImage;

            //position & size of column 2
            panel_column2.Top = top * HeightScreen / HeightImage;
            panel_column2.Left = (left + width + space) * WidthScreen / WidthImage;
            panel_column2.Width = panel_column1.Width;
            panel_column2.Height = panel_column1.Height;

            panel_column3.Visible = false;
            #endregion

            #region Get Winners
            var context = new bodb_luckyEntities();
            var winners = (from i in context.Staffs
                           join j in context.Winners on i.StaffId equals j.StaffId
                           join k in context.Prizes on j.PrizeId equals k.PrizeId
                           join c in context.Departments on i.DepartmentId equals c.DepartmentId
                           where k.PrizeType == PrizeType
                           orderby k.PriceIndex ascending
                           select new
                           {
                               i.FullName,
                               Department = c.ShortName,
                               Prize = k.Name,
                               k.PrizeId,
                               k.PrizeType
                           }).ToList();

            //var setting = context.PrizeSettings.First(i => i.PrizeType == PrizeType);
            #endregion

            #region Fill Winners
            //save top position load
            var topWinner = 0;
            //save Prize Name --> to determine when pass different prize name
            var tempPrize = string.Empty;
            //save PrizeID
            var tempPrizeId = -1;
            //save current column
            var currentColumn = 0;
            //15 maximum is 15 rows
            var maxRow = 15;

            var count1 = 1;
            var count2 = 1;

            foreach (var item in winners)
            {
                if (tempPrizeId != -1)
                {
                    if (item.Prize == tempPrize)
                    {
                        if (item.PrizeId == tempPrizeId)
                        {
                            topWinner = topWinner + panel_column1.Height / maxRow + 1; //1: space
                        }
                        else
                        {
                            count1++;
                            topWinner = topWinner + panel_column1.Height / maxRow + 8; //8: space
                        }
                    }
                    else
                    {
                        count2++;
                        count1++;
                        topWinner = topWinner + panel_column1.Height / maxRow + 15; //15: space
                        //conditions to go another prize name
                        if (!(tempPrizeId == -1 || count2 > 4))
                        {
                            //Label prize
                            Label labelPrize = new Label();
                            labelPrize.Width = panel_column1.Width;
                            labelPrize.Height = panel_column1.Height / maxRow;
                            labelPrize.Top = topWinner;
                            labelPrize.Font = new System.Drawing.Font("Arial", prizeNameSize, System.Drawing.FontStyle.Bold);
                            labelPrize.ForeColor = Color.White;
                            labelPrize.Text = item.Prize;

                            //Draw line
                            var topLine = topWinner + panel_column1.Height / maxRow - 5 * HeightScreen / HeightImage;
                            var line = new Microsoft.VisualBasic.PowerPacks.LineShape
                            {
                                BorderColor = Color.White,
                                BorderWidth = 1,
                                X1 = 5,
                                Y1 = topLine,
                                X2 = panel_column1.Width - 10,
                                Y2 = topLine,
                            };

                            if (currentColumn == 1)
                            {
                                panel_column1.Controls.Add(labelPrize);
                                this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] { line });
                            }
                            else
                            {
                                if (currentColumn == 2)
                                {
                                    panel_column2.Controls.Add(labelPrize);
                                    this.shapeContainer2.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] { line });
                                }
                                else
                                {
                                    panel_column3.Controls.Add(labelPrize);
                                    this.shapeContainer3.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] { line });
                                }
                            }

                            topWinner = topWinner + panel_column1.Height / maxRow;
                        }

                    }
                }

                //conditions to go another column
                if (tempPrizeId == -1 || count2 > 4)
                {
                    currentColumn++;
                    count1 = 1;
                    count2 = 1;
                    topWinner = 0;

                    //Label prize
                    Label labelPrize = new Label();
                    labelPrize.Width = panel_column1.Width;
                    labelPrize.Height = panel_column1.Height / maxRow;
                    labelPrize.Top = topWinner;
                    labelPrize.Font = new System.Drawing.Font("Arial", prizeNameSize, System.Drawing.FontStyle.Bold);
                    labelPrize.ForeColor = Color.White;
                    labelPrize.Text = item.Prize;

                    //Draw line
                    var topLine = topWinner + panel_column1.Height / maxRow - 5 * HeightScreen / HeightImage;
                    var line = new Microsoft.VisualBasic.PowerPacks.LineShape
                    {
                        BorderColor = Color.White,
                        BorderWidth = 1,
                        X1 = 5,
                        Y1 = topLine,
                        X2 = panel_column1.Width - 10,
                        Y2 = topLine,
                    };

                    if (currentColumn == 1)
                    {
                        panel_column1.Controls.Add(labelPrize);
                        this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] { line });
                    }
                    else
                    {
                        if (currentColumn == 2)
                        {
                            panel_column2.Controls.Add(labelPrize);
                            this.shapeContainer2.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] { line });
                        }
                        else
                        {
                            panel_column3.Controls.Add(labelPrize);
                            this.shapeContainer3.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] { line });
                        }
                    }

                    topWinner = topWinner + panel_column1.Height / maxRow;
                }

                //fill winners
                WinnerTile uc1 = new WinnerTile(item.FullName, item.Department);
                uc1.Width = panel_column1.Width;
                uc1.Height = panel_column1.Height / maxRow;
                uc1.Top = topWinner;

                if (currentColumn == 1)
                    panel_column1.Controls.Add(uc1);
                else
                {
                    if (currentColumn == 2)
                        panel_column2.Controls.Add(uc1);
                    else
                        panel_column3.Controls.Add(uc1);
                }

                //count++;
                tempPrize = item.Prize;
                tempPrizeId = item.PrizeId;
            }
            #endregion
        }
        
    }
}

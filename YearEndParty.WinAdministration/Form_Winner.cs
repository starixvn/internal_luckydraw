using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using YearEndParty.Core;

namespace YearEndParty.WinAdministration
{
    public partial class Form_Winner : Form
    {
        public Form_Winner()
        {
            InitializeComponent();

            __LoadWinnerList();
        }


        private void __LoadWinnerList()
        {
            using (bodb_luckyEntities context = new bodb_luckyEntities())
            {
                var listWinner = (from winner in context.Winners
                                  from staff in context.Staffs
                                  from prize in context.Prizes
                                  from type in context.PrizeSettings
                                  where winner.StaffId == staff.StaffId
                                     && winner.PrizeId == prize.PrizeId
                                     && prize.PrizeType == type.PrizeType
                                  select new
                                  {
                                      StaffCode = staff.StaffCode,
                                      WinnerName = staff.FirstName + " " + staff.LastName,
                                      Prize = prize.Name,
                                      Type = type.Name
                                  }).ToList().Select((item, index) => new
                                  {
                                      Num = index + 1,
                                      StaffCode = item.StaffCode,
                                      Winner = item.WinnerName,
                                      PrizeName = item.Prize,
                                      PrizeType = item.Type
                                  }).ToList();
                if (listWinner.Count > 0)
                {
                    gridView_Winner.DataSource = listWinner;
                    gridView_Winner.ColumnHeadersDefaultCellStyle.Font = new Font(gridView_Winner.Font, FontStyle.Bold);
                    gridView_Winner.Columns[0].Width = 74;
                    gridView_Winner.Columns[1].Width = 100;
                    gridView_Winner.Columns[2].Width = 200;
                    gridView_Winner.Columns[3].Width = 200;
                    gridView_Winner.Columns[4].Width = 130;
                }
            }
        }
    }
}

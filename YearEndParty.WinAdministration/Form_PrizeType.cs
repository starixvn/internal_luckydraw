using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using YearEndParty.Core;
namespace YearEndParty.WinAdministration
{
    public partial class Form_PrizeType : Form
    {
        private PrizeSettings __prizeType;
        private Dictionary<int, string> __prizeList = new Dictionary<int, string>();

        public Form_PrizeType()
        {
            InitializeComponent();

            LoadPrizeType();
        }

        #region form function

        private void LoadPrizeType()
        {
            using (var context = new bodb_luckyEntities())
            {
                var listData = (from prizeType in context.PrizeSettings
                                select new
                                {
                                    Type = prizeType.PrizeType,
                                    prizeType.Name,
                                    prizeType.Quantity,
                                    prizeType.CurrentPrize,
                                    Parent = prizeType.ParentType,
                                    prizeType.DuplicateAllow,
                                    prizeType.DuplicateFromAdHoc

                                }).ToList();
                if (listData.Any())
                {
                    __prizeList = listData.ToDictionary(s => s.Type, s => s.Name);

                    var listType = new List<string> { "None" };
                    listType.AddRange(__prizeList.Values.ToList());

                    gridView_PrizeType.DataSource = listData;
                    gridView_PrizeType.ColumnHeadersDefaultCellStyle.Font = new Font(gridView_PrizeType.Font, FontStyle.Bold);
                    gridView_PrizeType.Columns[4].Visible = false;
                    gridView_PrizeType.Columns[0].Width = 60;
                    gridView_PrizeType.Columns[1].Width = 200;
                    gridView_PrizeType.Columns[2].Width = 90;
                    gridView_PrizeType.Columns[3].Width = 90;

                    gridView_PrizeType.Columns[5].Width = 100;
                    gridView_PrizeType.Columns[6].Width = 150;
                }
                else
                {
                    gridView_PrizeType.DataSource = null;
                }
            }
        }

        private void btn_AddPrizeType_Click(object sender, EventArgs e)
        {
            var prizeType = new PrizeSettings
            {
                PrizeType = 0,
                Name = "",
                Quantity = 0,
                CurrentPrize = 0,
                XAdHoc = true
            };
            __FormEdit(prizeType);
        }

        private void __FormEdit(PrizeSettings prizeType)
        {
            group_Information.Enabled = true;

            __prizeType = prizeType;

            btn_Delete.Enabled = false;

            if (prizeType.PrizeType > 0)
            {
                if (prizeType.PrizeType > 4) btn_Delete.Enabled = true;
            }
            txt_PrizeName.Text = prizeType.Name;
            txt_PrizeQuantity.Text = prizeType.Quantity.ToString(CultureInfo.InvariantCulture);

            checkBoxDuplicateAllow.Checked = prizeType.DuplicateAllow;
            checkBoxDuplicateFromAdHoc.Checked = prizeType.DuplicateFromAdHoc;
        }

        private void btn_SavePrizeType_Click(object sender, EventArgs e)
        {
            if (__ValidPrizeType())
            {
                int result = 0;
                try
                {
                    this.Enabled = false;
                    using (var context = new bodb_luckyEntities())
                    {
                        if (__prizeType.PrizeType > 0)
                        {
                            var prizeUpdate = context.PrizeSettings.SingleOrDefault(pr => pr.PrizeType == __prizeType.PrizeType);
                            if (prizeUpdate != null)
                            {
                                prizeUpdate.Name = __prizeType.Name;
                                prizeUpdate.Quantity = __prizeType.Quantity;
                                prizeUpdate.CurrentPrize = __prizeType.CurrentPrize;
                                prizeUpdate.ParentType = __prizeType.ParentType;
                                prizeUpdate.DuplicateAllow = __prizeType.DuplicateAllow;
                                prizeUpdate.DuplicateFromAdHoc = __prizeType.DuplicateFromAdHoc;
                                result = context.SaveChanges();
                            }
                        }
                        else
                        {
                            context.AddToPrizeSettings(__prizeType);
                            result = context.SaveChanges();
                        }
                    }
                }
                catch (Exception ex)
                {
                    result = -1;
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    this.Enabled = true;
                }
                if (result > 0)
                {
                    LoadPrizeType();
                    btn_Cancel_Click(null, EventArgs.Empty);
                }
            }
        }

        private bool __ValidPrizeType()
        {
            if (__prizeType == null)
            {
                MessageBox.Show("Data is not valid.");
                return false;
            }
            try
            {
                __prizeType.Name = txt_PrizeName.Text.Trim();
                __prizeType.Quantity = Convert.ToInt32(txt_PrizeQuantity.Text.Trim());
                __prizeType.CurrentPrize = 0;
                __prizeType.ParentType = 0;
                __prizeType.DuplicateAllow = checkBoxDuplicateAllow.Checked;
                __prizeType.DuplicateFromAdHoc = checkBoxDuplicateFromAdHoc.Checked;


                if (string.IsNullOrEmpty(__prizeType.Name))
                {
                    MessageBox.Show("Name of deparment can not be empty.");
                    return false;
                }
                if (__prizeType.Quantity < 0)
                {
                    MessageBox.Show("Quantity of Prize must be number");
                    return false;
                }
                if (__prizeType.CurrentPrize < 0)
                {
                    MessageBox.Show("CurrentPrize must be a number");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            group_Information.Enabled = false;

            txt_PrizeName.Text = "";
            txt_PrizeQuantity.Text = "";
            checkBoxDuplicateAllow.Checked = false;
            checkBoxDuplicateFromAdHoc.Checked = false;

            __prizeType = null;
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (__prizeType.PrizeType < 5)
            {
                MessageBox.Show("You can't delete this special prize type!");
            }
            if (MessageBox.Show("Do you want to delete?", "Confirm delete", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                if (__prizeType != null)
                {
                    int result = 0;
                    try
                    {
                        this.Enabled = false;
                        using (var context = new bodb_luckyEntities())
                        {
                            if (__prizeType.PrizeType > 0)
                            {
                                PrizeSettings dataDelete = context.PrizeSettings.SingleOrDefault(pr => pr.PrizeType == __prizeType.PrizeType);
                                if (dataDelete != null)
                                {
                                    context.PrizeSettings.DeleteObject(dataDelete);
                                    result = context.SaveChanges();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        result = -1;
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        this.Enabled = true;
                    }
                    if (result > 0)
                    {
                        LoadPrizeType();
                        btn_Cancel_Click(null, EventArgs.Empty);
                    }
                }
            }
        }
        #endregion

        #region gridview function
        private void gridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var index = e.RowIndex;
            if (index >= 0)
            {
                var curCells = gridView_PrizeType.Rows[index].Cells;
                GridViewOnchange(curCells);
            }
        }

        private void gridView_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                var cellSelected = gridView_PrizeType.SelectedCells[0];
                var curCells = gridView_PrizeType.Rows[cellSelected.RowIndex].Cells;
                GridViewOnchange(curCells);
            }
            catch { }
        }
        private void GridViewOnchange(DataGridViewCellCollection curCells)
        {
            int ID = 0;
            string Name = "";
            int Quantity = 0;
            int currentPrize = 0;
            int Parent = 0;
            try
            {
                ID = Convert.ToInt32(curCells[0].Value.ToString());
                if (ID > 0)
                {
                    Name = curCells[1].Value.ToString();
                    Quantity = (int)curCells[2].Value;
                    currentPrize = (int)curCells[3].Value;
                    Parent = (int)curCells[4].Value;
                    var prizeType = new PrizeSettings
                    {
                        PrizeType = ID,
                        Name = Name,
                        Quantity = Quantity,
                        CurrentPrize = currentPrize,
                        ParentType = Parent,
                        DuplicateAllow = (bool)curCells[5].Value,
                        DuplicateFromAdHoc = (bool)curCells[6].Value
                    };
                    __FormEdit(prizeType);
                }
                else
                {
                    btn_AddPrizeType_Click(null, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
    }
}

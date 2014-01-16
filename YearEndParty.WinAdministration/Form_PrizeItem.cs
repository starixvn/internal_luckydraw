using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using YearEndParty.Core;

namespace YearEndParty.WinAdministration
{
    public partial class Form_PrizeItem : Form
    {
        private Dictionary<int, string> __prizeType;
        private Prizes __prizeItem;

        public Form_PrizeItem()
        {
            InitializeComponent();

            __LoadPrizeList();
        }

        private void __LoadPrizeList()
        {
            var textPrize = combo_SelType.Text;
            int typeSearch = 0;
            if (__prizeType != null && __prizeType.ContainsValue(textPrize))
            {
                typeSearch = __prizeType.Where(s => s.Value == textPrize).FirstOrDefault().Key;
            }
            using (bodb_luckyEntities context = new bodb_luckyEntities())
            {
                if (__prizeType == null)
                {
                    __prizeType = (from prizeType in context.PrizeSettings select prizeType).ToList().ToDictionary(t => t.PrizeType, t => t.Name);
                }

                var listPrize = (from prize in context.Prizes
                                 from prizeType in context.PrizeSettings
                                 where prize.PrizeType == prizeType.PrizeType
                                 && (typeSearch == 0 ? (1 == 1) : (prize.PrizeType == typeSearch))
                                 select new
                                 {
                                     ID = prize.PrizeId,
                                     PrizeName = prize.Name,
                                     PrizeType = prizeType.Name,
                                     PrizeIndex = prize.PriceIndex
                                 }).ToList().Select((item, index) => new
                                {
                                    Num = (index + 1),
                                    Identify = item.ID,
                                    PrizeName = item.PrizeName,
                                    PrizeType = item.PrizeType,
                                    PrizeIndex = item.PrizeIndex,
                                }).ToList();
                if (listPrize.Count > 0)
                {
                    gridView_PrizeList.DataSource = listPrize;
                    gridView_PrizeList.Columns[1].Visible = false;
                    gridView_PrizeList.ColumnHeadersDefaultCellStyle.Font = new Font(gridView_PrizeList.Font, FontStyle.Bold);
                    gridView_PrizeList.Columns[0].Width = 74;
                    gridView_PrizeList.Columns[2].Width = 250;
                    gridView_PrizeList.Columns[3].Width = 200;
                    gridView_PrizeList.Columns[4].Width = 74;
                }
                else
                {
                    gridView_PrizeList.DataSource = null;
                }
            }

            btn_Cancel_Click(null, EventArgs.Empty);

            List<string> listType = new List<string> { "None" };
            listType.AddRange(__prizeType.Values.ToList());
            combo_PrizeType.DataSource = listType;

            var listSearch = new List<string> { "All" };
            listSearch.AddRange(__prizeType.Values.ToList());
            combo_SelType.DataSource = listSearch;
            if (!string.IsNullOrEmpty(textPrize))
            {
                combo_SelType.Text = textPrize;
            }
        }

        private void btn_AddNewPrize_Click(object sender, EventArgs e)
        {
            var prizeItem = new Prizes
            {
                PrizeId = 0,
                PrizeType = 0,
                Name = "",
                PriceIndex = 0,
                Image = new byte[0]
            };
            __FormEdit(prizeItem);
        }
        private void __FormEdit(Prizes prize)
        {
            group_PrizeInformation.Enabled = true;

            __prizeItem = prize;

            btn_DeletePrize.Enabled = false;

            combo_PrizeType.SelectedText = string.Empty;
            var txtPrizeType = "None";
            if (__prizeItem.PrizeId > 0)
            {
                btn_DeletePrize.Enabled = true;
                var selType = __prizeType.ContainsKey(__prizeItem.PrizeType) ? __prizeType[__prizeItem.PrizeType] : "None";
                txtPrizeType = selType;
            }
            if (__prizeItem.Image != null && __prizeItem.Image.Length > 0)
            {
                System.IO.MemoryStream streamImage = new System.IO.MemoryStream(__prizeItem.Image);
                streamImage.Seek(0, System.IO.SeekOrigin.Begin);
                picture_Prize.Image = Utilities.ScaleImage(Image.FromStream(streamImage), 160, 135);
            }
            else
            {
                picture_Prize.Image = null;
            }
            combo_PrizeType.Text = txtPrizeType;
            txt_PrizeName.Text = __prizeItem.Name;
            txt_PrizeIndex.Text = __prizeItem.PriceIndex.ToString();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            group_PrizeInformation.Enabled = false;

            combo_PrizeType.Text = "None";
            picture_Prize.Image = null;
            txt_PrizeName.Text = "";
            txt_PrizeIndex.Text = "";

            __prizeItem = null;
        }

        private void btn_SavePrize_Click(object sender, EventArgs e)
        {
            if (__ValidPrize())
            {
                int result = 0;
                try
                {
                    this.Enabled = false;
                    using (bodb_luckyEntities context = new bodb_luckyEntities())
                    {
                        if (__prizeItem.PrizeId > 0)
                        {
                            var dataUpdate = context.Prizes.Where(prize => prize.PrizeId == __prizeItem.PrizeId).SingleOrDefault();
                            if (dataUpdate != null)
                            {
                                dataUpdate.Name = __prizeItem.Name;
                                dataUpdate.PriceIndex = __prizeItem.PriceIndex;
                                dataUpdate.PrizeType = __prizeItem.PrizeType;
                                dataUpdate.Image = __prizeItem.Image;
                            }
                        }
                        else
                        {
                            context.Prizes.AddObject(__prizeItem);
                        }
                        result = context.SaveChanges();
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
                    __LoadPrizeList();
                }
            }
        }

        private void btn_DeletePrize_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to delete?", "Confirm delete", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                if (__prizeItem != null)
                {
                    try
                    {
                        this.Enabled = false;
                        int result = 0;
                        using (bodb_luckyEntities context = new bodb_luckyEntities())
                        {
                            var prize = context.Prizes.Where(p => p.PrizeId == __prizeItem.PrizeId).SingleOrDefault();
                            if (prize != null)
                            {
                                context.Prizes.DeleteObject(prize);
                            }
                            result = context.SaveChanges();
                        }
                        if (result > 0)
                        {
                            __LoadPrizeList();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        this.Enabled = true;
                    }
                }
            }
        }

        private bool __ValidPrize()
        {
            if (__prizeItem == null)
            {
                MessageBox.Show("Prize item is not valid");
                return false;
            }
            try
            {
                __prizeItem.PrizeType = 0;
                var txtPrizeType = combo_PrizeType.Text;
                if (txtPrizeType.Equals("None"))
                {
                    MessageBox.Show("Please select a Prize Type");
                    return false;
                }
                else
                {
                    if (__prizeType.ContainsValue(txtPrizeType))
                    {
                        __prizeItem.PrizeType = __prizeType.First(k => k.Value == txtPrizeType).Key;
                    }
                    else
                    {
                        MessageBox.Show("Prize Type is not valid");
                        return false;
                    }
                }
                __prizeItem.Name = txt_PrizeName.Text.Trim();
                __prizeItem.PriceIndex = Convert.ToInt32(txt_PrizeIndex.Text.Trim());
                if (string.IsNullOrEmpty(__prizeItem.Name))
                {
                    MessageBox.Show("Prize name can not be empty");
                    return false;
                }
                if (__prizeItem.Image.Length == 0)
                {
                    MessageBox.Show("Please select an image");
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

        private void btn_SelectFile_Click(object sender, EventArgs e)
        {
            if (openFile_Image.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var regxExt = new Regex("jpg|jpeg|gif|png", RegexOptions.IgnoreCase);
                    var ext = System.IO.Path.GetExtension(openFile_Image.FileName);
                    if (regxExt.IsMatch(ext))
                    {
                        System.IO.FileStream stream = System.IO.File.Open(openFile_Image.FileName, System.IO.FileMode.Open);
                        var maxInM = ((stream.Length / 1024) / 1000);
                        if (maxInM <= 2)
                        {
                            byte[] buffer = new byte[stream.Length];
                            stream.Read(buffer, 0, buffer.Length);
                            System.IO.MemoryStream memStream = new System.IO.MemoryStream(buffer);
                            stream.Flush();
                            stream.Close();

                            memStream.Seek(0, System.IO.SeekOrigin.Begin);
                            picture_Prize.Image = Utilities.ScaleImage(Image.FromStream(memStream), 160, 135);
                            __prizeItem.Image = memStream.ToArray();
                        }
                        else
                        {
                            MessageBox.Show("File image is invalid. Allow file image (max in 2M)");
                        }
                    }
                    else
                    {
                        MessageBox.Show("File image is invalid. Allow file image (jpg,jpeg,gif,png)");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        #region gridview function
        private void gridView_PrizeList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var index = e.RowIndex;
            if (index >= 0)
            {
                var curCells = gridView_PrizeList.Rows[index].Cells;
                GridViewOnchange(curCells);
            }
        }
        private void gridView_PrizeList_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                var cellSelected = gridView_PrizeList.SelectedCells[0];
                var curCells = gridView_PrizeList.Rows[cellSelected.RowIndex].Cells;
                GridViewOnchange(curCells);
            }
            catch { }
        }

        private void GridViewOnchange(DataGridViewCellCollection curCells)
        {
            int ID = 0;
            try
            {
                ID = Convert.ToInt32(curCells[1].Value.ToString());
                if (ID > 0 && (__prizeItem == null || __prizeItem.PrizeId != ID))
                {
                    this.Enabled = false;
                    Prizes prize = null;
                    using (bodb_luckyEntities context = new bodb_luckyEntities())
                    {
                        prize = context.Prizes.Where(p => p.PrizeId == ID).SingleOrDefault();
                    }
                    if (prize != null)
                    {
                        __FormEdit(prize);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Enabled = true;
            }
        }
        #endregion

        private void btn_Back_Click(object sender, EventArgs e)
        {
            combo_SelType.Text = "All";
            __LoadPrizeList();
        }

        private void btn_Show_Click(object sender, EventArgs e)
        {
            var textPrize = combo_SelType.Text.Trim();
            try
            {
                int typeSearch = 0;
                if (__prizeType.ContainsValue(textPrize))
                {
                    typeSearch = __prizeType.Where(s => s.Value == textPrize).FirstOrDefault().Key;
                }
                this.Enabled = false;
                var listPrize = __GetData(typeSearch);
                if (listPrize.Count > 0)
                {
                    gridView_PrizeList.DataSource = listPrize;
                }
                else
                {
                    gridView_PrizeList.DataSource = null;
                }
                btn_Cancel_Click(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Enabled = true;
            }
        }

        private dynamic __GetData(int typeSearch)
        {
            dynamic listPrize;
            using (bodb_luckyEntities context = new bodb_luckyEntities())
            {
                listPrize = (from prize in context.Prizes
                             from prizeType in context.PrizeSettings
                             where prize.PrizeType == prizeType.PrizeType
                             && (typeSearch == 0 ? (1 == 1) : (prize.PrizeType == typeSearch))
                             select new
                             {
                                 ID = prize.PrizeId,
                                 PrizeName = prize.Name,
                                 PrizeType = prizeType.Name,
                                 PrizeIndex = prize.PriceIndex
                             }).ToList().Select((item, index) => new
                                 {
                                     Num = (index + 1),
                                     Identify = item.ID,
                                     PrizeName = item.PrizeName,
                                     PrizeType = item.PrizeType,
                                     PrizeIndex = item.PrizeIndex,
                                 }).ToList();
            }
            return listPrize;
        }
    }
}

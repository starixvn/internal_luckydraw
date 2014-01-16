using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using YearEndParty.Core;
using log4net;

namespace YearEndParty.WinAdministration
{
    public partial class Form_Staff : Form
    {
        private Staffs __staffInfo;
        private Dictionary<int, string> __departments;
        private int __currentPage = 1;
        private static readonly ILog Log = LogManager.GetLogger("Admin2012");
        public Form_Staff()
        {
            InitializeComponent();

            __LoadStaffList(__currentPage);

        }

        #region click function
        private void __LoadStaffList(int page)
        {
            try
            {
                __currentPage = page;
                const int counter = 50;
                Enabled = false;
                var offset = (page - 1) * counter;

                var textCode = txt_Search.Text.Trim();
                var textDept = combo_DeptSearch.Text;
                int deptSearch = 0;
                if (__departments != null && __departments.ContainsValue(textDept))
                {
                    deptSearch = __departments.FirstOrDefault(s => s.Value == textDept).Key;
                }

                dynamic listStaff;
                using (var context = new bodb_luckyEntities())
                {
                    if (__departments == null)
                    {
                        __departments = (from dept in context.Departments select dept).ToList().ToDictionary(d => d.DepartmentId, d => d.Name);
                    }

                    listStaff = (from staff in context.Staffs
                                 from dept in context.Departments
                                 where staff.DepartmentId == dept.DepartmentId
                                 && (string.IsNullOrEmpty(textCode) || staff.StaffCode.Contains(textCode))
                                 && (deptSearch == 0 || (staff.DepartmentId == deptSearch))
                                 select new
                                 {
                                     ID = staff.StaffId,
                                     staff.StaffCode,
                                     staff.FullName,
                                     Department = dept.Name,
                                     NickName = staff.Nick
                                 }).OrderByDescending(r => r.ID).Skip(offset).Take(counter).ToList().Select((item, index) => new
                                 {
                                     Num = ((index + 1) + offset),
                                     Identify = item.ID,
                                     item.StaffCode,
                                     item.FullName,
                                     item.Department,
                                     item.NickName
                                 }).ToList();


                    if (listStaff.Count > 0)
                    {
                        gridView_Staff.DataSource = listStaff;
                        gridView_Staff.ColumnHeadersDefaultCellStyle.Font = new Font(gridView_Staff.Font, FontStyle.Bold);
                        gridView_Staff.Columns[1].Visible = false;
                        gridView_Staff.Columns[0].Width = 74;
                        gridView_Staff.Columns[2].Width = 100;
                        gridView_Staff.Columns[3].Width = 200;
                        gridView_Staff.Columns[4].Width = 150;
                        gridView_Staff.Columns[5].Width = 100;
                    }
                    else
                    {
                        gridView_Staff.DataSource = null;
                    }
                }

                btn_Cancel_Click(null, EventArgs.Empty);

                // set pagging
                btn_Next.Enabled = false;
                btn_Previous.Enabled = false;
                if (listStaff.Count >= counter)
                {
                    btn_Next.Enabled = true;
                }
                if (__currentPage != 1)
                {
                    btn_Previous.Enabled = true;
                }
                combo_Department.DataSource = __departments.Values.ToArray();
                cbb_Department.DataSource = __departments.Values.ToArray();

                var arrSearch = new List<string> { "All" };
                arrSearch.AddRange(__departments.Values.ToList());
                combo_DeptSearch.DataSource = arrSearch;
                if (!string.IsNullOrEmpty(textDept))
                {
                    combo_DeptSearch.Text = textDept;
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

        private void btn_AddNewStaff_Click(object sender, EventArgs e)
        {
            var staff = new Staffs
            {
                StaffId = 0,
                StaffCode = "",
                DepartmentId = 0,
                FirstName = "",
                LastName = "",
                Nick = "",
                Avatar = new byte[0]
            };
            __FormEdit(staff);
        }

        private void __FormEdit(Staffs staff)
        {
            group_StaffInformation.Enabled = true;

            __staffInfo = staff;

            btn_Delete.Enabled = false;

            combo_Department.SelectedText = string.Empty;
            var txtDept = "Others";
            if (__staffInfo.StaffId > 0)
            {
                btn_Delete.Enabled = true;
                var deptID = __staffInfo.DepartmentId == null ? 0 : (int)__staffInfo.DepartmentId;
                txtDept = __departments.ContainsKey(deptID) ? __departments[deptID] : txtDept;
            }
            if (__staffInfo.Avatar != null && __staffInfo.Avatar.Length > 0)
            {
                var streamImage = new System.IO.MemoryStream(__staffInfo.Avatar);
                streamImage.Seek(0, System.IO.SeekOrigin.Begin);
                picture_Avatar.Image = Utilities.ScaleImage(Image.FromStream(streamImage), 120, 130);
            }
            else
            {
                picture_Avatar.Image = null;
            }
            combo_Department.Text = txtDept;
            txt_StaffCode.Text = __staffInfo.StaffCode;
            txt_FirstName.Text = __staffInfo.FirstName;
            txt_LastName.Text = __staffInfo.LastName;
            txt_NickName.Text = __staffInfo.Nick;
        }

        private void btn_SelectImage_Click(object sender, EventArgs e)
        {
            if (openFile_Avatar.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var regxExt = new Regex("jpg|jpeg|gif|png", RegexOptions.IgnoreCase);
                    var ext = System.IO.Path.GetExtension(openFile_Avatar.FileName);
                    if (regxExt.IsMatch(ext))
                    {
                        var stream = System.IO.File.Open(openFile_Avatar.FileName, System.IO.FileMode.Open);
                        var maxInM = ((stream.Length / 1024) / 1000);
                        if (maxInM <= 2)
                        {
                            var buffer = new byte[stream.Length];
                            stream.Read(buffer, 0, buffer.Length);
                            var memStream = new System.IO.MemoryStream(buffer);
                            stream.Flush();
                            stream.Close();

                            memStream.Seek(0, System.IO.SeekOrigin.Begin);
                            picture_Avatar.Image = Utilities.ScaleImage(Image.FromStream(memStream), 120, 130);
                            __staffInfo.Avatar = buffer;
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

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            group_StaffInformation.Enabled = false;

            picture_Avatar.Image = null;
            combo_Department.Text = "Others";
            txt_Department.Text = "";
            txt_StaffCode.Text = "";
            txt_FirstName.Text = "";
            txt_LastName.Text = "";
            txt_NickName.Text = "";

            __staffInfo = null;
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (__ValidStaff())
            {
                try
                {
                    Enabled = false;
                    var result = 0;
                    using (var context = new bodb_luckyEntities())
                    {
                        if (__staffInfo.StaffId > 0)
                        {
                            var dataUpdate = context.Staffs.SingleOrDefault(s => s.StaffId == __staffInfo.StaffId);
                            if (dataUpdate != null)
                            {
                                dataUpdate.StaffCode = __staffInfo.StaffCode;
                                dataUpdate.DepartmentId = __staffInfo.DepartmentId;
                                dataUpdate.FirstName = __staffInfo.FirstName;
                                dataUpdate.LastName = __staffInfo.LastName;
                                dataUpdate.Nick = __staffInfo.Nick;
                                dataUpdate.Avatar = __staffInfo.Avatar;
                            }
                        }
                        else
                        {
                            context.Staffs.AddObject(__staffInfo);
                        }
                        result = context.SaveChanges();
                    }
                    if (result > 0)
                    {
                        __LoadStaffList(__currentPage);

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

        private bool __ValidStaff()
        {
            if (__staffInfo == null)
            {
                MessageBox.Show("Staff information is not valid");
                return false;
            }
            try
            {
                __staffInfo.DepartmentId = 1;
                var txtDept = combo_Department.Text;
                if (txtDept.Equals("Others") && string.IsNullOrEmpty(txt_Department.Text.Trim()))
                {
                    MessageBox.Show("Please select a department");
                    return false;
                }
                else
                {
                    if (__departments.ContainsValue(txtDept))
                    {
                        __staffInfo.DepartmentId = __departments.First(k => k.Value == txtDept).Key;
                    }
                    else
                    {
                        MessageBox.Show("Department is not valid");
                        return false;
                    }
                }
                __staffInfo.StaffCode = txt_StaffCode.Text.Trim();
                __staffInfo.FirstName = txt_FirstName.Text.Trim();
                __staffInfo.LastName = txt_LastName.Text.Trim();
                __staffInfo.Nick = txt_NickName.Text.Trim();
                if (string.IsNullOrEmpty(__staffInfo.StaffCode))
                {
                    MessageBox.Show("Staff code can not be empty");
                    return false;
                }
                if (string.IsNullOrEmpty(__staffInfo.FirstName))
                {
                    MessageBox.Show("First Name can not be empty");
                    return false;
                }
                if (string.IsNullOrEmpty(__staffInfo.LastName))
                {
                    MessageBox.Show("Last Name can not be empty");
                    return false;
                }
                if (string.IsNullOrEmpty(__staffInfo.Nick))
                {
                    MessageBox.Show("Nick Name can not be empty");
                    return false;
                }
                //if (__staffInfo.Avatar.Length == 0)
                //{
                //    MessageBox.Show("Please select an image");
                //    return false;
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to delete?", "Confirm delete", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                if (__staffInfo != null)
                {
                    try
                    {
                        Enabled = false;
                        int result = 0;
                        using (var context = new bodb_luckyEntities())
                        {
                            var staff = context.Staffs.Where(s => s.StaffId == __staffInfo.StaffId).SingleOrDefault();
                            if (staff != null)
                            {
                                context.Staffs.DeleteObject(staff);
                            }
                            result = context.SaveChanges();
                        }
                        if (result > 0)
                        {
                            __LoadStaffList(__currentPage);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        Enabled = true;
                    }
                }
            }
        }

        private void combo_Department_SelectedIndexChanged(object sender, EventArgs e)
        {
            group_OtherCompany.Visible = false;
            if (combo_Department.Text.Equals("Others"))
            {
                group_OtherCompany.Visible = true;
            }
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            var txtSearch = txt_Search.Text.Trim();
            var deptId = 0;
            var deptText = combo_DeptSearch.Text.Trim();

            if (!deptText.ToLower().Equals("all"))
            {
                deptId = __departments.First(k => k.Value == deptText).Key;
            }
            try
            {
                btn_Cancel_Click(null, EventArgs.Empty);
                Enabled = false;

                using (var context = new bodb_luckyEntities())
                {
                    var dataSearch = (from staff in context.Staffs
                                      from dept in context.Departments
                                      where staff.DepartmentId == dept.DepartmentId
                                          && (string.IsNullOrEmpty(txtSearch) || (staff.StaffCode.Contains(txtSearch)))
                                          && (deptId == 0 || (staff.DepartmentId == deptId))
                                      select new
                                      {
                                          ID = staff.StaffId,
                                          staff.StaffCode,
                                          staff.FullName,
                                          Department = dept.Name,
                                          NickName = staff.Nick
                                      }).OrderByDescending(s => s.ID).ToList().Select((item, index) => new
                                      {
                                          Num = (index + 1),
                                          Identify = item.ID,
                                          item.StaffCode,
                                          item.FullName,
                                          item.Department,
                                          item.NickName
                                      }).ToList();

                    gridView_Staff.DataSource = dataSearch.Any() ? dataSearch : null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Enabled = true;
            }
        }


        private void btn_Previous_Click(object sender, EventArgs e)
        {
            if (__currentPage > 1)
            {
                __currentPage = __currentPage - 1;
                __LoadStaffList(__currentPage);
            }
        }

        private void btn_Next_Click(object sender, EventArgs e)
        {
            __currentPage = __currentPage + 1;
            __LoadStaffList(__currentPage);
        }

        private void btn_Back_Click(object sender, EventArgs e)
        {
            txt_Search.Text = "";
            combo_DeptSearch.Text = "All";
            __LoadStaffList(1);
        }
        #endregion

        #region gridview function
        private void gridView_Staff_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var index = e.RowIndex;
            if (index >= 0)
            {
                var curCells = gridView_Staff.Rows[index].Cells;
                GridViewOnchange(curCells);
            }
        }
        private void gridView_Staff_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                var cellSelected = gridView_Staff.SelectedCells[0];
                var curCells = gridView_Staff.Rows[cellSelected.RowIndex].Cells;
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
                if (ID > 0 && (__staffInfo == null || __staffInfo.StaffId != ID))
                {
                    Enabled = false;
                    Staffs staff = null;
                    using (var context = new bodb_luckyEntities())
                    {
                        staff = context.Staffs.Where(p => p.StaffId == ID).SingleOrDefault();
                    }
                    if (staff != null)
                    {
                        __FormEdit(staff);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Enabled = true;
            }
        }
        #endregion

        #region import excel

        private int __InsertStaff(IEnumerable<Staffs> staffs)
        {
            int result = 0;
            var context = new bodb_luckyEntities();

            try
            {
                foreach (var staff in staffs)
                {
                    context.Staffs.AddObject(staff);
                }
                result = context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                context.Connection.Close();
                context.Dispose();
            }
            return result;
        }

        #endregion

        #region Import images
        private void btn_ImportImages_Click(object sender, EventArgs e)
        {
            if (openFile_Excel.ShowDialog() == DialogResult.OK)
            {
                var list_images = openFile_Excel.FileNames;
                if (list_images.Length > 0)
                {
                    var departmentID = 4;

                    if (__departments.ContainsValue(cbb_Department.SelectedValue.ToString()))
                    {
                        departmentID = __departments.First(k => k.Value == cbb_Department.SelectedValue.ToString()).Key;
                    }

                    using (var context = new bodb_luckyEntities())
                    {
                        try
                        {
                            foreach (var pathname in list_images)
                            {
                                try
                                {
                                    var arr_temp = pathname.Split('\\');
                                    var staffID = arr_temp[arr_temp.Length - 1].Split('.')[0];

                                    if (arr_temp.Length > 0)
                                    {
                                        using (var stream = System.IO.File.Open(pathname, System.IO.FileMode.Open))
                                        {

                                            var maxInM = ((stream.Length / 1024) / 1000);
                                            if (maxInM <= 2)
                                            {
                                                var buffer = new byte[stream.Length];
                                                stream.Read(buffer, 0, buffer.Length);

                                                var staff = context.Staffs.SingleOrDefault(s => (s.BarCode == staffID && s.DepartmentId == departmentID));
                                                if (staff != null)
                                                {
                                                    staff.Avatar = buffer;
                                                    //if only change 1 image: show this image else none
                                                    if (list_images.Length == 1)
                                                    {
                                                        using (var memStream = new System.IO.MemoryStream(buffer))
                                                        {
                                                            memStream.Seek(0, System.IO.SeekOrigin.Begin);
                                                            picture_Avatar.Image = Utilities.ScaleImage(Image.FromStream(memStream), 120, 130);

                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                Log.Info(staffID + ": File image is invalid. Allow file image (max in 2M)");
                                                //MessageBox.Show("File image is invalid. Allow file image (max in 2M)");
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Log.Info(ex.Message);
                                }
                            }

                            //save to database
                            context.SaveChanges();
                            MessageBox.Show("Saved Success!");
                        }
                        catch (Exception ex)
                        {
                            Log.Info(ex.Message);
                        }
                    }
                }
            }
        }
    #endregion
    }
}

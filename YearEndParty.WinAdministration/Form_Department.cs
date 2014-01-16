using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using YearEndParty.Core;

namespace YearEndParty.WinAdministration
{
    public partial class Form_Department : Form
    {
        private Departments __department;
        public Form_Department()
        {
            InitializeComponent();
        }

        private void FormDepartment_Load(object sender, EventArgs e)
        {
            LoadDepartment();
        }

        public void LoadDepartment()
        {
            using (var context = new bodb_luckyEntities())
            {
                var listData = (from department in context.Departments
                                select new
                                {
                                    ID = department.DepartmentId,
                                    Name = department.Name,
                                    ShortName = department.ShortName
                                }).ToList().Select((item, index) => new
                                    {
                                        Num = (index + 1),
                                        DepartmentID = item.ID,
                                        Name = item.Name,
                                        ShortName = item.ShortName
                                    }).ToList();
                if (listData.Count > 0)
                {
                    gridView_Department.DataSource = listData;
                    gridView_Department.Columns[1].Visible = false;
                    gridView_Department.ColumnHeadersDefaultCellStyle.Font = new Font(gridView_Department.Font, FontStyle.Bold);
                    gridView_Department.Columns[2].Width = 150;
                    gridView_Department.Columns[3].Width = 200;
                }
            }
        }

        private void gridView_Department_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var index = e.RowIndex;
            if (index >= 0)
            {
                var curCells = gridView_Department.Rows[index].Cells;
                GridViewOnchange(curCells);
            }
        }

        private void gridView_Department_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                var cellSelected = gridView_Department.SelectedCells[0];
                var curCells = gridView_Department.Rows[cellSelected.RowIndex].Cells;
                GridViewOnchange(curCells);
            }
            catch { }
        }

        private void GridViewOnchange(DataGridViewCellCollection curCells)
        {
            int ID = 0;
            string Name = "";
            string Desc = "";
            try
            {
                ID = Convert.ToInt32(curCells[1].Value.ToString());
                Name = curCells[2].Value.ToString();
                Desc = curCells[3].Value.ToString();
            }
            catch { /**/ }
            Departments department = new Departments
            {
                DepartmentId = ID,
                Name = Name,
                ShortName = Desc
            };
            __FormEdit(department);
        }

        private void __FormEdit(Departments department)
        {
            group_Information.Enabled = true;
            __department = department;

            btn_Delete.Enabled = false;
            if (__department.DepartmentId > 0)
            {
                btn_Delete.Enabled = true;
            }
            txt_NameDeparment.Text = department.Name;
            txt_DescDepartment.Text = department.ShortName;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            group_Information.Enabled = false;
            txt_NameDeparment.Text = "";
            txt_DescDepartment.Text = "";

            __department = null;
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to delete?", "Confirm delete", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                if (__department != null)
                {
                    int result = 0;
                    try
                    {
                        this.Enabled = false;
                        using (bodb_luckyEntities context = new bodb_luckyEntities())
                        {
                            if (__department.DepartmentId > 0)
                            {
                                Departments dataDelete = context.Departments.Where(de => de.DepartmentId == __department.DepartmentId).SingleOrDefault();
                                if (dataDelete != null)
                                {
                                    context.Departments.DeleteObject(dataDelete);
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
                        LoadDepartment();
                        btn_Cancel_Click(null, EventArgs.Empty);
                    }
                }
            }
        }

        private void btn_SaveDepartment_Click(object sender, EventArgs e)
        {
            if (__ValidDepartment())
            {
                int result = 0;
                try
                {
                    this.Enabled = false;
                    using (bodb_luckyEntities context = new bodb_luckyEntities())
                    {
                        if (__department.DepartmentId > 0)
                        {
                            Departments dataUpdate = context.Departments.Where(de => de.DepartmentId == __department.DepartmentId).SingleOrDefault();
                            dataUpdate.Name = __department.Name;
                            dataUpdate.ShortName = __department.ShortName;
                        }
                        else
                        {
                            context.AddToDepartments(__department);
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
                    LoadDepartment();
                    btn_Cancel_Click(null, EventArgs.Empty);
                }
            }
        }

        private bool __ValidDepartment()
        {
            if (__department == null)
            {
                MessageBox.Show("Data is not valid.");
                return false;
            }
            __department.Name = txt_NameDeparment.Text.Trim();
            __department.ShortName = txt_DescDepartment.Text.Trim();
            if (string.IsNullOrEmpty(__department.Name))
            {
                MessageBox.Show("Name of deparment can not be empty.");
                return false;
            }
            return true;
        }

        private void btn_AddDepartment_Click(object sender, EventArgs e)
        {
            Departments department = new Departments
            {
                DepartmentId = 0,
                Name = "",
                ShortName = ""
            };
            __FormEdit(department);
        }
    }
}

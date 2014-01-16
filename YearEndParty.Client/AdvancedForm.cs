using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YearEndParty.Core;
using System.Configuration;

namespace YearEndParty.Client
{
    public partial class AdvancedForm : Form
    {
        private bodb_luckyEntities _context = new bodb_luckyEntities();
        private string _barcode = string.Empty;

        public delegate void SendBarcodeDelegate(string barcode, bool isScanner = false);
        public SendBarcodeDelegate SendBarcode;

        public AdvancedForm()
        {
            InitializeComponent();
        }

        private void AdvancedForm_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchStaff();
        }

        private void input_Enter(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                SearchStaff();
            }
        }

        private void SearchStaff()
        {
            string name = txtFullname.Text.Trim().ToLower();
            string nick = txtNickname.Text.Trim().ToLower();
            string barcode = txtBarcode.Text.Trim();

            if (name.Length > 0 || nick.Length > 0 || barcode.Length > 0)
            {
                var staffList = _context.Staffs
                    .Where(x => x.FullName.ToLower().Contains(name) || x.UnsignName.Contains(name))
                    .Where(x => x.Nick.ToLower().Contains(nick))
                    .Where(x => x.StaffCode.Contains(barcode)).Select(x => new
                    {
                        Fullname = x.FullName,
                        Nick = x.Nick,
                        StaffCode = x.StaffCode
                    });

                if (staffList.Count() > 0)
                {
                    gvStaff.DataSource = staffList;
                    ShowStaff(staffList.First().StaffCode);
                }
                else
                {
                    EmptyGrid();
                    EmptyStaffAvatar();
                    MessageBox.Show("No staff found!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void gvStaff_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var staffCode = gvStaff.Rows[e.RowIndex].Cells["Barcode"].Value.ToString();
            ShowStaff(staffCode);
        }

        private void ShowStaff(string staffCode)
        {
            var staff = _context.Staffs.Where(x => x.StaffCode == staffCode).FirstOrDefault();

            if (staff != null)
            {
                lblFullnameDetails.Text = staff.FullName;
                lblNickDetails.Text = staff.Nick;
                lblDepartmentDetails.Text = _context.Departments.Where(x => x.DepartmentId == staff.DepartmentId).First().Name;
                imgStaff.Image = Utilities.ScaleImage(staff.Avatar, 143, 167);
                btnSubmit.Enabled = true;

                _barcode = staff.StaffCode;
            }
            else
            {
                MessageBox.Show("Staff does not exist!", "Error", MessageBoxButtons.OK);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            EmptyFilter();
            EmptyStaffAvatar();
            EmptyGrid();
            btnSubmit.Enabled = false;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            SendBarcode(_barcode);

            if (Convert.ToBoolean(ConfigurationManager.AppSettings["ExistOnSubmit"]))
            {
                CloseForm();
            }
        }

        #region helpers
        
        private void EmptyStaffAvatar()
        {
            imgStaff.Image = null;
            lblFullnameDetails.Text = string.Empty;
            lblNickDetails.Text = string.Empty;
            lblDepartmentDetails.Text = string.Empty;
        }

        private void EmptyFilter()
        {
            txtFullname.Text = string.Empty;
            txtNickname.Text = string.Empty;
            txtBarcode.Text = string.Empty;
        }

        private void EmptyGrid()
        {
            gvStaff.DataSource = _context.Staffs.Take(0).Select(x => new
            {
                Fullname = x.FullName,
                Nick = x.Nick,
                StaffCode = x.StaffCode
            });

            btnSubmit.Enabled = false;
        }

        private void CloseForm()
        {
            Close();
        }

        #endregion
    }
}
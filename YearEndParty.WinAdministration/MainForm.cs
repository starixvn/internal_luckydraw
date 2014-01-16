using System;
using System.Drawing;
using System.Windows.Forms;

namespace YearEndParty.WinAdministration
{
    public partial class MainForm : Form
    {
        #region public form
        public static Form_Department formDepartment;
        public static Form_PrizeType formPrizeType;
        public static Form_PrizeItem formPrizeItem;
        public static Form_Staff formStaff;
        public static Form_Winner formWinner;

        private static FormChild __activeChild = FormChild.None;
        private static Color __colorNormal = Color.AliceBlue;
        private static Color __colorActive = Color.LightPink;
        #endregion


        public MainForm()
        {
            InitializeComponent();
            //__BindButtonEvent();

        }

        #region linkLabel
        private void linkDepartment_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            __ValidFormOpened(FormChild.Department);
            if (formDepartment == null)
            {
                formDepartment = new Form_Department();
                formDepartment.MdiParent = this;
                splitContainer1.Panel2.Controls.Add(formDepartment);

            }
            btn_Department.BackColor = Color.SteelBlue;
            __activeChild = FormChild.Department;
            formDepartment.Show();

        }

        private void linkPrizeType_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            __ValidFormOpened(FormChild.PrizeSetting);
            if (formPrizeType == null)
            {
                formPrizeType = new Form_PrizeType();
                formPrizeType.MdiParent = this;
                splitContainer1.Panel2.Controls.Add(formPrizeType);
            }
            __activeChild = FormChild.PrizeSetting;
            formPrizeType.Show();
        }

        private void linkPrizeItem_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            __ValidFormOpened(FormChild.Prize);
            if (formPrizeItem == null)
            {
                formPrizeItem = new Form_PrizeItem();
                formPrizeItem.MdiParent = this;
                splitContainer1.Panel2.Controls.Add(formPrizeItem);
            }
            __activeChild = FormChild.Prize;
            formPrizeItem.Show();
        }

        private void linkStaff_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            __ValidFormOpened(FormChild.Staff);
            if (formStaff == null)
            {
                formStaff = new Form_Staff();
                formStaff.MdiParent = this;
                splitContainer1.Panel2.Controls.Add(formStaff);
            }
            __activeChild = FormChild.Staff;
            formStaff.Show();
        }

        private void linkWinner_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            __ValidFormOpened(FormChild.Winner);
            if (formWinner == null)
            {
                formWinner = new Form_Winner();
                formWinner.MdiParent = this;
                splitContainer1.Panel2.Controls.Add(formWinner);
            }
            __activeChild = FormChild.Winner;
            formWinner.Show();
        }


        private void __ValidFormOpened(FormChild formChild)
        {
            if (__activeChild != formChild)
            {
                CloseForm(__activeChild);
            }
        }
        #endregion


        public void CloseForm(FormChild formChild)
        {
            switch (formChild)
            {
                case FormChild.Department:
                    formDepartment.Close();
                    formDepartment = null;
                    btn_Department.BackColor = __colorNormal;
                    break;
                case FormChild.PrizeSetting:
                    formPrizeType.Close();
                    formPrizeType = null;
                    btn_PrizeType.BackColor = __colorNormal;
                    break;
                case FormChild.Prize:
                    formPrizeItem.Close();
                    formPrizeItem = null;
                    btn_PrizeItem.BackColor = __colorNormal;
                    break;
                case FormChild.Staff:
                    formStaff.Close();
                    formStaff = null;
                    btn_Staff.BackColor = __colorNormal;
                    break;
                case FormChild.Winner:
                    formWinner.Close();
                    formWinner = null;
                    btn_Winner.BackColor = __colorNormal;
                    break;
                default:
                    break;

            }
            __activeChild = FormChild.None;
        }



        #region left menu click
        private void btn_Department_Click(object sender, EventArgs e)
        {
            __ValidFormOpened(FormChild.Department);
            if (formDepartment == null)
            {
                formDepartment = new Form_Department();
                formDepartment.MdiParent = this;
                splitContainer1.Panel2.Controls.Add(formDepartment);

            }
            __activeChild = FormChild.Department;
            formDepartment.Show();
            btn_Department.BackColor = __colorActive;
        }

        private void btn_PrizeType_Click(object sender, EventArgs e)
        {
            __ValidFormOpened(FormChild.PrizeSetting);
            if (formPrizeType == null)
            {
                formPrizeType = new Form_PrizeType();
                formPrizeType.MdiParent = this;
                splitContainer1.Panel2.Controls.Add(formPrizeType);
            }
            __activeChild = FormChild.PrizeSetting;
            formPrizeType.Show();
            btn_PrizeType.BackColor = __colorActive;
        }

        private void btn_PrizeItem_Click(object sender, EventArgs e)
        {
            __ValidFormOpened(FormChild.Prize);
            if (formPrizeItem == null)
            {
                formPrizeItem = new Form_PrizeItem();
                formPrizeItem.MdiParent = this;
                splitContainer1.Panel2.Controls.Add(formPrizeItem);
            }
            __activeChild = FormChild.Prize;
            formPrizeItem.Show();
            btn_PrizeItem.BackColor = __colorActive;
        }

        private void btn_Staff_Click(object sender, EventArgs e)
        {
            __ValidFormOpened(FormChild.Staff);
            if (formStaff == null)
            {
                formStaff = new Form_Staff();
                formStaff.MdiParent = this;
                splitContainer1.Panel2.Controls.Add(formStaff);
            }
            __activeChild = FormChild.Staff;
            formStaff.Show();
            btn_Staff.BackColor = __colorActive;
        }

        private void btn_Winner_Click(object sender, EventArgs e)
        {
            __ValidFormOpened(FormChild.Winner);
            if (formWinner == null)
            {
                formWinner = new Form_Winner();
                formWinner.MdiParent = this;
                splitContainer1.Panel2.Controls.Add(formWinner);
            }
            __activeChild = FormChild.Winner;
            formWinner.Show();
            btn_Winner.BackColor = __colorActive;
        }

        private void btn_ExitMain_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion



        public enum FormChild
        {
            Department,
            PrizeSetting,
            Prize,
            Staff,
            Winner,
            Award,
            WinnerAward,
            BestDress,
            None
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Configuration;

namespace YearEndParty.Presentation.Controls
{
    public partial class WinnerTile : UserControl
    {
        public WinnerTile()
        {
            InitializeComponent();
        }

        public WinnerTile(string name, string dept)
        {
            InitializeComponent();

            //labelDept.ForeColor = "#D29A01";
            labelName.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name.ToLower());
            labelDept.Text = dept;
        }

        private void WinnerTile_Load(object sender, EventArgs e)
        {
            var winnerNameSize = int.Parse(ConfigurationManager.AppSettings["listWinner_NameSize"]);
            var deptNameSize = int.Parse(ConfigurationManager.AppSettings["listWinner_DeptSize"]);

            labelName.Font = new Font(labelName.Font.Name, winnerNameSize, labelName.Font.Style);
            labelDept.Font = new Font(labelDept.Font.Name, deptNameSize, labelDept.Font.Style);
        }
    }
}

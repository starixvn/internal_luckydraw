using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LuckyDraw
{
    /// <summary>
    /// Interaction logic for PasswordDialog.xaml
    /// </summary>
    public partial class ConfirmDialog : Window
    {
        public ConfirmDialog(string content)
        {
            InitializeComponent();
            this.txtContent.Text = content;
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void HandleKeyDown(object sender, KeyEventArgs e)
        {   
            if (e.Key == Key.Right)
            {
                this.btnNo.Focus();
            }
            else if (e.Key == Key.Left)
            {
                this.btnYes.Focus();
            }
            else
            {
                // Do nothing
            }
        }
    }
}

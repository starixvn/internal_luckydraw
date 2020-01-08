using System.Windows;

namespace LuckyDraw
{
    public partial class PasswordDialog : Window
    {
        public PasswordDialog()
        {
            InitializeComponent();
            this.txtPassword.Focus();
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
           this.DialogResult = true;
        }

        public string Answer
        {
            get { return this.txtPassword.Password; }
        }
    }
}

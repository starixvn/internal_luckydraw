using System.Windows.Forms;

namespace YearEndParty.Client
{
    public partial class SecureForm : Form
    {
        public delegate void PassBarcodeDelegate();
        public PassBarcodeDelegate PassBarcode;

        //private const string passcode = "sms*school";
        private const string passcode = "R&D777879";

        public SecureForm()
        {
            InitializeComponent();
        }

        private void buttonEnter_Click(object sender, System.EventArgs e)
        {
            UnlockAdvancedZone();
        }

        private void txtPasscode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                UnlockAdvancedZone();
            }
        }

        private void UnlockAdvancedZone()
        {
            if (txtPasscode.Text != passcode)
            {
                var result = MessageBox.Show(
                    "Wrong Password. Please try again!", "Error",
                    MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

                if (result == DialogResult.Retry)
                {
                    txtPasscode.Focus();
                    txtPasscode.SelectAll();
                }
                else
                {
                    CloseForm();
                }

                return;
            }

            PassBarcode();
            CloseForm();
        }

        private void CloseForm()
        {
            txtPasscode.Text = string.Empty;
            Close();
        }
    }
}
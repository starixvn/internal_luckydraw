using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using YearEndParty.Core;
using System.Globalization;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using log4net;

namespace YearEndParty.Client
{
    public partial class MainForm : Form
    {
        private const string Key = "lkdf7#$786{8Z6r0&*(*%^*&sd";
        private readonly int KeypressTime = Convert.ToInt32(ConfigurationManager.AppSettings["KeypressTime"]);
        private readonly string Grade = ConfigurationManager.AppSettings["Grade"];

        private string _barcode = string.Empty;
        private TcpClient _client;
        private DateTime _startTime;
        private DateTime _endTime;
        private Thread _thread;
        private static ILog _logger;
        private bodb_luckyEntities _db = new bodb_luckyEntities();

        private delegate void MessageNotify(string message);

        public MainForm()
        {
            _logger = GetLog("Client_" + Grade.Replace(",", "_"));

            InitializeComponent();
            ConnectToServer();

            _thread = new Thread(ReadMessage);
            _thread.Start();
        }

        private static ILog GetLog(string logName)
        {
            ILog log = LogManager.GetLogger(logName);
            log4net.Config.XmlConfigurator.Configure();
            return log;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            switch (Grade)
            {
                case "10":
                    lblStatus.BackColor = Color.FromArgb(254, 126, 209);
                    break;
                case "11":
                    lblStatus.BackColor = Color.FromArgb(179, 255, 176);
                    break;
                case "12":
                    lblStatus.BackColor = Color.FromArgb(155, 156, 255);
                    break;
                default:
                    lblStatus.BackColor = Color.DimGray;
                    break;
            }
            //this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            ClearStaffInfo();
            this.lblCopyright.Text = string.Format("© {0} Nexcel Solutions", DateTime.Now.Year - 1);
        }

        private void ConnectToServer()
        {
            string ip = ConfigurationManager.AppSettings["Ip"];
            int port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);

            try
            {
                _client = new TcpClient(ip, port);

                // bootup sql connection
                var staff = _db.Staffs.FirstOrDefault();

                if (staff == null)
                {
                    CloseClient();
                }
            }
            catch
            {
                var result = MessageBox.Show(
                    //string.Format("Cannot connect to server {0}:{1}", ip, port), "Error",
                    "Cannot connect to server!", "Error",
                    MessageBoxButtons.RetryCancel,
                    MessageBoxIcon.Error
                );

                if (result.ToString() == "Retry")
                {
                    ConnectToServer();
                }
                else
                {
                    CloseClient();
                }
            }
        }

        private void ReadMessage()
        {
            var stream = _client.GetStream();

            while (true)
            {
                try
                {
                    var message = new byte[4096];
                    var bytesRead = stream.Read(message, 0, 4096);
                    var encoder = new ASCIIEncoding();
                    var content = encoder.GetString(message, 0, bytesRead);

                    if (content.Length > 0)
                    {
                        ShowMessage(content);
                    }
                }
                catch (Exception ex)
                {
                    if (ex is IOException)
                    {
                        MessageBox.Show(
                            ex.Message, "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }

                    CloseClient();
                }
            }
        }

        #region send messages to server

        private void ShowMessage(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new MessageNotify(ShowMessage), message);
            }
            else
            {
                lblMessage.Text = message.Substring(0, message.IndexOf(','));

                string barcode = message.Substring(Math.Max(0, message.Length - 7));
                var staff = _db.Staffs.FirstOrDefault(i => i.StaffCode == barcode);

                if (staff != null)
                {
                    _logger.Info(message + GetMoreLogInfo(staff));
                }
                else
                {
                    _logger.Info(message);
                }
            }
        }

        public void SendMessage(string message)
        {
            if (_client.Connected)
            {
                try
                {
                    _logger.Info("scan barcode: " + _barcode);

                    var chatWriter = new StreamWriter(_client.GetStream());
                    chatWriter.WriteLine(message);
                    chatWriter.Flush(); // flush the stream
                }
                catch (Exception e)
                {
                    if (e.InnerException != null)
                    {
                        MessageBox.Show(
                            "Cannot connect to SERVER. Make sure the SERVER is running then restart this program",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        CloseClient();
                    }
                }
            }
            else
            {
                CloseClient();
            }
        }

        #endregion

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseClient();
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsNumber(e.KeyChar))
            {
                return;
            }

            if (_barcode.Length == 0)
            {
                _startTime = DateTime.Now;
            }

            _endTime = DateTime.Now;

            TimeSpan timeSpan = _endTime - _startTime;

            if (timeSpan.Milliseconds > KeypressTime)
            {
                // reset
                _barcode = string.Empty;
                _startTime = DateTime.Now;
            }

            _barcode += e.KeyChar.ToString();

            if (_barcode.Length == 8)
            {
                SendBarcode(_barcode, true);
            }
        }

        #region activation status

        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            this.Opacity = 0.6;
            //lblStatus.BackColor = Color.Crimson;
            lblStatus.Text = "I'm inactive :(";
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            this.Opacity = 1;
            //lblStatus.BackColor = Color.Green;
            lblStatus.Text = "I'm ready :)";
        }

        #endregion

        #region advanced zone

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyCode == Keys.A)
            {
                ShowSecureForm();
            }
        }

        private void btnSecure_Click(object sender, EventArgs e)
        {
            ShowSecureForm();
        }

        private void ShowSecureForm()
        {
            (new SecureForm { PassBarcode = ShowSecureZone }).ShowDialog();
        }

        private void ShowSecureZone()
        {
            (new AdvancedForm { SendBarcode = this.SendBarcode }).ShowDialog();
        }

        #endregion

        private void SendBarcode(string barcode, bool isScanner = false)
        {
            _barcode = barcode;

            if (!Grade.Contains(_barcode.Substring(0, 2)))
            {
                string errMsg = "Wrong GROUP!";

                if (isScanner)
                {
                    lblMessage.Text = errMsg;
                }
                else
                {
                    MessageBox.Show(errMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                ClearStaffInfo();

                var staffToLog = _db.Staffs.FirstOrDefault(i => i.StaffCode == barcode);

                if (staffToLog != null)
                {
                    _logger.Info("Wrong group, barcode: " + _barcode + GetMoreLogInfo(staffToLog));
                }
                else
                {
                    _logger.Info("Wrong group, barcode: " + _barcode);
                }

                return;
            }

            var staff = _db.Staffs.FirstOrDefault(i => i.StaffCode == _barcode);

            if (staff == null)
            {
                _barcode = string.Empty;
                return;
            }

            var dept = _db.Departments.First(i => i.DepartmentId == staff.DepartmentId);

            lblBarcode.Text = _barcode;
            lblFullName.Text = staff.FullName;
            lblNickName.Text = string.IsNullOrWhiteSpace(staff.Nick) ? staff.FirstName : staff.Nick;
            lblDepartment.Text = dept.Name;
            pictureBoxAvatar.Image = Utilities.ScaleImage(staff.Avatar, 286, 334);
            lblMessage.Text = string.Empty;

            SendMessage(Utilities.EncryptTripleDes(_barcode, Key));
            _barcode = string.Empty;
        }

        private void CloseClient()
        {
            if (_client != null)
            {
                _client.Close();
                _client = null;
            }

            if (_thread != null)
            {
                _thread.Abort();
            }

            Environment.Exit(0);
        }

        #region utilities

        private string GetMoreLogInfo(Staffs staff)
        {
            return string.Format(" ({0} - {1} - Dept: {2})", staff.UnsignName, staff.Nick, staff.DepartmentId);
        }

        //private void ShowErrorStatus(string msg)
        //{
        //    panelStatus.BackColor = Color.Red;
        //    lblStatus.Text = msg;
        //}

        //private void ShowInfoStatus(string msg)
        //{
        //    panelStatus.BackColor = Color.Lime;
        //    lblStatus.Text = msg;
        //}

        private void ClearStaffInfo()
        {
            lblFullName.Text = string.Empty;
            lblBarcode.Text = string.Empty;
            lblNickName.Text = string.Empty;
            lblDepartment.Text = string.Empty;
            pictureBoxAvatar.Image = null;
        }

        #endregion

    }
}
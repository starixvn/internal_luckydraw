//
//
//
//
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using log4net;
using YearEndParty.Core;

namespace YearEndParty.Presentation
{
    public partial class MainForm : Form
    {
        private const string Key = "lkdf7#$786{8Z6r0&*(*%^*&sd";

        private TcpListener _serverListener;
        private TcpClient _activeClient;
        private IList<TcpClient> _client = new List<TcpClient>();

        private readonly Thread _serverThread;
        private PrizeList _prizeList;
        private AdhocPrize _adhocPrize;
        private LuckyList _luckyList;
        private static readonly ILog Log = LogManager.GetLogger("YearEndParty2012");
        private delegate void BarcodeNotify(string barcode);

        public MainForm()
        {
            InitializeComponent();
            //Cursor.Hide();
            //labelStatus.Text = "Available prizes/total prizes: Super grand: 4/10. Grand: 18/30. Minor: 0/30. Major: 0/30. Please access http://127.0.0.7 for more details";

            _serverThread = new Thread(StartServer);
            _serverThread.Start();

            SetStyle(ControlStyles.DoubleBuffer |
              ControlStyles.UserPaint |
              ControlStyles.AllPaintingInWmPaint,
              true);
            UpdateStyles();

            log4net.Config.XmlConfigurator.Configure();

            SetThreadExecutionState(0x00000002);
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern uint SetThreadExecutionState([In] uint esFlags);

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Exit();
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            var x = System.Windows.Forms.Screen.FromControl(this);
            switch (e.KeyChar)
            { 
                case (char)49: //1
                     _prizeList = new PrizeList
                                     {
                                         PrizeType = PrizeTypes.SuperGrand,
                                         Multiply = 1,
                                         TalkBack = TalkBack
                                     };
                    _prizeList.Show();
                    break;

                case (char)50: //2
                    _prizeList = new PrizeList
                                     {
                                         PrizeType = PrizeTypes.Grand,
                                         Multiply = 3,
                                         TalkBack = TalkBack
                                     };
                    _prizeList.Show();
                    break;

                case (char)51: //3
                    _prizeList = new PrizeList
                                     {
                                         PrizeType = PrizeTypes.Major,
                                         Multiply = 3,
                                         TalkBack = TalkBack
                                     };
                    _prizeList.Show();
                    break;

                case (char)52: //4
                  
                    _prizeList = new PrizeList
                    {
                        PrizeType = PrizeTypes.Cash,
                        Multiply = 3,
                        TalkBack = TalkBack
                    };
                    _prizeList.Show();
                    break;
                //Quinn add 2012/11/12

                case (char)53: //5
                    
                    _adhocPrize = new AdhocPrize { TalkBack = TalkBack };
                    _adhocPrize.Show();
                    break;

                case (char)5: //press ctrl+E to close
                    Exit();
                    break;
                case (char)97: //a
                    _luckyList = new LuckyList { PrizeType = (int)PrizeTypes.SuperGrand };
                    _luckyList.Show();
                    break;
                case (char)98: //b
                    _luckyList = new LuckyList { PrizeType = (int)PrizeTypes.Grand };
                    _luckyList.Show();
                    break;
                case (char)99: //c
                    _luckyList = new LuckyList { PrizeType = (int)PrizeTypes.Major };
                    _luckyList.Show();
                    break;
                case (char)100: //d
                    _luckyList = new LuckyList { PrizeType = (int)PrizeTypes.Cash };
                    _luckyList.Show();
                    break;
                case (char)101: //e
                    _luckyList = new LuckyList { PrizeType = (int)PrizeTypes.AdHoc };
                    _luckyList.Show();
                    break;
                case (char)102: //f
                    _luckyList = new LuckyList { PrizeType = 7 };
                    _luckyList.Show();
                    break;
                case (char)103: //g
                    _luckyList = new LuckyList { PrizeType = 8 };
                    _luckyList.Show();
                    break;
            }
        }

        private static void Exit()
        {
            Application.Exit();
            Environment.Exit(0);
        }

        public void StartServer()
        {
            try
            {
                //Start the tcpListner
                var ip = IPAddress.Parse(ConfigurationManager.AppSettings["IP"]);
                _serverListener = new TcpListener(ip, int.Parse(ConfigurationManager.AppSettings["Port"]));
                _serverListener.Start();
                while (true)
                {
                    //blocks until a client has connected to the server
                    var client = _serverListener.AcceptTcpClient();
                    _client.Add(client);


                    //create a thread to handle communication 
                    //with connected client

                    var clientThread = new Thread(HandleClientComm);
                    clientThread.Start(client);
                }
            }
            catch(Exception ex)
            {
                if (_serverListener != null) _serverListener.Stop();
                MessageBox.Show("Can't open port for clients connect!");
                Log.Info(ex.Message);
                Application.Exit();
                Environment.Exit(0);
            }
        }

        private void HandleClientComm(object client)
        {
            var tcpClient = (TcpClient)client;
            var clientStream = tcpClient.GetStream();

            var message = new byte[4096];
            int bytesRead;

            while (true)
            {
                try
                {
                    //blocks until a client sends a message
                    bytesRead = clientStream.Read(message, 0, 4096);
                }
                catch (Exception ex)
                {
                    //a socket error has occured
                    Log.Info(ex.Message);
                    break;
                }

                if (bytesRead == 0)
                {
                    //the client has disconnected from the server
                    break;
                }

                //message has successfully been received
                var encoder = new ASCIIEncoding();
                var barcode = Utilities.DecryptTripleDes(encoder.GetString(message, 0, bytesRead), Key);
                if (barcode != null)
                {
                    barcode = barcode.Replace(Environment.NewLine, string.Empty);
                    _activeClient = tcpClient;
                    BarcodeDelivery(barcode);
                }
                //write log
                Log.Info(tcpClient.Client.LocalEndPoint + " - " + barcode);
            }

            tcpClient.Close();
        }

        private void BarcodeDelivery(string barcode)
        {
            if (InvokeRequired)
            {
                Invoke(new BarcodeNotify(BarcodeDelivery), barcode);
            }
            else
            {
                if (_prizeList != null && !_prizeList.IsClosed)
                    _prizeList.Barcode(barcode);

                if (_adhocPrize != null && !_adhocPrize.IsClosed)
                    _adhocPrize.Barcode(barcode);
            }
        }

        private void TalkBack(string message)
        {
            var clientStream = _activeClient.GetStream();
            var encoder = new ASCIIEncoding();
            var buffer = encoder.GetBytes(message);

            clientStream.Write(buffer, 0, buffer.Length);
            clientStream.Flush();
        }
        
    }
}

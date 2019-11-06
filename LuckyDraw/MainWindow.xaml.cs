using LuckyDraw.Constants;
using LuckyDraw.Model;
using LuckyDraw.Resources;
using LuckyDraw.Service;
using LuckyDraw.Utilities;
using LuckyDraw.Views;
using Microsoft.Win32;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LuckyDraw
{
    public partial class MainWindow : Window
    {
        private readonly ICommonService commonService;
        private readonly ISeedService seedService;
        private readonly IRunningService runningService;
        private readonly IValidationService validationService;
        private readonly IAuthenticationService authenticationService;
        private readonly IUtils utils;
        private bool isLoading;

        public MainWindow():this(null, null, null, null, null, null)
        {            
        }

        public MainWindow(
            ICommonService commonService,
            ISeedService seedService,
            IRunningService runningService,
            IValidationService validationService,
            IAuthenticationService authenticationService,
            IUtils utils)
        {
            this.commonService = commonService ?? new CommonService();
            this.seedService = seedService ?? new SeedService();
            this.runningService = runningService ?? new RunningService();
            this.validationService = validationService ?? new ValidationService();
            this.authenticationService = authenticationService ?? new AuthenticationService();
            this.utils = utils ?? new Utils();

            InitializeComponent();
            this.isLoading = false;
            this.seedService.GenerateSeedData();
            LoadSettings();
        }

        private void LoadSettings()
        {
            this.prizeDataGrid.ItemsSource = commonService.GetPrizeList();
            this.rangeDataGrid.ItemsSource = commonService.GetSettingList();
            this.ignoredListDataGrid.ItemsSource = commonService.GetSettingList();
        }

        public void UpdateRoundText()
        {
            this.textRound.Text = this.runningService.GetRoundName().ToUpper(CultureInfo.CurrentCulture);
        }

        public void UpdateIgnoredListText() {
            this.textIgnored.Text = string.Join(" - ", this.runningService.Setting.Excluded.Replace(" ", "").Split(','));
        }

        public void UpdateRandomRangeText() {
            this.textRandomRange.Text = String.Format("{0}-{1}",
                this.runningService.Setting.RandomFrom.ToString(),
                this.runningService.Setting.RandomTo.ToString());
        }

        private void BuildRoundUI() {
            if (this.runningService.IsValid) {
                UpdateIgnoredListText();
                UpdateRandomRangeText();
                UpdateRoundText();
                UpdateRoundUI();
            }
        }

        private void UpdateRoundUI()
        {
            var roundSize = this.runningService.CurrentList.Count;
            Type roundViewType = Type.GetType(string.Format("LuckyDraw.Views.RoundView{0}", roundSize));
            this.roundViewBox.Child = (UserControl)Activator.CreateInstance(roundViewType);            

            var roundView = (UserControl)this.roundViewBox.Child;  
            for (var i = 0; i < roundSize; i++)
            {
                var r = this.runningService.CurrentList[i];

                var item = (Item)((Viewbox)roundView.FindName("viewBoxItem" + (i + 1))).Child;

                var pLabel = (TextBlock)item.FindName("textPrize");
                pLabel.Text = r.PrizeName;

                var rLabel = (TextBlock)item.FindName("textRandom");
                rLabel.Text = r.Random == string.Empty ? "000" : r.Random;

                var imageItem = (ImageBrush)item.FindName("img");
                var image = utils.PrizeImage(r.PrizeId.ToString() + ".png", Consts.HomeImageSize);
                imageItem.ImageSource = image ?? utils.PrizeImage("prize.png", Consts.HomeImageSize);
            }
        }
       
        private string CreateRandomNumber(){
            Random rnd = new Random();
            for(var i = 0; i < Consts.MaxRandomTrials; i++)
            {
                var random = rnd.Next(this.runningService.Setting.RandomFrom, this.runningService.Setting.RandomTo + 1).ToString("000");
                if (!this.runningService.IsExistRandom(random))
                {
                    return random;
                }
            }

            return "000";
        }     

        private async Task RunRandoms() {
            var roundView = (UserControl)this.roundViewBox.Child;
            for (var i = 0; i < this.runningService.CurrentList.Count; i++) {                
                if (this.runningService.CurrentList[i].Status == 0)
                {
                    var item = (Item)((Viewbox)roundView.FindName("viewBoxItem" + (i + 1))).Child;

                    var label = (TextBlock)item.FindName("textRandom");
                    await AnimateRandom(Consts.RangeThreeDigits, string.Empty, label);
                    string random = CreateRandomNumber();
                    const int two = 2;
                    await AnimateRandom(Consts.RangeTwoDigits, random.Substring(0, 1), label);
                    await AnimateRandom(Consts.RangeOneDigit, random.Substring(0, two), label);

                    label.Text = random;

                    if (random != "000")
                    {
                        this.runningService.UpdateRandom(random, i);
                    }
                }
            }
        }
        
        private async void RunButton_Click(object sender, RoutedEventArgs e) {
            if (this.isLoading) {
                return;
            }

            MessageBoxResult messageBoxResult = MessageBox.Show(Labels.ConfirmRun, Labels.Confirmation, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                this.isLoading = true;
                await RunRandoms();
                this.isLoading = false;
            }  
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.isLoading)
            {
                return;
            }

            if (this.runningService.MoveBack()) {
                BuildRoundUI();
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.isLoading)
            {
                return;
            }

            if (this.runningService.MoveNext()) {
                BuildRoundUI();
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (this.tabHome.IsSelected && this.runningService.LoadModel())
            {
                BuildRoundUI();
            }            

            if (this.tabResult.IsSelected) {
                this.resultDataGrid.ItemsSource = commonService.GetResultList();
            }
           
            if (this.tabLog.IsSelected)
            {
                this.logDataGrid.ItemsSource = commonService.GetLogList();
            }
        }

        [SuppressMessage("csharpsquid", "S2325:Methods and properties that don't access instance data can be static to prevent any misunderstanding about the contract of the method.")]
        private void ResultDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
        }

        [SuppressMessage("csharpsquid", "S2325:Methods and properties that don't access instance data can be static to prevent any misunderstanding about the contract of the method.")]
        private void LogDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
        }           

        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            var viewPrize = ((Button)sender).DataContext as ViewPrize;
            if(viewPrize == null || viewPrize.Id == 0)
            {
                MessageBox.Show(Labels.SaveItemAlert);
                return;
            }

            OpenFileDialog op = new OpenFileDialog { Title = Labels.SelectPicture, Filter = Labels.AllSupportedGraphics };            
            if (op.ShowDialog() == true)
            {
                if (utils.GetFileSize(op.FileName) > Consts.MaxFileSize) {
                    MessageBox.Show(Labels.FileSizeAlert, Labels.Error, MessageBoxButton.OK, MessageBoxImage.Error);                    
                    return;
                }

                var path = String.Format("{0}{1}.png", utils.BaseDirectory(), viewPrize.Id.ToString());                              
                utils.SaveFile(op.FileName, path);

                this.prizeDataGrid.Items.Refresh();                
            }
        }

        private void SavePrizesButton_Click(object sender, RoutedEventArgs e)
        {
            var prizes = this.prizeDataGrid.Items.OfType<ViewPrize>();
            var validation = validationService.ValidatePrizes(prizes);
            if (string.IsNullOrEmpty(validation))
            {
                commonService.UpdatePrize(prizes);
                this.prizeDataGrid.ItemsSource = commonService.GetPrizeList();               

                MessageBox.Show(Labels.SaveSuccess, Labels.Inform, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show(validation, Labels.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveIgnoredListButton_Click(object sender, RoutedEventArgs e)
        {
            commonService.UpdateIgnoredList(this.ignoredListDataGrid.Items.OfType<Setting>());
            this.runningService.LoadSetting();
            UpdateIgnoredListText();
            MessageBox.Show(Labels.SaveSuccess, Labels.Inform, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SaveRangeButton_Click(object sender, RoutedEventArgs e)
        {
            commonService.UpdateRange(this.rangeDataGrid.Items.OfType<Setting>());
            this.runningService.LoadSetting();
            UpdateRandomRangeText();
            MessageBox.Show(Labels.SaveSuccess, Labels.Inform, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            var startIndex = 0;
            if (int.TryParse(this.textPrizeStartIndex.Text, out startIndex) && startIndex > 0)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show(Labels.ConfirmNewGame, Labels.Confirmation, MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    commonService.UpdateStartIndex(startIndex);
                    commonService.StartNewGame();
                    this.runningService.ResetModel();
                    MessageBox.Show(Labels.InformNewGame, Labels.Inform, MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show(Labels.PositiveStartIndexAlert, Labels.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [SuppressMessage("csharpsquid", "S2325:Methods and properties that don't access instance data can be static to prevent any misunderstanding about the contract of the method.")]
        private async Task AnimateRandom(int range, string subRandom, TextBlock l)
        {
            Random rnd = new Random();
            for (var i = 0; i < Consts.AnimationLoops; i++)
            {
                int random = rnd.Next(1, range);
                await Task.Delay(1);
                l.Text = subRandom + random.ToString();
            }
        }

        private void HomeMenu_Click(object sender, RoutedEventArgs e)
        {
            if (this.isLoading)
            {
                return;
            }

            this.mainTab.SelectedIndex = 0;
        }
      
        private void SettingMenu_Click(object sender, RoutedEventArgs e)
        {
            if (this.isLoading)
            {
                return;
            }

            PasswordDialog passwordDialog = new PasswordDialog();
            var result = passwordDialog.ShowDialog();

            if (result != true) {
                return;
            }
                
            if (authenticationService.Authenticate(passwordDialog.Answer)) {
                this.mainTab.SelectedIndex = 1;
            }
            else
            {
                MessageBox.Show(Labels.WrongPassword, Labels.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }            
        }

        private void ResultMenu_Click(object sender, RoutedEventArgs e)
        {
            if (this.isLoading)
            {
                return;
            }

            const int resultTabIndex = 2;
            this.mainTab.SelectedIndex = resultTabIndex;
        }

        private void LogMenu_Click(object sender, RoutedEventArgs e)
        {
            if (this.isLoading)
            {
                return;
            }

            const int logTabIndex = 3;
            this.mainTab.SelectedIndex = logTabIndex;
        }

        private void MinimizeMenu_Click(object sender, RoutedEventArgs e)
        {
            if (this.isLoading)
            {
                return;
            }

            this.WindowState = WindowState.Minimized;
        }

        private void ExitMenu_Click(object sender, RoutedEventArgs e)
        {
            if (this.isLoading)
            {
                return;
            }

            MessageBoxResult messageBoxResult = MessageBox.Show(Labels.ConfirmExit, Labels.Confirmation, MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
    }
}

using LuckyDraw.Model;
using LuckyDraw.Resources;
using LuckyDraw.Service;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace LuckyDraw
{
    public partial class MainWindow : Window
    {
        private CommonService commonService;
        private SeedService seedService;
        private RunningService runningService;
        private bool isLoading;
        private string baseDirectory;
        private AuthenticationService authenticationService;

        public MainWindow()
        {
            InitializeComponent();

            commonService = new CommonService();
            seedService = new SeedService();
            runningService = new RunningService();
            authenticationService = new AuthenticationService();
            isLoading = false;     
            baseDirectory = String.Format("{0}\\AppData\\Images\\", AppDomain.CurrentDomain.BaseDirectory);

            seedService.GenerateSeedData();
            LoadSettings();
        }

        private void LoadSettings()
        {
            this.prizeDataGrid.ItemsSource = commonService.GetPrizeList();
            this.rangeDataGrid.ItemsSource = commonService.GetSettingList();
            this.ignoredListDataGrid.ItemsSource = commonService.GetSettingList();
        }

        private void UpdateIgnoredListText() {
            this.textIgnored.Content = String.Format("{0}: {1}", Labels.Ignored, this.runningService.Setting.Excluded);
        }

        private void UpdateRandomRangeText() {
            var randomFrom = this.runningService.Setting.RandomFrom.ToString();
            var randomTo = this.runningService.Setting.RandomTo.ToString();
            this.textRandomRange.Content = String.Format("{0}: {1} {2} {3} {4}", Labels.Random, Labels.From, randomFrom, Labels.To, randomTo );
        }

        private void BuildRoundUI() {
            if (this.runningService.IsValid) {
                UpdateIgnoredListText();
                UpdateRandomRangeText();
                this.textRound.Content = this.runningService.GetRoundName();
                for (var i = 0; i < this.runningService.CurrentList.Count; i++) {
                    var pLabel = (Label)this.FindName("textPrize" + (i + 1));
                    var rLabel = (Label)this.FindName("textRandom" + (i + 1));
                    var imageItem = (Image)this.FindName("img" + (i + 1));
                    var cItem = (Canvas)this.FindName("canvasItem" + (i + 1));
                    var r = this.runningService.CurrentList[i];
                    pLabel.Content = r.PrizeName;
                    rLabel.Content = r.Random == string.Empty ? "000" : r.Random;
                    var image = TaskImg(r.PrizeId.ToString() + ".png");
                    imageItem.Source = image?? TaskImg("prize.png");
                    cItem.Visibility = Visibility.Visible;
                }

                for (var i = this.runningService.CurrentList.Count; i < Constants.MaxRoundItems; i++) {
                    var cItem = (Canvas)this.FindName("canvasItem" + (i + 1));
                    cItem.Visibility = Visibility.Hidden;
                }
            }
        }

        private BitmapImage TaskImg(string imageName)
        {            
            var path = String.Format("{0}{1}", baseDirectory, imageName);
            BitmapImage image = null;
            if (File.Exists(path))
            {
                image = BitmapFromUri(new Uri(path));
            }

            return image;            
        }

        private BitmapImage BitmapFromUri(Uri source)
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = source;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            bitmap.DecodePixelHeight = Constants.ImageHeight;
            bitmap.DecodePixelWidth = Constants.ImageWidth;
            bitmap.EndInit();

            return bitmap;
        }


        private string CreateRandomNumber(){
            Random rnd = new Random();
            for(var i = 0; i < Constants.MaxRandomTrials; i++)
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
            for (var i = 0; i < this.runningService.CurrentList.Count; i++) {                
                if (this.runningService.CurrentList[i].Status == 0)
                {
                    var label = (Label)this.FindName("textRandom" + (i + 1));
                    await AnimateRandom(Constants.RangeThreeDigits, string.Empty, label);
                    string random = CreateRandomNumber();
                    await AnimateRandom(Constants.RangeTwoDigits, random.Substring(0, 1), label);
                    await AnimateRandom(Constants.RangeOneDigit, random.Substring(0, 2), label);

                    label.Content = random;

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

        private void ResultDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            e.Handled = true;
        }

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

            OpenFileDialog op = new OpenFileDialog();
            op.Title = Labels.SelectPicture;
            op.Filter = Labels.AllSupportedGraphics;
            if (op.ShowDialog() == true)
            {
                var fileSize = (new FileInfo(op.FileName)).Length;
                if (fileSize > Constants.MaxFileSize) {
                    MessageBox.Show(Labels.FileSizeAlert, Labels.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                    
                    return;
                }

                var image = new BitmapImage(new Uri(op.FileName));                
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));

                var path = String.Format("{0}{1}.png", baseDirectory, viewPrize.Id.ToString());               
                using (var fileStream = new System.IO.FileStream(path, System.IO.FileMode.Create))
                {
                    encoder.Save(fileStream);
                }

                this.prizeDataGrid.Items.Refresh();                
            }
        }

        private void SavePrizesButton_Click(object sender, RoutedEventArgs e)
        {
            var prizes = this.prizeDataGrid.Items.OfType<ViewPrize>();
            var validation = ValidationService.ValidatePrizes(prizes);
            if (string.IsNullOrEmpty(validation))
            {
                this.commonService.UpdatePrize(prizes);
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
            this.commonService.UpdateIgnoredList(this.ignoredListDataGrid.Items.OfType<Setting>());
            this.runningService.LoadSetting();
            UpdateIgnoredListText();
            MessageBox.Show(Labels.SaveSuccess, Labels.Inform, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SaveRangeButton_Click(object sender, RoutedEventArgs e)
        {
            this.commonService.UpdateRange(this.rangeDataGrid.Items.OfType<Setting>());
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
                    this.commonService.UpdateStartIndex(startIndex);
                    this.commonService.StartNewGame();
                    this.runningService.ResetModel();
                    MessageBox.Show(Labels.InformNewGame, Labels.Inform, MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show(Labels.PositiveStartIndexAlert, Labels.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }        

        private async Task AnimateRandom(int range, string subRandom, Label l)
        {
            Random rnd = new Random();
            for (var i = 0; i < Constants.AnimationLoops; i++)
            {
                int random = rnd.Next(1, range);
                await Task.Delay(1);
                l.Content = subRandom + random.ToString();
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

            this.mainTab.SelectedIndex = 2;
        }

        private void LogMenu_Click(object sender, RoutedEventArgs e)
        {
            if (this.isLoading)
            {
                return;
            }

            this.mainTab.SelectedIndex = 3;
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

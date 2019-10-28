using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LuckyDraw.Model;
using LuckyDraw.Repository;
using Microsoft.Win32;

namespace LuckyDraw
{   
    public partial class MainWindow : Window
    {
        private SeedModel seedModel;
        private RunningModel runningModel;
        private CommonRepository commonRepository;
        private bool isLoading;


        public MainWindow()
        {
            InitializeComponent();

            seedModel = new SeedModel();
            runningModel = new RunningModel();
            commonRepository = new CommonRepository();
            isLoading = false;

            //var directoryPath = AppDomain.CurrentDomain.BaseDirectory + @"\AppData\Images";
            //var path = directoryPath + @"\hand2.cur";
            //this.Cursor = new Cursor(path);           

            seedModel.GenerateSeedData();
            LoadSettings();
        }

        private void LoadSettings()
        {
            var viewModel = new ViewModel();
            this.prizeDataGrid.ItemsSource = null;
            this.prizeDataGrid.ItemsSource = viewModel.PrizeList;
            this.rangeDataGrid.ItemsSource = null;
            this.rangeDataGrid.ItemsSource = viewModel.SettingList;
            this.ignoredListDataGrid.ItemsSource = null;
            this.ignoredListDataGrid.ItemsSource = viewModel.SettingList;
        }

        private void UpdateIgnoredListText() {
            this.textIgnored.Content = "Ignored: " + this.runningModel.Setting.Excluded;
        }

        private void UpdateRandomRangeText() {
            this.textRandomRange.Content = "Random: from " + this.runningModel.Setting.RandomFrom.ToString() + " to " + this.runningModel.Setting.RandomTo.ToString();
        }

        private void BuildRoundUI() {
            if (this.runningModel.IsValid) {
                UpdateIgnoredListText();
                UpdateRandomRangeText();
                this.textRound.Content = this.runningModel.GetRoundName();
                for (var i = 0; i < this.runningModel.CurrentList.Count; i++) {
                    var pLabel = (Label)this.FindName("textPrize" + (i + 1));
                    var rLabel = (Label)this.FindName("textRandom" + (i + 1));
                    var imageItem = (Image)this.FindName("img" + (i + 1));
                    var cItem = (Canvas)this.FindName("canvasItem" + (i + 1));
                    var r = this.runningModel.CurrentList[i];
                    pLabel.Content = r.PrizeName;
                    rLabel.Content = r.Random == string.Empty ? "000" : r.Random;
                    var image = TaskImg(r.PrizeId.ToString() + ".png");
                    imageItem.Source = image != null ? image : TaskImg("prize.jpg");
                    cItem.Visibility = Visibility.Visible;
                }

                for (var i = this.runningModel.CurrentList.Count; i < 10; i++) {
                    var cItem = (Canvas)this.FindName("canvasItem" + (i + 1));
                    cItem.Visibility = Visibility.Hidden;
                }
            }
        }

        private BitmapImage TaskImg(string imageName)
        {            
            var path = AppDomain.CurrentDomain.BaseDirectory + @"\AppData\Images\" + imageName;
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
            bitmap.DecodePixelHeight = 50;
            bitmap.DecodePixelWidth = 50;
            bitmap.EndInit();

            return bitmap;
        }


        private string CreateRandomNumber(){
            Random rnd = new Random();
            var random = "000";
            for(var i = 0; i < 10000; i++)
            {
                var r = rnd.Next(this.runningModel.Setting.RandomFrom, this.runningModel.Setting.RandomTo + 1).ToString("000");
                if (!this.runningModel.IsExistRandom(r))
                {
                    random = r;
                    break;
                }
            }

            return random;
        }     

        private async Task RunRandoms() {
            for (var i = 0; i < this.runningModel.CurrentList.Count; i++) {                
                if (this.runningModel.CurrentList[i].Status == 0)
                {
                    var label = (Label)this.FindName("textRandom" + (i + 1));
                    await AnimateRandom(1000, string.Empty, label);
                    string random = CreateRandomNumber();
                    await AnimateRandom(100, random.Substring(0, 1), label);
                    await AnimateRandom(10, random.Substring(0, 2), label);

                    label.Content = random;

                    if (random != "000")
                    {
                        this.runningModel.UpdateRandom(random, i);
                    }
                }
            }
        }
        
        private async void RunButton_Click(object sender, RoutedEventArgs e) {
            if (this.isLoading) {
                return;
            }



            this.isLoading = true;
            await RunRandoms();
            this.isLoading = false;
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.isLoading)
            {
                return;
            }

            if (this.runningModel.MoveBack()) {
                BuildRoundUI();
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.isLoading)
            {
                return;
            }

            if (this.runningModel.MoveNext()) {
                BuildRoundUI();
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (this.tabHome.IsSelected && this.runningModel.LoadModel())
            {
                BuildRoundUI();
            }            

            if (this.tabResult.IsSelected) {
                var viewModel = new ViewModel();
                this.resultDataGrid.ItemsSource = viewModel.ResultList;
            }
           
            if (this.tabLog.IsSelected)
            {
                var viewModel = new ViewModel();
                this.logDataGrid.ItemsSource = viewModel.LogList;
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

        private void SettingTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
          
        }       

        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            var viewPrize = ((Button)sender).DataContext as ViewPrize;
            if(viewPrize == null || viewPrize.Id == 0)
            {
                MessageBox.Show("You need to save the item first!");
                return;
            }

            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                var fileSize = (new FileInfo(op.FileName)).Length;
                if (fileSize > 5000000) {
                    MessageBox.Show("File size should be less than 5M", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    
                    return;
                }

                var image = new BitmapImage(new Uri(op.FileName));
                

                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));

                var directoryPath = AppDomain.CurrentDomain.BaseDirectory + @"\AppData\Images";
                if (!Directory.Exists(directoryPath))
                {
                    DirectoryInfo di = Directory.CreateDirectory(directoryPath);
                }

                var path = directoryPath + @"\" + viewPrize.Id.ToString() + @".png";
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
            var validation = ValidationModel.ValidatePrizes(prizes);
            if (string.IsNullOrEmpty(validation))
            {
                this.commonRepository.UpdatePrize(prizes);
                this.prizeDataGrid.ItemsSource = null;
                this.prizeDataGrid.ItemsSource = (new ViewModel()).PrizeList;               

                MessageBox.Show("Saved successfully!", "Inform", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show(validation, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveIgnoredListButton_Click(object sender, RoutedEventArgs e)
        {
            this.commonRepository.UpdateIgnoredList(this.ignoredListDataGrid.Items.OfType<Setting>());
            this.runningModel.LoadSetting();
            UpdateIgnoredListText();
            MessageBox.Show("Saved successfully!", "Inform", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SaveRangeButton_Click(object sender, RoutedEventArgs e)
        {
            this.commonRepository.UpdateRange(this.rangeDataGrid.Items.OfType<Setting>());
            this.runningModel.LoadSetting();
            UpdateRandomRangeText();
            MessageBox.Show("Saved successfully!", "Inform", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            var startIndex = 0;
            if (int.TryParse(this.textPrizeStartIndex.Text, out startIndex) && startIndex > 0)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to start over a new game?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    this.commonRepository.UpdateStartIndex(startIndex);
                    this.commonRepository.StartNewGame();
                    this.runningModel.ResetModel();
                    MessageBox.Show("New Game started!", "Inform", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Prize Start Index must be a positive number", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }        

        private async Task AnimateRandom(int range, string subRandom, Label l)
        {
            Random rnd = new Random();
            for (var i = 0; i < 100; i++)
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
                
            if (passwordDialog.Answer == "123") {
                this.mainTab.SelectedIndex = 1;
            }
            else
            {
                MessageBox.Show("Wrong password!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to exit?", "Exit Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
    }
}

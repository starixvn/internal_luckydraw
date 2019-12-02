using LuckyDraw.Constants;
using LuckyDraw.Export;
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
using System.Windows.Input;
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
        private readonly IResultExporter resultExporter;
        private bool isLoading;

        public MainWindow() : this(null, null, null, null, null, null, null)
        {
        }

        public MainWindow(
            ICommonService commonService,
            ISeedService seedService,
            IRunningService runningService,
            IValidationService validationService,
            IAuthenticationService authenticationService,
            IUtils utils,
            IResultExporter resultExporter)
        {
            this.commonService = commonService ?? new CommonService();
            this.seedService = seedService ?? new SeedService();
            this.runningService = runningService ?? new RunningService();
            this.validationService = validationService ?? new ValidationService();
            this.authenticationService = authenticationService ?? new AuthenticationService();
            this.utils = utils ?? new Utils();
            this.resultExporter = resultExporter ?? new ResultExporter();

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
            this.randomTimeDataGrid.ItemsSource = commonService.GetSettingList();
        }

        public void UpdateRoundText()
        {
            this.textRound.Text = this.runningService.GetRoundName();
        }

        public void UpdateIgnoredListText() {
            this.textIgnored.Text = this.runningService.GetIgnoredListText();
        }

        public void UpdateRandomRangeText() {
            this.textRandomRange.Text = this.runningService.GetRandomRangeText();
        }

        private void BuildRoundUI() {
            if (this.runningService.IsValid) {
                UpdateIgnoredListText();
                UpdateRandomRangeText();
                UpdateRoundText();
                UpdateRoundUI();
            }
        }

        private Item FindItem(int index)
        {
            var roundView = (UserControl)this.roundViewBox.Child;
            return (Item)((StackPanel)roundView.FindName("viewBoxItem" + (index + 1))).Children[0];
        }

        private void UpdateRoundUI()
        {
            var roundSize = this.runningService.CurrentList.Count;
            Type roundViewType = Type.GetType(string.Format("LuckyDraw.Views.RoundView{0}", roundSize));
            this.roundViewBox.Child = (UserControl)Activator.CreateInstance(roundViewType);

            for (var i = 0; i < roundSize; i++)
            {
                var item = FindItem(i);
                item.LoadItemData(this.runningService.CurrentList[i]);
                if (this.runningService.IsSkipped(this.runningService.CurrentList[i]))
                {
                    item.BlurItem();
                }
            }
        }

        private bool IsRoundFinishRun()
        {
            for (var i = 0; i < this.runningService.CurrentList.Count; i++)
            {
                if (this.runningService.IsRunnable(this.runningService.CurrentList[i]))
                {
                    return false;
                }
            }

            return true;
        }

        private async Task RunRandoms() {
            var randomTrasitionTime = this.runningService.Setting.RandomTransitionDuration * 1000;
            var randomTime = this.runningService.Setting.RandomAnimationDuration * 1000 / 3;
            for (var i = 0; i < this.runningService.CurrentList.Count; i++) {
                if (this.runningService.IsRunnable(this.runningService.CurrentList[i]))
                {                    
                    string random = this.runningService.CreateRandomNumber();
                    await FindItem(i).RunRandom(random, randomTime);
                    this.runningService.UpdateRandom(random, i);
                    await Task.Delay(randomTrasitionTime);
                }
            }
        }
        
        private async void RunButton_Click(object sender, RoutedEventArgs e) {
            await Run(); 
        }

        private async Task Run()
        {
            if (this.isLoading || !this.runningService.IsValid || this.IsRoundFinishRun())
            {
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
            GoPrevious();
        }

        private void GoPrevious()
        {
            if (this.isLoading)
            {
                return;
            }

            if (this.runningService.MoveBack())
            {
                BuildRoundUI();
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            GoNext();
        }

        private void GoNext()
        {
            if (this.isLoading)
            {
                return;
            }

            if (this.runningService.MoveNext())
            {
                BuildRoundUI();
            }
        }

        private async void HandleKeyDown(object sender, KeyEventArgs e)
        {            
            if (e.Key == Key.Enter)
            {
                await Run();
            }           
            else if(e.Key == Key.Right)
            {
                GoNext();
            }
            else if(e.Key == Key.Left)
            {
                GoPrevious();
            }
            else
            {
                // Do nothing
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
                utils.SaveImageFile(op.FileName, path);

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
            var settings = this.rangeDataGrid.Items.OfType<Setting>();
            var validation = this.validationService.ValidateRange(settings.First());
            if (string.IsNullOrEmpty(validation))
            {
                commonService.UpdateRange(settings);
                this.runningService.LoadSetting();
                UpdateRandomRangeText();
                MessageBox.Show(Labels.SaveSuccess, Labels.Inform, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show(validation, Labels.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }   
        }

        private void SaveRandomTimeButton_Click(object sender, RoutedEventArgs e)
        {
            var settings = this.randomTimeDataGrid.Items.OfType<Setting>();
            var validation = this.validationService.ValidateRandomTime(settings.First());
            if (string.IsNullOrEmpty(validation))
            {
                commonService.UpdateRandomTime(this.randomTimeDataGrid.Items.OfType<Setting>());
                this.runningService.LoadSetting();
                MessageBox.Show(Labels.SaveSuccess, Labels.Inform, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show(validation, Labels.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }            
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show(Labels.ConfirmNewGame, Labels.Confirmation, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                commonService.StartNewGame();
                commonService.UpdateRunningIndex(1);
                this.runningService.ResetModel();
                MessageBox.Show(Labels.InformNewGame, Labels.Inform, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
        }

        private void UpdateRunningIndexButton_Click(object sender, RoutedEventArgs e)
        {
            int runningIndex = 0;
            if (int.TryParse(this.textRunningIndex.Text, out runningIndex) && runningIndex > 0)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show(Labels.ConfirmUpdateRunningIndex, Labels.Confirmation, MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    commonService.UpdateRunningIndex(runningIndex);
                    MessageBox.Show(Labels.SaveSuccess, Labels.Inform, MessageBoxButton.OK, MessageBoxImage.Information);
                    this.runningService.ResetModel();
                }
            }
            else
            {
                MessageBox.Show(Labels.WrongRunningIndex, Labels.Error, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }        

        private void ResetRandomButton_Click(object sender, RoutedEventArgs e)
        {
            int index = 0;
            if (!(int.TryParse(this.textResetRandoms.Text, out index) && index > 0))
            {
                MessageBox.Show(Labels.WrongRunningIndex, Labels.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBoxResult messageBoxResult = MessageBox.Show(Labels.ConfirmResetRandom, Labels.Confirmation, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                var result = this.runningService.ResetRandom(index);
                if (result)
                {
                    MessageBox.Show(Labels.SaveSuccess, Labels.Inform, MessageBoxButton.OK, MessageBoxImage.Information);
                    this.runningService.ResetModel();
                }
                else
                {
                    MessageBox.Show(Labels.RunningIndexOutOfRange, Labels.Error, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void ExportResultButton_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new SaveFileDialog();
            dlg.FileName = $"LuckyDrawResult_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";
            dlg.DefaultExt = ".xlsx";
            dlg.Filter = "Excel (.xlsx)|*.xlsx";

            var result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                var data = this.commonService.GetResultList();
                resultExporter.Export(data, filename);
                MessageBox.Show(Labels.ExportSuccess, Labels.Inform, MessageBoxButton.OK, MessageBoxImage.Information);
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

using FluentAssertions;
using LuckyDraw.Export;
using LuckyDraw.Service;
using LuckyDraw.Utilities;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using Xunit;

namespace LuckyDraw.UnitTests.Views
{
    public class MainWindowTests
    {
        private readonly ICommonService commonService;
        private readonly ISeedService seedService;
        private readonly IRunningService runningService;
        private readonly IValidationService validationService;
        private readonly IAuthenticationService authenticationService;
        private readonly IUtils utils;
        private readonly IResultExporter resultExporter;

        public MainWindowTests()
        {
            commonService = Substitute.For<ICommonService>();
            seedService = Substitute.For<ISeedService>();
            runningService = Substitute.For<IRunningService>();
            validationService = Substitute.For<IValidationService>();
            authenticationService = Substitute.For<IAuthenticationService>();
            resultExporter = Substitute.For<IResultExporter>();
            utils = Substitute.For<IUtils>();
        }

        [WpfFact]
        public void MainWindow_Init_WithoutError()
        {
            // Act && Assert
            new MainWindow(commonService, 
                seedService, 
                runningService, 
                validationService, 
                authenticationService, 
                utils,
                resultExporter);
        }

        [WpfFact]
        public void UpdateRoundText_WhenCalled_UpdateRoundText()
        {
            // Arrange
            var expectedRoundText = "abcd";
            runningService.GetRoundName().Returns(expectedRoundText);
            var mainWindow = new MainWindow(commonService, seedService, runningService, validationService, authenticationService, utils, resultExporter);

            // Act
            mainWindow.UpdateRoundText();

            // Assert
            var textRound = (TextBlock)mainWindow.FindName("textRound");
            textRound.Text.Should().Be(expectedRoundText);
        }

        [WpfFact]
        public void UpdateIgnoredListText_WhenCalled_UpdateIgnoredListText()
        {
            // Arrange            
            var expectedIgnoredText = "100 - 200 - 300";
            runningService.GetIgnoredListText().Returns(expectedIgnoredText);
            var mainWindow = new MainWindow(commonService, seedService, runningService, validationService, authenticationService, utils, resultExporter);

            // Act
            mainWindow.UpdateIgnoredListText();

            // Assert
            var textRound = (TextBlock)mainWindow.FindName("textIgnored");
            textRound.Text.Should().Be(expectedIgnoredText);
        }

        [WpfFact]
        public void UpdateRandomRangeText_WhenCalled_UpdateRandomRangeText()
        {
            // Arrange           
            var expectedRandomText = "101-202";
            runningService.GetRandomRangeText().Returns(expectedRandomText);
            var mainWindow = new MainWindow(commonService, seedService, runningService, validationService, authenticationService, utils, resultExporter);

            // Act
            mainWindow.UpdateRandomRangeText();

            // Assert
            var textRound = (TextBlock)mainWindow.FindName("textRandomRange");
            textRound.Text.Should().Be(expectedRandomText);
        }

        [WpfFact]
        public void HomeMenuClick_IsLoading_DoNothing()
        {
            // Arrange           
            var mainWindow = new MainWindow(commonService, seedService, runningService, validationService, authenticationService, utils, resultExporter);
            mainWindow.UpdateIsLoading(true);

            // Act && Assert
            mainWindow.HomeMenu_Click(null, null);
        }

        [WpfFact]
        public void SettingMenuClick_IsLoading_DoNothing()
        {
            // Arrange           
            var mainWindow = new MainWindow(commonService, seedService, runningService, validationService, authenticationService, utils, resultExporter);
            mainWindow.UpdateIsLoading(true);

            // Act && Assert
            mainWindow.SettingMenu_Click(null, null);
        }

        [WpfFact]
        public void ResultMenuClick_IsLoading_DoNothing()
        {
            // Arrange           
            var mainWindow = new MainWindow(commonService, seedService, runningService, validationService, authenticationService, utils, resultExporter);
            mainWindow.UpdateIsLoading(true);

            // Act && Assert
            mainWindow.ResultMenu_Click(null, null);
        }

        [WpfFact]
        public void LogMenuClick_IsLoading_DoNothing()
        {
            // Arrange           
            var mainWindow = new MainWindow(commonService, seedService, runningService, validationService, authenticationService, utils, resultExporter);
            mainWindow.UpdateIsLoading(true);

            // Act && Assert
            mainWindow.LogMenu_Click(null, null);
        }

        [WpfFact]
        public void MinimizeMenuClick_IsLoading_DoNothing()
        {
            // Arrange           
            var mainWindow = new MainWindow(commonService, seedService, runningService, validationService, authenticationService, utils, resultExporter);
            mainWindow.UpdateIsLoading(true);

            // Act && Assert
            mainWindow.MinimizeMenu_Click(null, null);
        }

        [WpfFact]
        public void ExitMenuClick_IsLoading_DoNothing()
        {
            // Arrange           
            var mainWindow = new MainWindow(commonService, seedService, runningService, validationService, authenticationService, utils, resultExporter);
            mainWindow.UpdateIsLoading(true);

            // Act && Assert
            mainWindow.ExitMenu_Click(null, null);
        }
    }
}

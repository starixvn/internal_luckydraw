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
            textRound.Text.Should().Be(expectedRoundText.ToUpper());
        }

        [WpfFact]
        public void UpdateIgnoredListText_WhenCalled_UpdateIgnoredListText()
        {
            // Arrange
            var setting = new Setting
            {
                Excluded = "100, 200, 300"
            };
            var expectedIgnoredText = "100 - 200 - 300";
            runningService.Setting.Returns(setting);
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
            var setting = new Setting
            {
                RandomFrom = 101,
                RandomTo = 202
            };
            var expectedRandomText = "101-202";
            runningService.Setting.Returns(setting);
            var mainWindow = new MainWindow(commonService, seedService, runningService, validationService, authenticationService, utils, resultExporter);

            // Act
            mainWindow.UpdateRandomRangeText();

            // Assert
            var textRound = (TextBlock)mainWindow.FindName("textRandomRange");
            textRound.Text.Should().Be(expectedRandomText);
        }
    }
}

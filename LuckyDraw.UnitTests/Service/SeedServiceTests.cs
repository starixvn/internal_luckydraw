using LuckyDraw.Repository;
using LuckyDraw.Service;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LuckyDraw.UnitTests.Service
{
    public class SeedServiceTests
    {
        [Fact]
        public void GenerateSeedData_WhenPrizesNotEmptyAndSettingNotEmpty_DoNothing()
        {
            // Arrange
            var commonRepository = Substitute.For<ICommonRepository>();
            var service = new SeedService(commonRepository);
            commonRepository.IsEmptySetting().Returns(false);
            commonRepository.IsEmptyPrize().Returns(false);

            // Act
            service.GenerateSeedData();

            // Assert
            commonRepository.DidNotReceive().AddPrizes(Arg.Any<List<Prize>>());
            commonRepository.DidNotReceive().AddSetting(Arg.Any<Setting>());
        }      

        [Fact]
        public void GenerateSeedData_WhenSettingIsEmpty_AddSetting()
        {
            // Arrange
            var commonRepository = Substitute.For<ICommonRepository>();
            var service = new SeedService(commonRepository);
            commonRepository.IsEmptySetting().Returns(true);

            // Act
            service.GenerateSeedData();

            // Assert
            commonRepository.Received().AddSetting(Arg.Any<Setting>());
        }

        [Fact]
        public void GenerateSeedData_WhenPrizesEmpty_AddPrizes()
        {
            // Arrange
            var commonRepository = Substitute.For<ICommonRepository>();
            var service = new SeedService(commonRepository);
            commonRepository.IsEmptyPrize().Returns(true);

            // Act
            service.GenerateSeedData();

            // Assert
            commonRepository.Received().AddPrizes(Arg.Any<List<Prize>>());
        }
    }
}

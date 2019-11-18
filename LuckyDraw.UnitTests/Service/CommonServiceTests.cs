using AutoFixture;
using FluentAssertions;
using LuckyDraw.Model;
using LuckyDraw.Repository;
using LuckyDraw.Service;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LuckyDraw.UnitTests.Service
{
    public class CommonServiceTests
    {
        private readonly Fixture fixture;

        public CommonServiceTests()
        {
            fixture = new Fixture();
        }

        [Fact]
        public void CommonService_Init_WithoutError()
        {
            // Act && Assert
            new CommonService();
        }

        [Fact]
        public void UpdateIgnoredList_WhenCalled_CallUpdateIgnoredList()
        {
            // Arrange
            var commonRepository = Substitute.For<ICommonRepository>();
            var service = new CommonService(commonRepository);
            var settings = fixture.CreateMany<Setting>();

            // Act
            service.UpdateIgnoredList(settings);

            // Assert
            commonRepository.Received().UpdateIgnoredList(settings);
        }

        [Fact]
        public void UpdatePrize_WhenCalled_CallUpdatePrize()
        {
            // Arrange
            var commonRepository = Substitute.For<ICommonRepository>();
            var service = new CommonService(commonRepository);
            var viewPrizes = fixture.CreateMany<ViewPrize>();

            // Act
            service.UpdatePrize(viewPrizes);

            // Assert
            commonRepository.Received().UpdatePrize(viewPrizes);
        }

        [Fact]
        public void UpdateRange_WhenCalled_CallUpdateRange()
        {
            // Arrange
            var commonRepository = Substitute.For<ICommonRepository>();
            var service = new CommonService(commonRepository);
            var settings = fixture.CreateMany<Setting>();

            // Act
            service.UpdateRange(settings);

            // Assert
            commonRepository.Received().UpdateRange(settings);
        }

        [Fact]
        public void UpdateRandomTime_WhenCalled_UpdateRandomeTime()
        {
            // Arrange
            var commonRepository = Substitute.For<ICommonRepository>();
            var service = new CommonService(commonRepository);
            var settings = fixture.CreateMany<Setting>();

            // Act
            service.UpdateRandomTime(settings);

            // Assert
            commonRepository.Received().UpdateRandomTime(settings);
        }

        [Fact]
        public void UpdateRunningIndex_WhenCalled_CallUpdateRunningIndex()
        {
            // Arrange
            var commonRepository = Substitute.For<ICommonRepository>();
            var service = new CommonService(commonRepository);
            var runningIndex = fixture.Create<int>();

            // Act
            service.UpdateRunningIndex(runningIndex);

            // Assert
            commonRepository.Received().UpdateRunningIndex(runningIndex);
        }

        [Fact]
        public void StartNewGame_WhenCalled_CallStartNewGame()
        {
            // Arrange
            var commonRepository = Substitute.For<ICommonRepository>();
            var service = new CommonService(commonRepository);

            // Act
            service.StartNewGame();

            // Assert
            commonRepository.Received().StartNewGame();
        }

        [Fact]
        public void GetPrizeList_WhenCalled_GetPrizeList()
        {
            // Arrange
            var commonRepository = Substitute.For<ICommonRepository>();
            var service = new CommonService(commonRepository);
            var prizes = fixture.CreateMany<Prize>().ToList();
            commonRepository.GetPrizeList().Returns(prizes);

            var expectedResult = new ObservableCollection<ViewPrize>();
            foreach (var h in prizes)
            {
                var p = new ViewPrize();
                p.Id = h.Id;
                p.PrizeIndex = h.PrizeIndex;
                p.PrizeName = h.PrizeName;
                p.RoundIndex = h.RoundIndex;
                p.RoundName = h.RoundName;

                expectedResult.Add(p);
            }

            // Act
            var result = service.GetPrizeList();

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void GetSettingList_WhenCalled_GetSettingList()
        {
            // Arrange
            var commonRepository = Substitute.For<ICommonRepository>();
            var service = new CommonService(commonRepository);
            var settings = fixture.CreateMany<Setting>().ToList();
            commonRepository.GetSettingList().Returns(settings);

            var expectedResult = new ObservableCollection<Setting>();
            foreach (var h in settings)
            {
                expectedResult.Add(h);
            }

            // Act
            var result = service.GetSettingList();

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void GetResultList_WhenCalled_GetResultList()
        {
            // Arrange
            var commonRepository = Substitute.For<ICommonRepository>();
            var service = new CommonService(commonRepository);
            var results = fixture.CreateMany<Result>().ToList();
            commonRepository.GetResultList().Returns(results);

            var expectedResult = new ObservableCollection<ViewResult>();
            foreach (var h in results.ToList())
            {
                var r = new ViewResult();
                r.PrizeIndex = h.PrizeIndex;
                r.PrizeName = h.PrizeName;
                r.Random = h.Random;
                r.RoundName = h.RoundName;

                expectedResult.Add(r);
            }

            // Act
            var result = service.GetResultList();

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public void GetLogList_WhenCalled_GetLogList()
        {
            // Arrange
            var commonRepository = Substitute.For<ICommonRepository>();
            var service = new CommonService(commonRepository);
            var results = fixture.CreateMany<Result>().ToList();
            commonRepository.GetLogList().Returns(results);

            var expectedResult = new ObservableCollection<ViewLog>();
            foreach (var h in results)
            {
                var r = new ViewLog();
                r.Flag = h.Flag;
                r.PrizeIndex = h.PrizeIndex;
                r.PrizeName = h.PrizeName;
                r.Random = h.Random;
                r.RoundName = h.RoundName;
                r.UpdatedTime = h.UpdatedTime;

                expectedResult.Add(r);
            }

            // Act
            var result = service.GetLogList();

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}

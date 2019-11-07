using AutoFixture;
using FluentAssertions;
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
    public class RunningServiceTests
    {
        private readonly Fixture fixture;
        public RunningServiceTests()
        {
            fixture = new Fixture();
        }

        [Fact]
        public void RunningService_Init_WithoutError()
        {
            // Act && Assert
            new RunningService();
        }

        [Fact]
        public void GetRoundName_WhenCalled_GetCurrentRoundName()
        {
            // Arrange
            var commonRepository = Substitute.For<ICommonRepository>();
            var service = new RunningService(commonRepository);
            var results = FakeResult();
            var setting = fixture.Create<Setting>();
            commonRepository.GetCurrentResultList(Arg.Any<int>()).Returns(results);
            commonRepository.IsDBReady().Returns(true);
            commonRepository.GetSetting().Returns(setting);

            // Act
            service.LoadModel();
            var result = service.GetRoundName();

            // Assert
            result.Should().Be(results.First().RoundName.ToUpper());
        }

        [Fact]
        public void GetIgnoredListText_WhenCalled_ReturnGetIgnoredListText()
        {
            // Arrange
            var commonRepository = Substitute.For<ICommonRepository>();
            var service = new RunningService(commonRepository);
            var setting = new Setting
            {
                Excluded = "100, 200, 300"
            };
            commonRepository.GetSetting().Returns(setting);
            var expectedIgnoredListText = "100 - 200 - 300";

            // Act
            service.LoadSetting();
            var result = service.GetIgnoredListText();

            // Assert
            result.Should().Be(expectedIgnoredListText);
        }

        [Fact]
        public void GetRandomRangeText_WhenCalled_ReturnGetRandomRangeText()
        {
            // Arrange
            var commonRepository = Substitute.For<ICommonRepository>();
            var service = new RunningService(commonRepository);
            var setting = new Setting
            {
                RandomFrom = 101,
                RandomTo = 202
            };
            commonRepository.GetSetting().Returns(setting);
            var expectedRandomRange = "101-202";

            // Act
            service.LoadSetting();
            var result = service.GetRandomRangeText();

            // Assert
            result.Should().Be(expectedRandomRange);
        }

        [Fact]
        public void ResetModel_WhenCalled_ResetIsValid()
        {
            // Arrange
            var commonRepository = Substitute.For<ICommonRepository>();
            var service = new RunningService(commonRepository);
            var results = FakeResult();
            var setting = fixture.Create<Setting>();
            commonRepository.GetCurrentResultList(Arg.Any<int>()).Returns(results);
            commonRepository.IsDBReady().Returns(true);
            commonRepository.GetSetting().Returns(setting);

            // Act
            service.LoadModel();
            service.ResetModel();

            // Assert
            service.IsValid.Should().Be(false);
        }

        [Fact]
        public void LoadSetting_WhenCalled_GetSetting()
        {
            // Arrange
            var commonRepository = Substitute.For<ICommonRepository>();
            var service = new RunningService(commonRepository);
            var setting = fixture.Create<Setting>();
            commonRepository.GetSetting().Returns(setting);

            // Assert
            service.LoadSetting();

            // Assert
            service.Setting.Should().BeEquivalentTo(setting);
        }

        [Fact]
        public void UpdateRandom_WhenRandomIsZero_DoNothing()
        {
            // Arrange
            var commonRepository = Substitute.For<ICommonRepository>();
            var service = new RunningService(commonRepository);
            var results = FakeResult();
            var setting = fixture.Create<Setting>();
            commonRepository.GetCurrentResultList(Arg.Any<int>()).Returns(results);
            commonRepository.IsDBReady().Returns(true);
            commonRepository.GetSetting().Returns(setting);

            var random = "000";
            var index = 1;

            // Act
            service.LoadModel();
            service.UpdateRandom(random, index);

            // Assert
            commonRepository.DidNotReceive().UpdateRandom(service.CurrentList[index].Id, random);
        }

        [Fact]
        public void UpdateRandom_WhenCalled_UpdateRandom()
        {
            // Arrange
            var commonRepository = Substitute.For<ICommonRepository>();
            var service = new RunningService(commonRepository);
            var results = FakeResult();
            var setting = fixture.Create<Setting>();
            commonRepository.GetCurrentResultList(Arg.Any<int>()).Returns(results);
            commonRepository.IsDBReady().Returns(true);
            commonRepository.GetSetting().Returns(setting);

            var random = "132";
            var index = 1;

            // Act
            service.LoadModel();
            service.UpdateRandom(random, index);

            // Assert
            service.CurrentList[index].Random.Should().Be(random);
            service.CurrentList[index].Status.Should().Be(1);
            commonRepository.Received().UpdateRandom(service.CurrentList[index].Id, random);
        }

        [Fact]
        public void MoveNext_WhenIsValidFalse_ReturnFalse()
        {
            // Arrange
            var commonRepository = Substitute.For<ICommonRepository>();
            var service = new RunningService(commonRepository);

            // Act
            var result = service.MoveNext();

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void MoveNext_WhenMaxRound_ReturnFalse()
        {
            // Arrange
            var commonRepository = Substitute.For<ICommonRepository>();
            var service = new RunningService(commonRepository);
            var results = new List<Result>
            {
                new Result
                {
                     RoundIndex = 1, RoundName = "a1"
                }
            };
            var setting = fixture.Create<Setting>();
            commonRepository.GetCurrentResultList(Arg.Any<int>()).Returns(results);
            commonRepository.IsDBReady().Returns(true);
            commonRepository.GetSetting().Returns(setting);

            // Act
            service.LoadModel();
            var result = service.MoveNext();

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void MoveNext_WhenNotMaxRound_UpdateCurrentRound()
        {
            // Arrange
            var commonRepository = Substitute.For<ICommonRepository>();
            var service = new RunningService(commonRepository);
            var results = FakeResult();
            var setting = fixture.Create<Setting>();
            commonRepository.GetCurrentResultList(Arg.Any<int>()).Returns(results);
            commonRepository.IsDBReady().Returns(true);
            commonRepository.GetSetting().Returns(setting);

            var expectedRound = 1;
            var exprectedCurrentList = new List<Result>
            {
                results[2]
            };

            // Act
            service.LoadModel();
            var result = service.MoveNext();

            // Assert
            result.Should().BeTrue();
            service.CurrentRound.Should().Be(expectedRound);
            service.CurrentList.Should().BeEquivalentTo(exprectedCurrentList);
        }

        [Fact]
        public void MoveBack_WhenIsValidFalse_ReturnFalse()
        {
            // Arrange
            var commonRepository = Substitute.For<ICommonRepository>();
            var service = new RunningService(commonRepository);

            // Act
            var result = service.MoveBack();

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void MoveBack_WhenMinRound_ReturnFalse()
        {
            // Arrange
            var commonRepository = Substitute.For<ICommonRepository>();
            var service = new RunningService(commonRepository);
            var results = new List<Result>
            {
                new Result
                {
                     RoundIndex = 1, RoundName = "a1"
                }
            };
            var setting = fixture.Create<Setting>();
            commonRepository.GetCurrentResultList(Arg.Any<int>()).Returns(results);
            commonRepository.IsDBReady().Returns(true);
            commonRepository.GetSetting().Returns(setting);

            // Act
            service.LoadModel();
            var result = service.MoveBack();

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void MoveBack_WhenNotMinRound_UpdateCurrentRound()
        {
            // Arrange
            var commonRepository = Substitute.For<ICommonRepository>();
            var service = new RunningService(commonRepository);
            var results = FakeResult();
            var setting = fixture.Create<Setting>();
            commonRepository.GetCurrentResultList(Arg.Any<int>()).Returns(results);
            commonRepository.IsDBReady().Returns(true);
            commonRepository.GetSetting().Returns(setting);

            var expectedRound = 0;
            var exprectedCurrentList = new List<Result>
            {
                results[0],
                results[1]
            };

            // Act
            service.LoadModel();
            service.MoveNext();
            var result = service.MoveBack();

            // Assert
            result.Should().BeTrue();
            service.CurrentRound.Should().Be(expectedRound);
            service.CurrentList.Should().BeEquivalentTo(exprectedCurrentList);
        }

        [Fact]
        public void IsExistRandom_WhenNotExistRandom_ReturnFalse()
        {
            var commonRepository = Substitute.For<ICommonRepository>();
            var service = new RunningService(commonRepository);
            var results = new List<Result>
            {
                new Result { RoundIndex = 1, RoundName = "a1", Random = "200" }
            };
            var setting = new Setting { Excluded = "100" };
            commonRepository.GetCurrentResultList(Arg.Any<int>()).Returns(results);
            commonRepository.IsDBReady().Returns(true);
            commonRepository.GetSetting().Returns(setting);

            var random = "300";

            // Act
            service.LoadModel();
            var result = service.IsExistRandom(random);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsExistRandom_WhenExistInRunningList_ReturnTrue()
        {
            // Arrange
            var commonRepository = Substitute.For<ICommonRepository>();
            var service = new RunningService(commonRepository);
            var results = new List<Result>
            {
                new Result { RoundIndex = 1, RoundName = "a1", Random = "200" },
                new Result { RoundIndex = 2, RoundName = "a2", Random = "300" }
            };
            var setting = new Setting { Excluded = "100" };
            commonRepository.GetCurrentResultList(Arg.Any<int>()).Returns(results);
            commonRepository.IsDBReady().Returns(true);
            commonRepository.GetSetting().Returns(setting);

            var random = "300";

            // Act
            service.LoadModel();
            var result = service.IsExistRandom(random);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsExistRandom_WhenExistInExcluded_ReturnTrue()
        {
            var commonRepository = Substitute.For<ICommonRepository>();
            var service = new RunningService(commonRepository);
            var results = new List<Result>
            {
                new Result { RoundIndex = 1, RoundName = "a1", Random = "200" }
            };
            var setting = new Setting { Excluded = "100, 300" };
            commonRepository.GetCurrentResultList(Arg.Any<int>()).Returns(results);
            commonRepository.IsDBReady().Returns(true);
            commonRepository.GetSetting().Returns(setting);

            var random = "300";

            // Act
            service.LoadModel();
            var result = service.IsExistRandom(random);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void LoadModel_WhenIsValid_ReturnFalse()
        {
            // Arrange
            var commonRepository = Substitute.For<ICommonRepository>();
            var service = new RunningService(commonRepository);
            var results = FakeResult();
            var setting = fixture.Create<Setting>();
            commonRepository.GetCurrentResultList(Arg.Any<int>()).Returns(results);
            commonRepository.IsDBReady().Returns(true);
            commonRepository.GetSetting().Returns(setting);

            // Act
            service.LoadModel();
            var result = service.LoadModel();

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void LoadModel_WhenDBIsNotReady_ReturnFalse()
        {
            // Arrange
            var commonRepository = Substitute.For<ICommonRepository>();
            var service = new RunningService(commonRepository);
            commonRepository.IsDBReady().Returns(false);

            // Act
            var result = service.LoadModel();

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void LoadModel_WhenEmptyResultList_ReturnFalse()
        {
            // Arrange
            var commonRepository = Substitute.For<ICommonRepository>();
            var service = new RunningService(commonRepository);
            var results = new List<Result>();
            var setting = fixture.Create<Setting>();
            commonRepository.GetCurrentResultList(Arg.Any<int>()).Returns(results);
            commonRepository.IsDBReady().Returns(true);
            commonRepository.GetSetting().Returns(setting);

            // Act
            var result = service.LoadModel();

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void LoadModel_WhenSuccess_ReturnTrue()
        {
            // Arrange
            var commonRepository = Substitute.For<ICommonRepository>();
            var service = new RunningService(commonRepository);
            var results = FakeResult();
            var setting = fixture.Create<Setting>();
            commonRepository.GetCurrentResultList(Arg.Any<int>()).Returns(results);
            commonRepository.IsDBReady().Returns(true);
            commonRepository.GetSetting().Returns(setting);

            var expectedCurrentRound = 0;
            var expectedMaxRound = 1;
            var expectedCurrentList = new List<Result>
            {
                new Result { RoundIndex = 1, RoundName = "a1"},
                new Result { RoundIndex = 1, RoundName = "a1"}
            };

            // Act
            var result = service.LoadModel();

            // Assert
            result.Should().BeTrue();
            service.IsValid.Should().BeTrue();
            service.MaxRound.Should().Be(expectedMaxRound);
            service.CurrentRound.Should().Be(expectedCurrentRound);
            service.CurrentList.Should().BeEquivalentTo(expectedCurrentList);
        }

        [Fact]
        public void CreateRandomNumber_WhenCanNotFindRandom_ReturnZero()
        {
            // Arrange
            var commonRepository = Substitute.For<ICommonRepository>();
            var service = new RunningService(commonRepository);
            var results = new List<Result>
            {
                new Result { RoundIndex = 1, RoundName = "a1", Random = "010" },
                new Result { RoundIndex = 2, RoundName = "a2", Random = "012" }
            };
            var setting = new Setting { Excluded = "011", RandomFrom = 10, RandomTo = 12 };
            commonRepository.GetCurrentResultList(Arg.Any<int>()).Returns(results);
            commonRepository.IsDBReady().Returns(true);
            commonRepository.GetSetting().Returns(setting);

            // Act
            service.LoadModel();
            var result = service.CreateRandomNumber();

            // Assert
            result.Should().Be("000");
        }

        [Fact]
        public void CreateRandomNumber_WhenCanFindRandom_ReturnRandom()
        {
            // Arrange
            var commonRepository = Substitute.For<ICommonRepository>();
            var service = new RunningService(commonRepository);
            var results = new List<Result>
            {
                new Result { RoundIndex = 1, RoundName = "a1", Random = "010" },
                new Result { RoundIndex = 2, RoundName = "a2", Random = "012" }
            };
            var excluded = string.Empty;
            var expectedRandom = 920;
            for(var i = 1; i < 1000; i++)
            {
                if (i != expectedRandom)
                {
                    excluded = excluded + "," + i.ToString("000");
                }
            }
            var setting = new Setting { Excluded = excluded, RandomFrom = 1, RandomTo = 999 };
            commonRepository.GetCurrentResultList(Arg.Any<int>()).Returns(results);
            commonRepository.IsDBReady().Returns(true);
            commonRepository.GetSetting().Returns(setting);

            // Act
            service.LoadModel();
            var result = service.CreateRandomNumber();

            // Assert
            result.Should().Be(expectedRandom.ToString("000"));
        }

        private List<Result> FakeResult()
        {
            return new List<Result>
            {
                new Result
                {
                     RoundIndex = 1, RoundName = "a1"
                },
                new Result
                {
                     RoundIndex = 1, RoundName = "a1"
                },
                new Result
                {
                     RoundIndex = 2, RoundName = "a2"
                }
            };
        }
    }
}

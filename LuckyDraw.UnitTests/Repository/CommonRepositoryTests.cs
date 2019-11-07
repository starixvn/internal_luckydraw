using AutoFixture;
using Effort;
using Effort.Provider;
using FluentAssertions;
using LuckyDraw.DBContexts;
using LuckyDraw.Model;
using LuckyDraw.Repository;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LuckyDraw.UnitTests.Repository
{
    public class CommonRepositoryTests
    {
        private readonly Fixture fixture;

        public CommonRepositoryTests()
        {
            //Effort.Provider.EffortProviderConfiguration.RegisterProvider();

            fixture = new Fixture();
        }

        [Fact]
        public void CommonRepository_Init_WithoutError()
        {
            // Act && Assert
            new CommonRepository();
        }

        [Fact]
        public void UpdatePrize_WhenCalled_UpdatePrize()
        {
            // Arrange
            var connection = GetConnection();
            var repository = GetRepository(connection);

            var prizes = fixture.CreateMany<Prize>(5).ToList();
            var id = 0;
            using (var db = GetDBContext(connection))
            {
                db.Prizes.AddRange(prizes);
                db.SaveChanges();
                id = db.Prizes.First().Id;
            }

            var newPrizes = fixture.CreateMany<ViewPrize>().ToList();
            newPrizes.First().Id = id;
            newPrizes[1].Id = 0;
            newPrizes[2].Id = 0;

            // Act
            repository.UpdatePrize(newPrizes);

            // Assert
            using (var db = GetDBContext(connection))
            {
                db.Prizes.Count().Should().Be(newPrizes.Count);
                db.Prizes.Find(id).PrizeName.Should().Be(newPrizes.First().PrizeName);
                db.Prizes.Find(id).PrizeIndex.Should().Be(newPrizes.First().PrizeIndex);
            }
        }

        [Fact]
        public void UpdateIgnoredList_WhenSettingEmpty_DoNothing()
        {
            // Arrange
            var connection = GetConnection();
            var repository = GetRepository(connection);

            var settings = fixture.CreateMany<Setting>().ToList();
           
            // Act
            repository.UpdateIgnoredList(settings);

            // Assert
            using (var db = GetDBContext(connection))
            {
                db.Settings.Count().Should().Be(0);
            }
        }

        [Fact]
        public void UpdateIgnoredList_WhenSettingNotEmpty_UpdateIgnoredList()
        {
            // Arrange
            var connection = GetConnection();
            var repository = GetRepository(connection);

            var settings = fixture.CreateMany<Setting>().ToList();
            using (var db = GetDBContext(connection))
            {
                db.Settings.Add(fixture.Create<Setting>());
                db.SaveChanges();
            }

            // Act
            repository.UpdateIgnoredList(settings);

            // Assert
            using (var db = GetDBContext(connection))
            {
                var s = db.Settings.First();
                s.Excluded.Should().Be(settings.First().Excluded);
            }
        }

        [Fact]
        public void UpdateRange_WhenSettingEmpty_DoNothing()
        {
            // Arrange
            var connection = GetConnection();
            var repository = GetRepository(connection);

            var settings = fixture.CreateMany<Setting>().ToList();

            // Act
            repository.UpdateRange(settings);

            // Assert
            using (var db = GetDBContext(connection))
            {
                db.Settings.Count().Should().Be(0);
            }
        }

        [Fact]
        public void UpdateRange_WhenCalled_UpdateRange()
        {
            // Arrange
            var connection = GetConnection();
            var repository = GetRepository(connection);

            var settings = fixture.CreateMany<Setting>().ToList();
            using (var db = GetDBContext(connection))
            {
                db.Settings.Add(fixture.Create<Setting>());
                db.SaveChanges();
            }

            // Act
            repository.UpdateRange(settings);

            // Assert
            using (var db = GetDBContext(connection))
            {
                var s = db.Settings.First();
                s.RandomFrom.Should().Be(settings.First().RandomFrom);
                s.RandomTo.Should().Be(settings.First().RandomTo);
            }
        }

        [Fact]
        public void UpdateRandomTime_WhenSettingEmpty_DoNothing()
        {
            // Arrange
            var connection = GetConnection();
            var repository = GetRepository(connection);

            var settings = fixture.CreateMany<Setting>().ToList();

            // Act
            repository.UpdateRandomTime(settings);

            // Assert
            using (var db = GetDBContext(connection))
            {
                db.Settings.Count().Should().Be(0);
            }
        }

        [Fact]
        public void UpdateRandomTime_WhenCalled_UpdateRandomTime()
        {
            // Arrange
            var connection = GetConnection();
            var repository = GetRepository(connection);

            var settings = fixture.CreateMany<Setting>().ToList();
            using (var db = GetDBContext(connection))
            {
                db.Settings.Add(fixture.Create<Setting>());
                db.SaveChanges();
            }

            // Act
            repository.UpdateRandomTime(settings);

            // Assert
            using (var db = GetDBContext(connection))
            {
                var s = db.Settings.First();
                s.RandomAnimationDuration.Should().Be(settings.First().RandomAnimationDuration);
                s.RandomTransitionDuration.Should().Be(settings.First().RandomTransitionDuration);
            }
        }

        [Fact]
        public void UpdateStartIndex_WhenSettingEmpty_DoNothing()
        {
            // Arrange
            var connection = GetConnection();
            var repository = GetRepository(connection);

            var startIndex = fixture.Create<int>();

            // Act
            repository.UpdateStartIndex(startIndex);

            // Assert
            using (var db = GetDBContext(connection))
            {
                db.Settings.Count().Should().Be(0);
            }
        }

        [Fact]
        public void UpdateStartIndex_WhenCalled_UpdateStartIndex()
        {
            // Arrange
            var connection = GetConnection();
            var repository = GetRepository(connection);

            var startIndex = fixture.Create<int>();
            var settings = fixture.CreateMany<Setting>().ToList();
            using (var db = GetDBContext(connection))
            {
                db.Settings.AddRange(settings);
                db.SaveChanges();
            }

            // Act
            repository.UpdateStartIndex(startIndex);

            // Assert
            using (var db = GetDBContext(connection))
            {
                var s = db.Settings.First();
                s.StartIndex.Should().Be(startIndex);
            }
        }

        [Fact]
        public void StartNewGame_WhenPrizeEmpty_DoNotUpdateResult()
        {
            // Arrange
            var connection = GetConnection();
            var repository = GetRepository(connection);

            // Act
            repository.StartNewGame();

            // Assert
            using (var db = GetDBContext(connection))
            {
                db.Results.Count().Should().Be(0);
            }
        }

        [Fact]
        public void StartNewGame_WhenPrizeNotEmptyAndResultEmpty_UpdateResult()
        {
            // Arrange
            var connection = GetConnection();
            var repository = GetRepository(connection);

            var prizes = fixture.CreateMany<Prize>().ToList();
            using (var db = GetDBContext(connection))
            {
                db.Prizes.AddRange(prizes);
                db.SaveChanges();
            }

            // Act
            repository.StartNewGame();

            // Assert
            using (var db = GetDBContext(connection))
            {
                db.Results.Select(i => i.Flag).Should().BeEquivalentTo(prizes.Select(i => 1));
            }
        }

        [Fact]
        public void StartNewGame_WhenPrizeNotEmptyAndResultNotEmpty_UpdateResult()
        {
            // Arrange
            var connection = GetConnection();
            var repository = GetRepository(connection);

            var prizes = fixture.CreateMany<Prize>().ToList();
            var results = fixture.CreateMany<Result>().ToList();
            using (var db = GetDBContext(connection))
            {
                db.Prizes.AddRange(prizes);
                db.Results.AddRange(results);
                db.SaveChanges();
            }

            // Act
            repository.StartNewGame();

            // Assert
            var flag = results.Max(i => i.Flag) + 1;
            using (var db = GetDBContext(connection))
            {
                db.Results.Where(i => i.Flag == flag).Count().Should().Be(prizes.Count);
            }
        }

        [Fact]
        public void UpdateRandom_WhenCalled_UpdateRandom()
        {
            // Arrange
            var connection = GetConnection();
            var repository = GetRepository(connection);

            int id = 0;
            var random = "234";
            using (var db = GetDBContext(connection))
            {
                db.Results.Add(fixture.Create<Result>());
                db.Results.Add(fixture.Create<Result>());
                db.SaveChanges();
                id = db.Results.First().Id;
            }

            // Act
            repository.UpdateRandom(id, random);

            // Assert
            using (var db = GetDBContext(connection))
            {
                var s = db.Results.Find(id);
                s.Random.Should().Be(random);
                s.Status.Should().Be(1);
            }
        }

        [Fact]
        public void IsDBReady_WhenResultAndSettingNotEmpty_ReturnTrue()
        {
            // Arrange
            var connection = GetConnection();
            var repository = GetRepository(connection);

            using (var db = GetDBContext(connection))
            {
                db.Settings.Add(fixture.Create<Setting>());
                db.Results.Add(fixture.Create<Result>());
                db.SaveChanges();
            }

            // Act
            var result = repository.IsDBReady();

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsDBReady_WhenResultEmpty_ReturnFalse()
        {
            // Arrange
            var connection = GetConnection();
            var repository = GetRepository(connection);

            using (var db = GetDBContext(connection))
            {
                db.Settings.Add(fixture.Create<Setting>());
                db.SaveChanges();
            }

            // Act
            var result = repository.IsDBReady();

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsDBReady_WhenSettingEmpty_ReturnFalse()
        {
            // Arrange
            var connection = GetConnection();
            var repository = GetRepository(connection);

            using (var db = GetDBContext(connection))
            {
                db.Results.Add(fixture.Create<Result>());
                db.SaveChanges();
            }

            // Act
            var result = repository.IsDBReady();

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void GetSetting_WhenCalled_GetFirstSetting()
        {
            // Arrange
            var connection = GetConnection();
            var repository = GetRepository(connection);

            var settings = fixture.CreateMany<Setting>().ToList();
            using (var db = GetDBContext(connection))
            {
                db.Settings.AddRange(settings);
                db.SaveChanges();
            }

            // Act
            var result = repository.GetSetting();

            // Assert
            using (var db = GetDBContext(connection))
            {
                var firstSetting = db.Settings.First();
                result.Should().BeEquivalentTo(firstSetting);
            }   
        }

        [Fact]
        public void GetPrizeList_WhenCalled_GetPrizeList()
        {
            // Arrange
            var connection = GetConnection();
            var repository = GetRepository(connection);

            var prizes = fixture.CreateMany<Prize>().ToList();
            using (var db = GetDBContext(connection))
            {
                db.Prizes.AddRange(prizes);
                db.SaveChanges();
            }

            // Act
            var result = repository.GetPrizeList();

            // Assert
            result.Count.Should().Be(prizes.Count);
        }

        [Fact]
        public void GetSettingList_WhenCalled_GetSettingList()
        {
            // Arrange
            var connection = GetConnection();
            var repository = GetRepository(connection);

            var settings = fixture.CreateMany<Setting>().ToList();
            using (var db = GetDBContext(connection))
            {
                db.Settings.AddRange(settings);                
                db.SaveChanges();
            }

            // Act
            var result = repository.GetSettingList();

            // Assert
            result.Count.Should().Be(settings.Count);
        }

        [Fact]
        public void GetResultList_WhenResultEmpty_ReturnEmptyList()
        {
            // Arrange
            var connection = GetConnection();
            var repository = GetRepository(connection);

            // Act
            var result = repository.GetResultList();

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void GetResultList_WhenResultNotEmpty_ReturnResultList()
        {
            // Arrange
            var connection = GetConnection();
            var repository = GetRepository(connection);

            var results = new List<Result>
            {
                new Result { Flag = 1, PrizeIndex = 1},
                new Result { Flag = 2, PrizeIndex = 2},
                new Result { Flag = 2, PrizeIndex = 3}
            };

            using (var db = GetDBContext(connection))
            {
                db.Results.AddRange(results);
                db.SaveChanges();
            }

            // Act
            var result = repository.GetResultList();

            // Assert
            result.Count.Should().Be(2);
        }

        [Fact]
        public void GetLogList_WhenCalled_GetLogList()
        {
            // Arrange
            var connection = GetConnection();
            var repository = GetRepository(connection);

            var results = fixture.CreateMany<Result>().ToList();
            using (var db = GetDBContext(connection))
            {
                db.Results.AddRange(results);
                db.SaveChanges();
            }

            // Act
            var result = repository.GetLogList();

            // Assert
            result.Count.Should().Be(results.Count);
        }

        [Fact]
        public void AddPrizes_WhenCalled_AddPrizes()
        {
            // Arrange
            var connection = GetConnection();
            var repository = GetRepository(connection);

            var prizes = fixture.CreateMany<Prize>().ToList();

            // Act
            repository.AddPrizes(prizes);

            // Assert
            using (var db = GetDBContext(connection))
            {
                var prizeList = db.Prizes.ToList();
                prizeList.Count.Should().Be(prizes.Count);
            }
        }

        [Fact]
        public void IsEmptyPrize_WhenPrizeEmpty_ReturnTrue()
        {
            // Arrange
            var connection = GetConnection();
            var repository = GetRepository(connection);

            // Act
            var result = repository.IsEmptyPrize();

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsEmptyPrize_WhenPrizeNotEmpty_ReturnFalse()
        {
            // Arrange
            var connection = GetConnection();
            var repository = GetRepository(connection);

            using (var db = GetDBContext(connection))
            {
                db.Prizes.Add(fixture.Create<Prize>());
                db.Prizes.Add(fixture.Create<Prize>());
                db.SaveChanges();
            }

            // Act
            var result = repository.IsEmptyPrize();

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void AddSetting_WhenCalled_AddSetting()
        {
            // Arrange
            var connection = GetConnection();
            var repository = GetRepository(connection);
            var setting = fixture.Create<Setting>();

            // Act
            repository.AddSetting(setting);

            // Assert
            using(var db = GetDBContext(connection))
            {
                var s = db.Settings.First();
                s.RandomFrom.Should().Be(setting.RandomFrom);
                s.RandomTo.Should().Be(setting.RandomTo);
                s.Excluded.Should().Be(setting.Excluded);
                s.StartIndex.Should().Be(setting.StartIndex);
            }
        }

        [Fact]
        public void IsEmptySetting_WhenSettingIsEmpty_ReturnTrue()
        {
            // Arrange
            var connection = GetConnection();
            var repository = GetRepository(connection);

            // Act
            var result = repository.IsEmptySetting();

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsEmptySetting_WhenSettingIsNotEmpty_ReturnFalse()
        {
            // Arrange            
            var connection = GetConnection();
            var repository = GetRepository(connection);

            using(var db = GetDBContext(connection))
            {
                db.Settings.Add(fixture.Create<Setting>());
                db.Settings.Add(fixture.Create<Setting>());
                db.SaveChanges();
            }

            // Act
            var result = repository.IsEmptySetting();

            // Assert
            result.Should().BeFalse();
        }

        private CommonRepository GetRepository(EffortConnection connection)
        {
            var luckyDrawEntity = Substitute.For<ILuckyDrawEntity>();
            var dbContext = new LuckDrawDBContext(connection);
            luckyDrawEntity.GetDBContext().Returns(dbContext);

            return new CommonRepository(luckyDrawEntity);
        }

        private EffortConnection GetConnection()
        {
            return DbConnectionFactory.CreateTransient();
        }

        private LuckDrawDBContext GetDBContext(EffortConnection connection)
        {
            return new LuckDrawDBContext(connection);
        }
    }
}

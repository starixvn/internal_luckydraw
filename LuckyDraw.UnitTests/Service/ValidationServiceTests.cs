using FluentAssertions;
using LuckyDraw.Model;
using LuckyDraw.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using LuckyDraw.Resources;


namespace LuckyDraw.UnitTests.Service
{
    public class ValidationServiceTests
    {
        [Fact]
        public void ValidationService_Init_WithoutError()
        {
            // Act && Assert
            new ValidationService();
        }

        [Fact]
        public void ValidatePrizes_WhenValid_ReturnEmpty()
        {
            // Arrange
            var prizes = new List<ViewPrize>() {
                new ViewPrize { RoundIndex = 1, RoundName = "1"},
                new ViewPrize { RoundIndex = 1, RoundName = "1"},
                new ViewPrize { RoundIndex = 2, RoundName = "2"}
            };
            var service = new ValidationService();

            // Act
            var result = service.ValidatePrizes(prizes);

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public void ValidatePrizes_WhenDuplicateRoundName_ReturnInconsistantRoundNames()
        {
            // Arrange
            var prizes = new List<ViewPrize>() {
                new ViewPrize { RoundIndex = 1, RoundName = "1"},
                new ViewPrize { RoundIndex = 1, RoundName = "3"},
                new ViewPrize { RoundIndex = 2, RoundName = "2"}
            };
            var service = new ValidationService();

            // Act
            var result = service.ValidatePrizes(prizes);

            // Assert
            result.Should().Be("Inconsistant Round Index and Round Name");
        }

        [Fact]
        public void ValidatePrizes_WhenDuplicateRoundIndex_ReturnInconsistantRoundNames()
        {
            // Arrange
            var prizes = new List<ViewPrize>() {
                new ViewPrize { RoundIndex = 1, RoundName = "1"},
                new ViewPrize { RoundIndex = 2, RoundName = "1"},
                new ViewPrize { RoundIndex = 3, RoundName = "3"}
            };
            var service = new ValidationService();

            // Act
            var result = service.ValidatePrizes(prizes);

            // Assert
            result.Should().Be("Inconsistant Round Index and Round Name");
        }

        [Fact]
        public void ValidatePrizes_WhenMaxRoundSize_ReturnMaxRoundSizeError()
        {
            // Arrange
            var prizes = new List<ViewPrize>() {
                new ViewPrize { RoundIndex = 2, RoundName = "2"},
                new ViewPrize { RoundIndex = 1, RoundName = "1"},
                new ViewPrize { RoundIndex = 1, RoundName = "1"},
                new ViewPrize { RoundIndex = 1, RoundName = "1"},
                new ViewPrize { RoundIndex = 1, RoundName = "1"},
                new ViewPrize { RoundIndex = 1, RoundName = "1"},
                new ViewPrize { RoundIndex = 1, RoundName = "1"},
                new ViewPrize { RoundIndex = 1, RoundName = "1"},
                new ViewPrize { RoundIndex = 1, RoundName = "1"},
                new ViewPrize { RoundIndex = 1, RoundName = "1"},
                new ViewPrize { RoundIndex = 1, RoundName = "1"},
                new ViewPrize { RoundIndex = 1, RoundName = "1"}
            };
            var service = new ValidationService();

            // Act
            var result = service.ValidatePrizes(prizes);

            // Assert
            result.Should().Be("Each round must have no more than 10 items!");
        }            
    }
}

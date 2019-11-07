using LuckyDraw.Export;
using LuckyDraw.Model;
using LuckyDraw.Utilities;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LuckyDraw.UnitTests.Export
{
    public class ResultExporterTest
    {
        [Fact]
        public void ResultExporter_Init_WithoutError()
        {
            // Act && Assert
            new ResultExporter();
        }

        [Fact]
        public void Export_WhenCalled_WithoutError()
        {
            // Arrange
            var utils = Substitute.For<IUtils>();
            var exporter = new ResultExporter(utils);
            var data = new List<ViewResult>() {
                new ViewResult { PrizeIndex = 1, PrizeName = "Prize 1", RoundName = "Round 1", Random = "001" }
            };

            // Act && Assert
            exporter.Export(data, string.Empty);
        }
    }
}

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
        public void Export_WhenCalled_WithoutError()
        {
            // Arrange
            var utils = Substitute.For<IUtils>();
            var exporter = new ResultExporter(utils);
            var data = new List<ViewResult>();

            // Act && Assert
            exporter.Export(data, string.Empty);
        }
    }
}

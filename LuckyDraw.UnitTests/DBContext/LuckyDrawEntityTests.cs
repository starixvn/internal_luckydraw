using FluentAssertions;
using LuckyDraw.DBContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LuckyDraw.UnitTests.DBContext
{
    public class LuckyDrawEntityTests
    {
        [Fact]
        public void LuckDrawDBContext_WhenCalled_LuckDrawDBContext()
        {
            // Arrange
            var entity = new LuckyDrawEntity();

            // Act
            var result = entity.GetDBContext();

            // Assert
            result.Should().BeAssignableTo<LuckDrawDBContext>();
        }
    }
}

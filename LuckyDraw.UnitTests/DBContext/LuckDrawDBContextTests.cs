using LuckyDraw.DBContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LuckyDraw.UnitTests.DBContext
{
    public class LuckDrawDBContextTests
    {
        [Fact]
        public void LuckDrawDBContext_Init_WithoutError()
        {
            // Act && Assert
            new LuckDrawDBContext();
        }
    }
}

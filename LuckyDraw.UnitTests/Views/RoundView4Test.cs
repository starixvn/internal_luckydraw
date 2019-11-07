using LuckyDraw.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LuckyDraw.UnitTests.Views
{
    public class RoundView4Test
    {
        [WpfFact]
        public void RoundView4_Init_WithoutError()
        {
            // Act && Assert
            new RoundView4();
        }
    }
}

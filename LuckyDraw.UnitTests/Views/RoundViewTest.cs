using LuckyDraw.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LuckyDraw.UnitTests.Views
{
    public class RoundViewTest
    {
        [WpfFact]
        public void RoundView1_Init_WithoutError()
        {
            // Act && Assert
            new RoundView1();
        }

        [WpfFact]
        public void RoundView2_Init_WithoutError()
        {
            // Act && Assert
            new RoundView2();
        }

        [WpfFact]
        public void RoundView3_Init_WithoutError()
        {
            // Act && Assert
            new RoundView3();
        }

        [WpfFact]
        public void RoundView4_Init_WithoutError()
        {
            // Act && Assert
            new RoundView4();
        }

        [WpfFact]
        public void RoundView5_Init_WithoutError()
        {
            // Act && Assert
            new RoundView5();
        }
    }
}

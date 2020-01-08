using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LuckyDraw.UnitTests.Views
{
    public class ConfirmDialogTest
    {
        [WpfFact]
        public void ConfirmDialog_Init_WithoutError()
        {
            // Act && Assert
            new ConfirmDialog("abc");
        }
    }
}

using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Xunit;

namespace LuckyDraw.UnitTests.Views
{
    public class PasswordDialogTests
    {
        [WpfFact]
        public void Answer_WhenCalled_GetTextPassword()
        {
            // Arrange
            var dialog = new PasswordDialog();
            var passwordText = "abc";
            ((TextBox)dialog.FindName("txtPassword")).Text = passwordText;

            // Act
            var result = dialog.Answer;

            // Assert
            result.Should().Be(passwordText);
        }
    }
}

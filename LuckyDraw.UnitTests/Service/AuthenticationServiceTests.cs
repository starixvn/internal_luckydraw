using FluentAssertions;
using LuckyDraw.Configurations;
using LuckyDraw.Service;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LuckyDraw.UnitTests.Service
{
    public class AuthenticationServiceTests
    {
        [Fact]
        public void Authenticate_WhenPasswordIsWrong_ReturnFalse()
        {
            // Arrange
            var configs = Substitute.For<IConfigs>();
            var service = new AuthenticationService(configs);
            var password = "abc";
            var hashPassword = "abcd";
            configs.Password.Returns(hashPassword);

            // Act
            var result = service.Authenticate(password);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Authenticate_WhenPasswordIsTrue_ReturnTrue()
        {
            // Arrange
            var configs = Substitute.For<IConfigs>();
            var service = new AuthenticationService(configs);
            var password = "abc";
            var hashPassword = "ungWv48Bz+pBQUDeXa4iI7ADYaOWF3qctBD/YfIAFa0=";
            configs.Password.Returns(hashPassword);

            // Act
            var result = service.Authenticate(password);

            // Assert
            result.Should().BeTrue();
        }
    }
}

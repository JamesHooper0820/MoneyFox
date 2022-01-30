﻿using FluentAssertions;
using MoneyFox.Core._Pending_;
using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace MoneyFox.Core.Tests._Pending_
{
    [ExcludeFromCodeCoverage]
    public class SystemDateHelperTests
    {
        [Fact]
        public void ValueCorrectInitialized()
        {
            // Arrange
            // Act
            var systemDateHelper = new SystemDateHelper();
            // Assert
            systemDateHelper.Today.Should().Be(DateTime.Today);
        }
    }
}
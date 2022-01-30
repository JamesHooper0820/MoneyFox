﻿using MoneyFox.Core._Pending_.Common.QueryObjects;
using MoneyFox.Core.Aggregates;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;

namespace MoneyFox.Core.Tests._Pending_.QueryObjects
{
    [ExcludeFromCodeCoverage]
    public class AccountQueriesExtensionsTests
    {
        [Fact]
        public void AreActive()
        {
            // Arrange
            var accountQueryList = new List<Account> { new Account("Foo1"), new Account("Foo2"), new Account("absd") };

            accountQueryList[1].Deactivate();

            // Act
            List<Account> resultList = accountQueryList.AsQueryable().AreActive().ToList();

            // Assert
            Assert.Equal(2, resultList.Count);
            Assert.Equal("Foo1", resultList[0].Name);
            Assert.Equal("absd", resultList[1].Name);
        }
    }
}
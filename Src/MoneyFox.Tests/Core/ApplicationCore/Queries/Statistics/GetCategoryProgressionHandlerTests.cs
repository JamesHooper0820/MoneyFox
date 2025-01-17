﻿namespace MoneyFox.Tests.Core.ApplicationCore.Queries.Statistics
{

    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using FluentAssertions;
    using MoneyFox.Core.ApplicationCore.Domain.Aggregates.AccountAggregate;
    using MoneyFox.Core.ApplicationCore.Domain.Aggregates.CategoryAggregate;
    using MoneyFox.Core.ApplicationCore.Queries.Statistics;
    using MoneyFox.Infrastructure.Persistence;
    using TestFramework;
    using Xunit;

    [ExcludeFromCodeCoverage]
    [Collection("CultureCollection")]
    public class GetCategoryProgressionHandlerTests
    {
        private readonly AppDbContext context;
        private readonly GetCategoryProgressionHandler handler;

        public GetCategoryProgressionHandlerTests()
        {
            context = InMemoryAppDbContextFactory.Create();
            handler = new GetCategoryProgressionHandler(context);
        }

        [Fact]
        public async Task CalculateCorrectSums()
        {
            // Arrange
            var account = new Account("Foo1");
            var category = new Category("abcd");
            context.AddRange(
                new List<Payment>
                {
                    new Payment(
                        date: DateTime.Today,
                        amount: 60,
                        type: PaymentType.Income,
                        chargedAccount: account,
                        category: category),
                    new Payment(
                        date: DateTime.Today,
                        amount: 20,
                        type: PaymentType.Expense,
                        chargedAccount: account,
                        category: category),
                    new Payment(
                        date: DateTime.Today.AddMonths(-1),
                        amount: 50,
                        type: PaymentType.Expense,
                        chargedAccount: account,
                        category: category),
                    new Payment(
                        date: DateTime.Today.AddMonths(-2),
                        amount: 40,
                        type: PaymentType.Expense,
                        chargedAccount: account,
                        category: category)
                });

            context.Add(account);
            context.Add(category);
            await context.SaveChangesAsync();

            // Act
            var result = await handler.Handle(
                request: new GetCategoryProgressionQuery(categoryId: category.Id, startDate: DateTime.Today.AddYears(-1), endDate: DateTime.Today.AddDays(3)),
                cancellationToken: default);

            // Assert
            result[12].Value.Should().Be(40);
            result[11].Value.Should().Be(-50);
            result[10].Value.Should().Be(-40);
        }
    }

}

﻿namespace MoneyFox.Tests.TestFramework
{

    using MoneyFox.Core.ApplicationCore.Domain.Aggregates.AccountAggregate;
    using MoneyFox.Core.ApplicationCore.Domain.Aggregates.CategoryAggregate;

    internal static class TestPaymentDbFactory
    {
        internal static Payment CreateDbPayment(this TestData.IPayment payment)
        {
            // todo switch this around to have the account be the aggregate
            var chargedAccount = payment.ChargedAccount.CreateDbAccount();
            var targetAccount = payment.TargetAccount.CreateDbAccount();

            //TODO Handle category better
            var category = new Category(payment.CategoryName);

            //TODO Handle RecurringPayment better

            return new Payment(
                date: payment.Date,
                amount: payment.Amount,
                type: payment.Type,
                chargedAccount: chargedAccount,
                targetAccount: targetAccount,
                category: category,
                note: payment.Note);
        }
    }

}

﻿namespace MoneyFox.Core.ApplicationCore.Queries
{

    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Common.Helpers;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using MoneyFox.Core._Pending_.Common;
    using MoneyFox.Core._Pending_.Common.QueryObjects;
    using MoneyFox.Core.Common;
    using MoneyFox.Core.Common.Interfaces;

    public class GetMonthlyIncomeQuery : IRequest<decimal>
    {
        public class Handler : IRequestHandler<GetMonthlyIncomeQuery, decimal>
        {
            private readonly IAppDbContext appDbContext;
            private readonly ISystemDateHelper systemDateHelper;

            public Handler(IAppDbContext appDbContext, ISystemDateHelper systemDateHelper)
            {
                this.appDbContext = appDbContext;
                this.systemDateHelper = systemDateHelper;
            }

            public async Task<decimal> Handle(GetMonthlyIncomeQuery request, CancellationToken cancellationToken)
            {
                return (await appDbContext.Payments.HasDateLargerEqualsThan(HelperFunctions.GetFirstDayMonth(systemDateHelper))
                    .HasDateSmallerEqualsThan(HelperFunctions.GetEndOfMonth(systemDateHelper))
                    .IsIncome()
                    .Select(x => x.Amount)
                    .ToListAsync(cancellationToken)).Sum();
            }
        }
    }

}

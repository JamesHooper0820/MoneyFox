namespace MoneyFox.Infrastructure.DataAccess
{

    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;
    using System.Threading.Tasks;
    using Core.ApplicationCore.Domain.Aggregates.BudgetAggregate;
    using Microsoft.EntityFrameworkCore;
    using MoneyFox.Core.Common.Interfaces;

    internal sealed class BudgetRepository : IBudgetRepository
    {
        private readonly IAppDbContext appDbContext;

        public BudgetRepository(IAppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task AddAsync(Budget budget)
        {
            await appDbContext.Budgets.AddAsync(budget);
            await appDbContext.SaveChangesAsync();
        }

        public async Task<Budget> GetAsync(int budgetId)
        {
            return await appDbContext.Budgets.Where(b => b.Id == budgetId).SingleAsync();
        }

        public async Task<IReadOnlyCollection<Budget>> GetAsync()
        {
            return await appDbContext.Budgets.ToListAsync();
        }

        public async Task UpdateAsync(Budget budget)
        {
            await appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int testBudgetId)
        {
            var budget = await GetAsync(testBudgetId);
            appDbContext.Budgets.Remove(budget);
            await appDbContext.SaveChangesAsync();
        }
    }

}

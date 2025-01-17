﻿namespace MoneyFox.ViewModels.Budget
{

    using System.Linq;
    using System.Threading.Tasks;
    using CommunityToolkit.Mvvm.Input;
    using CommunityToolkit.Mvvm.Messaging;
    using Core.ApplicationCore.Queries.BudgetEntryLoading;
    using Core.ApplicationCore.UseCases.BudgetDeletion;
    using Core.ApplicationCore.UseCases.BudgetUpdate;
    using Core.Common.Extensions;
    using Core.Common.Interfaces;
    using Core.Common.Messages;
    using Core.Interfaces;
    using Core.Resources;
    using MediatR;

    internal sealed class EditBudgetViewModel : ModifyBudgetViewModel
    {
        private readonly ISender sender;
        private readonly INavigationService navigationService;
        private readonly IDialogService dialogService;

        public EditBudgetViewModel(ISender sender, INavigationService navigationService, IDialogService dialogService) : base(
            navigationService: navigationService)
        {
            this.sender = sender;
            this.navigationService = navigationService;
            this.dialogService = dialogService;
        }

        public AsyncRelayCommand<int> InitializeCommand => new AsyncRelayCommand<int>(InitializeAsync);

        public AsyncRelayCommand DeleteBudgetCommand => new AsyncRelayCommand(DeleteBudgetAsync);

        private async Task InitializeAsync(int budgetId)
        {
            var query = new LoadBudgetEntry.Query(budgetId: budgetId);
            var budgetData = await sender.Send(query);
            SelectedBudget.Id = budgetData.Id;
            SelectedBudget.Name = budgetData.Name;
            SelectedBudget.SpendingLimit = budgetData.SpendingLimit;
            SelectedCategories.Clear();
            SelectedCategories.AddRange(budgetData.Categories.Select(bc => new BudgetCategoryViewModel(categoryId: bc.Id, name: bc.Name)));
        }

        private async Task DeleteBudgetAsync()
        {
            if (await dialogService.ShowConfirmMessageAsync(title: Strings.DeleteTitle, message: Strings.DeleteBudgetConfirmationMessage))
            {
                var command = new DeleteBudget.Command(budgetId: SelectedBudget.Id);
                await sender.Send(command);
                Messenger.Send(new ReloadMessage());
                await navigationService.GoBackFromModal();
            }
        }

        protected override async Task SaveBudgetAsync()
        {
            var command = new UpdateBudget.Command(
                budgetId: SelectedBudget.Id,
                name: SelectedBudget.Name,
                spendingLimit: SelectedBudget.SpendingLimit,
                categories: SelectedCategories.Select(sc => sc.CategoryId).ToList());

            await sender.Send(command);
            Messenger.Send(new ReloadMessage());
            await navigationService.GoBackFromModal();
        }
    }

}

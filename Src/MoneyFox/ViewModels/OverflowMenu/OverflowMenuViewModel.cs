﻿namespace MoneyFox.ViewModels.OverflowMenu
{

    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CommunityToolkit.Mvvm.Input;
    using Core.Interfaces;
    using Core.Resources;
    using JetBrains.Annotations;
    using Views.About;
    using Views.Backup;
    using Views.Budget;
    using Views.Categories;
    using Views.Settings;

    [UsedImplicitly]
    internal sealed class OverflowMenuViewModel : BaseViewModel
    {
        private readonly INavigationService navigationService;

        public OverflowMenuViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public AsyncRelayCommand<OverflowItemViewModel> GoToSelectedItemCommand
            => new AsyncRelayCommand<OverflowItemViewModel>(async s => await GoToSelectedItem(s.Type));

        public List<OverflowItemViewModel> OverflowEntries
            => new List<OverflowItemViewModel>
            {
                new OverflowItemViewModel { IconGlyph = "savings", Name = Strings.BudgetsTitle, Type = OverflowMenuItemType.Budgets },
                new OverflowItemViewModel { IconGlyph = "label", Name = Strings.CategoriesTitle, Type = OverflowMenuItemType.Categories },
                new OverflowItemViewModel { IconGlyph = "backup", Name = Strings.BackupTitle, Type = OverflowMenuItemType.Backup },
                new OverflowItemViewModel { IconGlyph = "settings", Name = Strings.SettingsTitle, Type = OverflowMenuItemType.Settings },
                new OverflowItemViewModel { IconGlyph = "info", Name = Strings.AboutTitle, Type = OverflowMenuItemType.About }
            };

        private async Task GoToSelectedItem(OverflowMenuItemType menuType)
        {
            switch (menuType)
            {
                case OverflowMenuItemType.Budgets:
                    await navigationService.NavigateTo<BudgetListPage>();

                    break;
                case OverflowMenuItemType.Categories:
                    await navigationService.NavigateTo<CategoryListPage>();

                    break;
                case OverflowMenuItemType.Backup:
                    await navigationService.NavigateTo<BackupPage>();

                    break;
                case OverflowMenuItemType.Settings:
                    await navigationService.NavigateTo<SettingsPage>();

                    break;
                case OverflowMenuItemType.About:
                    await navigationService.NavigateTo<AboutPage>();

                    break;
            }
        }
    }

}

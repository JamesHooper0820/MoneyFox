﻿namespace MoneyFox.Views.Settings
{

    using ViewModels.Settings;

    public partial class SettingsPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            BindingContext = App.GetViewModel<SettingsViewModel>();
        }

        private SettingsViewModel ViewModel => (SettingsViewModel)BindingContext;

        protected override async void OnAppearing()
        {
            await ViewModel.InitializeAsync();
        }
    }

}

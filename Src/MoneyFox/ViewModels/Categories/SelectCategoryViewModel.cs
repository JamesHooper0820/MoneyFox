﻿namespace MoneyFox.ViewModels.Categories
{

    using AutoMapper;
    using CommunityToolkit.Mvvm.Input;
    using CommunityToolkit.Mvvm.Messaging;
    using Core.Common.Interfaces;
    using Core.Common.Messages;
    using MediatR;
    using Xamarin.Forms;

    internal sealed class SelectCategoryViewModel : CategoryListViewModel
    {
        public SelectCategoryViewModel(IMediator mediator, IMapper mapper, IDialogService dialogService) : base(
            mediator: mediator,
            mapper: mapper,
            dialogService: dialogService) { }

        public AsyncRelayCommand<CategoryViewModel> SelectCategoryCommand
            => new AsyncRelayCommand<CategoryViewModel>(
                async c =>
                {
                    var dataSet = new CategorySelectedDataSet(categoryId: c.Id, name: c.Name);
                    Messenger.Send(new CategorySelectedMessage(dataSet));
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                });
    }

}

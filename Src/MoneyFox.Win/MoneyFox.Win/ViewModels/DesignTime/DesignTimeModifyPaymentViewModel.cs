﻿namespace MoneyFox.Win.ViewModels.DesignTime;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Accounts;
using CommunityToolkit.Mvvm.Input;
using Core.ApplicationCore.Domain.Aggregates.AccountAggregate;
using Payments;

public class DesignTimeModifyPaymentViewModel : IModifyPaymentViewModel
{
    public bool IsTransfer { get; }

    public DateTime EndDate { get; } = DateTime.Now;

    public DateTime Date { get; }

    public RelayCommand DeleteCommand { get; } = null!;

    public PaymentRecurrence Recurrence { get; } = PaymentRecurrence.Monthly;

    public List<PaymentRecurrence> RecurrenceList
        => new()
        {
            PaymentRecurrence.Daily,
            PaymentRecurrence.DailyWithoutWeekend,
            PaymentRecurrence.Weekly,
            PaymentRecurrence.Biweekly,
            PaymentRecurrence.Monthly,
            PaymentRecurrence.Bimonthly,
            PaymentRecurrence.Quarterly,
            PaymentRecurrence.Biannually,
            PaymentRecurrence.Yearly
        };

    public PaymentViewModel SelectedPayment { get; } = null!;

    public string AmountString { get; } = "";

    public ObservableCollection<AccountViewModel> ChargedAccounts { get; } = null!;

    public ObservableCollection<AccountViewModel> TargetAccounts { get; } = null!;

    public string Title { get; } = "My Title";

    public string AccountHeader { get; } = "";

    public RelayCommand SelectedItemChangedCommand { get; } = null!;

    public RelayCommand SaveCommand { get; } = null!;

    public AsyncRelayCommand GoToSelectCategoryDialogCommand { get; } = null!;

    public RelayCommand CancelCommand { get; } = null!;

    public RelayCommand ResetCategoryCommand { get; } = null!;
}

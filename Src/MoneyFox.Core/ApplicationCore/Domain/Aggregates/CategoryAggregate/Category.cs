﻿namespace MoneyFox.Core.ApplicationCore.Domain.Aggregates.CategoryAggregate
{

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using AccountAggregate;
    using Dawn;
    using JetBrains.Annotations;
    using MoneyFox.Core.Common.Interfaces;

    public class Category : EntityBase, IAggregateRoot
    {
        [UsedImplicitly]
        private Category() { }

        public Category(string name, string? note = null, bool requireNote = false)
        {
            UpdateData(name: name, note: note, requireNote: requireNote);
        }

        public int Id
        {
            get;

            [UsedImplicitly]
            private set;
        }

        public string Name { get; private set; } = "";

        public string? Note { get; private set; }

        public bool RequireNote { get; private set; }

        public List<Payment> Payments
        {
            get;

            [UsedImplicitly]
            private set;
        } = new List<Payment>();

        public void UpdateData(string name, string? note = "", bool requireNote = false)
        {
            Guard.Argument(value: name, name: nameof(name)).NotWhiteSpace();
            Name = name;
            Note = note;
            RequireNote = requireNote;
        }
    }

}

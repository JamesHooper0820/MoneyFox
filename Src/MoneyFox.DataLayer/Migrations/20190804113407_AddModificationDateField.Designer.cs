﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MoneyFox.DataLayer;

namespace MoneyFox.DataLayer.Migrations
{
    [DbContext(typeof(EfCoreContext))]
    [Migration("20190804113407_AddModificationDateField")]
    partial class AddModificationDateField
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("MoneyFox.DataLayer.Entities.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<double>("CurrentBalance");

                    b.Property<bool>("IsExcluded");

                    b.Property<bool>("IsOverdrawn");

                    b.Property<DateTime>("ModificationDate");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Note");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("MoneyFox.DataLayer.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ModificationDate");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Note");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("MoneyFox.DataLayer.Entities.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Amount");

                    b.Property<int?>("CategoryId");

                    b.Property<int>("ChargedAccountId");

                    b.Property<DateTime>("CreationTime");

                    b.Property<DateTime>("Date");

                    b.Property<bool>("IsCleared");

                    b.Property<bool>("IsRecurring");

                    b.Property<DateTime>("ModificationDate");

                    b.Property<string>("Note");

                    b.Property<int?>("RecurringPaymentId");

                    b.Property<int?>("TargetAccountId");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ChargedAccountId");

                    b.HasIndex("RecurringPaymentId");

                    b.HasIndex("TargetAccountId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("MoneyFox.DataLayer.Entities.RecurringPayment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Amount");

                    b.Property<int?>("CategoryId");

                    b.Property<int?>("ChargedAccountId");

                    b.Property<DateTime?>("EndDate");

                    b.Property<bool>("IsEndless");

                    b.Property<DateTime>("ModificationDate");

                    b.Property<string>("Note");

                    b.Property<int>("Recurrence");

                    b.Property<DateTime>("StartDate");

                    b.Property<int?>("TargetAccountId");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ChargedAccountId");

                    b.HasIndex("TargetAccountId");

                    b.ToTable("RecurringPayments");
                });

            modelBuilder.Entity("MoneyFox.DataLayer.Entities.Payment", b =>
                {
                    b.HasOne("MoneyFox.DataLayer.Entities.Category", "Category")
                        .WithMany("Payments")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("MoneyFox.DataLayer.Entities.Account", "ChargedAccount")
                        .WithMany()
                        .HasForeignKey("ChargedAccountId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MoneyFox.DataLayer.Entities.RecurringPayment", "RecurringPayment")
                        .WithMany("RelatedPayments")
                        .HasForeignKey("RecurringPaymentId");

                    b.HasOne("MoneyFox.DataLayer.Entities.Account", "TargetAccount")
                        .WithMany()
                        .HasForeignKey("TargetAccountId");
                });

            modelBuilder.Entity("MoneyFox.DataLayer.Entities.RecurringPayment", b =>
                {
                    b.HasOne("MoneyFox.DataLayer.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("MoneyFox.DataLayer.Entities.Account", "ChargedAccount")
                        .WithMany()
                        .HasForeignKey("ChargedAccountId");

                    b.HasOne("MoneyFox.DataLayer.Entities.Account", "TargetAccount")
                        .WithMany()
                        .HasForeignKey("TargetAccountId");
                });
#pragma warning restore 612, 618
        }
    }
}

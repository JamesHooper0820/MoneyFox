﻿namespace MoneyFox.Infrastructure.Persistence.Configurations
{

    using Core.ApplicationCore.Domain.Aggregates.CategoryAggregate;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired();
            builder.HasIndex(c => c.Name);
            builder.HasMany(c => c.Payments).WithOne(t => t.Category!).HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.SetNull);
        }
    }

}

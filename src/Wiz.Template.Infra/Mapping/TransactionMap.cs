using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Wiz.Template.Domain.Entities;

namespace Wiz.Template.Infra.Mapping;

public class TransactionMap : IEntityTypeConfiguration<Transaction>
{
    private readonly IServiceProvider _serviceProvider;

    public TransactionMap(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transaction");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.MerchantId)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.Amount)
            .IsRequired();

        builder.Property(x => x.ExternalId)
            .IsRequired();

        builder.Property(x => x.PaymentMethodId)
            .IsRequired();

        builder.Property(x => x.CriadoEm)
            .IsRequired();

        builder.Property(x => x.ChangedAt)
            .IsRequired();
    }
}

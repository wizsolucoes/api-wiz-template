using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Wiz.Template.Domain.Entities;

namespace Wiz.Template.Infra.Mapping;

public class MerchantMap : IEntityTypeConfiguration<Merchant>
{
    private readonly IServiceProvider _serviceProvider;

    public MerchantMap(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void Configure(EntityTypeBuilder<Merchant> builder)
    {
        builder.ToTable("Merchant");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.ChangedAt)
            .IsRequired();
    }
}

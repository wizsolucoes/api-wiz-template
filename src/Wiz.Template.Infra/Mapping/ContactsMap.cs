using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Wiz.Template.Domain.Entities;

namespace Wiz.Template.Infra.Mapping;

public class ContactsMap : IEntityTypeConfiguration<Contacts>
{
    private readonly IServiceProvider _serviceProvider;

    public ContactsMap(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void Configure(EntityTypeBuilder<Contacts> builder)
    {
        builder.ToTable("ses_contatos");

        builder.Property(x => x.RazaoSocial)
            .HasColumnName("pesrazaosocial")
            .IsRequired();

        builder.Property(x => x.Cargo)
            .HasColumnName("dircargo")
            .IsRequired();

        builder.Property(x => x.CodigoFip)
            .HasColumnName("entcodigofip")
            .IsRequired();
    }
}

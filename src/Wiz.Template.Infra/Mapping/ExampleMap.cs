using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wiz.Template.Domain.Entities;

namespace Wiz.Template.Infra.Mapping
{
    public class ExampleMap : IEntityTypeConfiguration<Example>
    {
        public void Configure(EntityTypeBuilder<Example> builder)
        {
            builder.ToTable("Example", "dbo");

            builder.HasKey(x => x.Id);

            builder.OwnsOne(x => x.TemperatureC)
                .Property(x => x.Value)
                .HasColumnName("TemperatureC");

            builder.OwnsOne(x => x.TemperatureC).Ignore(x => x.IsValid);
            builder.OwnsOne(x => x.TemperatureC).Ignore(x => x.ValidationResult);
        }
    }
}

namespace Erialdev.BackOffice.Api.Infrastructure.Persistence.Configurations;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Erialdev.BackOffice.Api.Domain.Entites;
using Erialdev.BackOffice.Api.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
{
    public void Configure(EntityTypeBuilder<Resource> builder)
    {
        builder.ToTable("backoffice_resources");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Code).HasConversion(v => v.Value, v => new Code(v)).IsRequired().HasMaxLength(20);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
    }
}
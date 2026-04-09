using Erialdev.BackOffice.Api.Domain.Entites;
using Erialdev.BackOffice.Api.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Erialdev.BackOffice.Api.Infrastructure.Persistence.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("backoffice_roles");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Code).HasConversion(v => v.Value, v => new Code(v)).IsRequired().HasMaxLength(20);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
    }
}
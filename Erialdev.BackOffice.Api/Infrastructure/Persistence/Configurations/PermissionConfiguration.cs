namespace Erialdev.BackOffice.Api.Infrastructure.Persistence.Configurations;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Erialdev.BackOffice.Api.Domain.Entites;
using Erialdev.BackOffice.Api.Domain.ValueObjects.Permission;
using Erialdev.BackOffice.Api.Domain.ValueObjects;

using Microsoft.EntityFrameworkCore;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("permissions");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Code).HasConversion(v => v.Value, v => new Code(v)).IsRequired().HasMaxLength(30);
        builder.Property(x => x.Action).HasConversion(v => v.Value, v => new PermissionAction(v)).IsRequired().HasMaxLength(30);

        builder.HasOne(x => x.Resource).WithMany().HasForeignKey("resourceid");


    }
}

namespace Erialdev.BackOffice.Api.Infrastructure.Persistence.Configurations;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Erialdev.BackOffice.Api.Domain.Entites;
using Erialdev.BackOffice.Api.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("backoffice_rolepermissions");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Code).HasConversion(v => v.Value, v => new Code(v)).IsRequired().HasMaxLength(30);

        builder.HasOne(x => x.Role)
        .WithMany()
        .HasForeignKey("roleid");

        builder.HasOne(x => x.Permission)
        .WithMany()
        .HasForeignKey("permissionid");



    }
}

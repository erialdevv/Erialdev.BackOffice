using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Erialdev.BackOffice.Api.Domain.Entites;
using Erialdev.BackOffice.Api.Domain.ValueObjects;
namespace Erialdev.BackOffice.Api.Infrastructure.Persistence.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("backoffice_user_role");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Code).HasConversion(v => v.Value, v => new Code(v)).IsRequired().HasMaxLength(20);
        builder.Property(x => x.Code).HasConversion(v => v.Value, v => new Code(v)).IsRequired().HasMaxLength(20);


        builder.HasOne(x => x.User)
            .WithMany(x => x.UserRoles)
            .HasForeignKey("userid");

        builder.HasOne(x => x.Role)
            .WithMany()
            .HasForeignKey("roleid");

    }
}

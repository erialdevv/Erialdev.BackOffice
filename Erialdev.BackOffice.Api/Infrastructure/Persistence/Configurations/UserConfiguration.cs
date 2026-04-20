using Erialdev.BackOffice.Api.Domain.Entites;
using Erialdev.BackOffice.Api.Domain.ValueObjects.User;
using Erialdev.BackOffice.Api.Domain.ValueObjects;
using Erialdev.BackOffice.Api.Domain.ValueObjects.Jwt;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Erialdev.BackOffice.Api.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("backoffice_users");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Code).HasConversion(v => v.Value, v => new Code(v)).IsRequired().HasMaxLength(30);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(30);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(30);
        builder.Property(x => x.UserName).HasConversion(v => v.Value, v => new UserName(v)).IsRequired().HasMaxLength(30);
        builder.Property(x => x.Password).HasConversion(v => v.Value, v => new Password(v)).IsRequired().HasMaxLength(100);

        builder.HasMany(x => x.RefreshTokens)
            .WithOne()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}

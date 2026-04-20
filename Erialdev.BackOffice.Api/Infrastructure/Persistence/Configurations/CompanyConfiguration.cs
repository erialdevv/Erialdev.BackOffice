namespace Erialdev.BackOffice.Api.Infrastructure.Persistence.Configurations;

using Erialdev.BackOffice.Api.Domain.Entites;
using Erialdev.BackOffice.Api.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("backoffice_companies");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Code)
            .HasConversion(v => v.Value, v => new Code(v))
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.CreateDate).IsRequired();
        builder.Property(x => x.CreateAt).IsRequired();
        builder.Property(x => x.EditAt);
        builder.Property(x => x.EditDate);
        builder.Property(x => x.CancelAt);
        builder.Property(x => x.CancelDate);
        builder.Property(x => x.IsCanceled).IsRequired();
        builder.Property(x => x.Pcid);
    }
}

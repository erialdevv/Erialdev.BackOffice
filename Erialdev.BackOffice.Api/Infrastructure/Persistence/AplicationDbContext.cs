using Microsoft.EntityFrameworkCore;
using Erialdev.BackOffice.Api.Domain.Entites;
using Erialdev.BackOffice.Api.Infrastructure.Services;

namespace Erialdev.BackOffice.Api.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    private readonly ITenantContext _tenant;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ITenantContext tenant)
       : base(options)
    {
        _tenant = tenant;
    }

    public DbSet<Company> Companies { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<Resource> Resources { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        modelBuilder.Entity<User>().HasQueryFilter(x => x.CompanyId == _tenant.CompanyId);
        modelBuilder.Entity<Role>().HasQueryFilter(x => x.CompanyId == _tenant.CompanyId);
        modelBuilder.Entity<Resource>().HasQueryFilter(x => x.CompanyId == _tenant.CompanyId);
        modelBuilder.Entity<Permission>().HasQueryFilter(x => x.CompanyId == _tenant.CompanyId);
        modelBuilder.Entity<UserRole>().HasQueryFilter(x => x.CompanyId == _tenant.CompanyId);
        modelBuilder.Entity<RolePermission>().HasQueryFilter(x => x.CompanyId == _tenant.CompanyId);
        modelBuilder.Entity<RefreshToken>().HasQueryFilter(x => x.CompanyId == _tenant.CompanyId);
    }
}

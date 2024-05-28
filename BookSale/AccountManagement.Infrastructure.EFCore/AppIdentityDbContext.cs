using AccountManagement.Domain.Entities.RoleAgg;
using AccountManagement.Domain.Entities.UserAgg;
using AccountManagement.Infrastructure.EFCore.Configurations;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EFCore;

public class AppIdentityDbContext : DbContext
{
    public DbSet<ApplicationUser> Accounts { get; set; }
    public DbSet<ApplicationRole> Roles { get; set; }
    public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var assembly = typeof(AccountConfig).Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        base.OnModelCreating(modelBuilder);
    }
}

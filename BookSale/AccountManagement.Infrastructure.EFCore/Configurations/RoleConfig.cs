using AccountManagement.Domain.Entities.RoleAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EFCore.Configurations;

public class RoleConfig : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.ToTable("Roles");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(100).IsRequired(true);
        builder.OwnsMany(x => x.Permissions, NavigationBuilder =>
        {
            NavigationBuilder.HasKey(x => x.Id);
            NavigationBuilder.ToTable("RolePermissions");
            NavigationBuilder.Ignore(x => x.Name);
            NavigationBuilder.WithOwner(x => x.Role);
        });
    }
}

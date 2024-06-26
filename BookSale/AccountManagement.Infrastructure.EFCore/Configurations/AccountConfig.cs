﻿using AccountManagement.Domain.Entities.UserAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EFCore.Configurations;

public class AccountConfig : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.FullName).HasMaxLength(100);
        builder.Property(x => x.UserName).HasMaxLength(100);
        builder.Property(x => x.Password).HasMaxLength(1000);
        builder.Property(x => x.ProfilePhoto).HasMaxLength(500);
        builder.Property(x => x.PhoneNumber).HasMaxLength(20);

        //builder.HasOne(x => x.Role).WithMany(x => x.Accounts).HasForeignKey(x => x.RoleId);
    }
}


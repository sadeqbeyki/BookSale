﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Infrastructure.EFCore.Mapping
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(255);
            builder.Property(x => x.Code).HasMaxLength(15);
            builder.Property(x=>x.ShortDescription).HasMaxLength(500);
            builder.Property(x=>x.Description).HasMaxLength(1000).IsRequired(false);
            builder.Property(x => x.Picture).HasMaxLength(1000).IsRequired(false);
            builder.Property(x=>x.PictureAlt).HasMaxLength(255).IsRequired(false);
            builder.Property(x=>x.PictureTitle).HasMaxLength(500).IsRequired(false);
            builder.Property(x=>x.Keywords).HasMaxLength(100);
            builder.Property(x=>x.MetaDescription).HasMaxLength(150);
            builder.Property(x=>x.Slug).HasMaxLength(500);
            builder.HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(x=>x.CategoryId);  
        }
    }
}

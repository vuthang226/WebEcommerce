using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Data.Entities;
using Web.Data.Enum;

namespace Web.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.PriceSaleOff).HasDefaultValue(-1);
            builder.Property(x => x.Stock).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.PhoneContaxt).IsRequired();
            builder.Property(x => x.ProductStatus).HasDefaultValue(Status.InActive);
            builder.HasOne(x => x.AppUser).WithMany(x => x.Products).HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(x => x.CategoryId);




        }
    }
}

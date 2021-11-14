using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Data.Entities;
using Web.Data.Enum;

namespace Web.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.SortOrder).HasDefaultValue(1);
            builder.Property(x => x.IsShowOnHome).HasDefaultValue(true);
            builder.Property(x => x.CategoryUrl).IsRequired();
            builder.Property(x => x.Id).UseMySqlIdentityColumn();
            builder.Property(x => x.Status).HasDefaultValue(Status.Active);
        }
    }
}

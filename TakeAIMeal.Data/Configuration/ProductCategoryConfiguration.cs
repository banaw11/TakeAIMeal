// <auto-generated>
// ReSharper disable All

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TakeAIMeal.Data
{
    // ProductCategories
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable("ProductCategories", "dbo");
            builder.HasKey(x => x.Id).HasName("PK__ProductC__3214EC072357BC36").IsClustered();

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(x => x.Name).HasColumnName(@"Name").HasColumnType("varchar(30)").IsRequired().IsUnicode(false).HasMaxLength(30);
            builder.Property(x => x.NormalizedName).HasColumnName(@"NormalizedName").HasColumnType("varchar(30)").IsRequired().IsUnicode(false).HasMaxLength(30);
        }
    }

}
// </auto-generated>

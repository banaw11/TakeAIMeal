// <auto-generated>
// ReSharper disable All

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TakeAIMeal.Data
{
    // Recipes
    public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.ToTable("Recipes", "dbo");
            builder.HasKey(x => x.Id).HasName("PK__Receipes__3214EC07E8BBBFA7").IsClustered();

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(x => x.UserId).HasColumnName(@"UserId").HasColumnType("int").IsRequired();
            builder.Property(x => x.MealType).HasColumnName(@"MealType").HasColumnType("int").IsRequired();
            builder.Property(x => x.RecipeIdentifier).HasColumnName(@"RecipeIdentifier").HasColumnType("uniqueidentifier").IsRequired();
        }
    }

}
// </auto-generated>
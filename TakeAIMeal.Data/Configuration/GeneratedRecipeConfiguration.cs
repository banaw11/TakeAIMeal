// <auto-generated>
// ReSharper disable All

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TakeAIMeal.Data
{
    // ****************************************************************************************************
    // This is not a commercial licence, therefore only a few tables/views/stored procedures are generated.
    // ****************************************************************************************************

    // GeneratedRecipes
    public class GeneratedRecipeConfiguration : IEntityTypeConfiguration<GeneratedRecipe>
    {
        public void Configure(EntityTypeBuilder<GeneratedRecipe> builder)
        {
            builder.ToTable("GeneratedRecipes", "dbo");
            builder.HasKey(x => x.Id).HasName("PK__Generate__3214EC076DE96850").IsClustered();

            builder.Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(x => x.Date).HasColumnName(@"Date").HasColumnType("datetime").IsRequired(false);
            builder.Property(x => x.MealType).HasColumnName(@"MealType").HasColumnType("int").IsRequired();
            builder.Property(x => x.UserId).HasColumnName(@"UserId").HasColumnType("int").IsRequired(false);
        }
    }

}
// </auto-generated>
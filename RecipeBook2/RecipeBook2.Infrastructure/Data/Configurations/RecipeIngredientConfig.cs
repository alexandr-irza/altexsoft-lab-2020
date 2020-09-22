using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeBook2.Core.Entities;

namespace RecipeBook2.Infrastructure.Data.Configurations
{
    class RecipeIngredientConfig : IEntityTypeConfiguration<RecipeIngredient>
    {
        public void Configure(EntityTypeBuilder<RecipeIngredient> builder)
        {
            builder.HasKey(x => new { x.IngredientId, x.RecipeId });
            builder.Ignore("Id");
        }
    }
}

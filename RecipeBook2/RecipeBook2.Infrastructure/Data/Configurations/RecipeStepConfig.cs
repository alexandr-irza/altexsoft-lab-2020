using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecipeBook2.Core.Entities;

namespace RecipeBook2.Infrastructure.Data.Configurations
{
    class RecipeStepConfig : IEntityTypeConfiguration<RecipeStep>
    {
        public void Configure(EntityTypeBuilder<RecipeStep> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}

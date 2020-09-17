using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecipeBook2.Core.Entities;

namespace RecipeBook2.Infrastructure.Data
{
    public class RecipeBookContext: DbContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Ingredient> Ingredients { get; set; }
        DbSet<Recipe> Recipes { get; set; }
        DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        DbSet<RecipeStep> RecipeSteps { get; set; }

        public RecipeBookContext(DbContextOptions<RecipeBookContext> options): base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecipeIngredient>().ToTable("RecipeIngredients").HasKey(x => new { x.IngredientId, x.RecipeId });
            modelBuilder.Entity<RecipeIngredient>().Ignore("Id");
        }
    }
}

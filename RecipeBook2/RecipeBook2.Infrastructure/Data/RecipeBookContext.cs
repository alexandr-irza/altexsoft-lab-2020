using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RecipeBook2.Core.Entities;
using System.IO;

namespace RecipeBook2.Infrastructure.Data
{
    public class RecipeBookContext: DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<RecipeStep> RecipeSteps { get; set; }

        public RecipeBookContext(DbContextOptions options): base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecipeIngredient>().ToTable("RecipeIngredients").HasKey(x => new { x.IngredientId, x.RecipeId });
            modelBuilder.Entity<RecipeIngredient>().Ignore("Id");
        }
    }
}

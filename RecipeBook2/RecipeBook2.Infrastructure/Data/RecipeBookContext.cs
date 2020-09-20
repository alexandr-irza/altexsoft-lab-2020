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

        public RecipeBookContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            //AIrza As we use this library with main program, we have access to appsettings file
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = builder.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecipeIngredient>().ToTable("RecipeIngredients").HasKey(x => new { x.IngredientId, x.RecipeId });
            modelBuilder.Entity<RecipeIngredient>().Ignore("Id");
        }
    }
}

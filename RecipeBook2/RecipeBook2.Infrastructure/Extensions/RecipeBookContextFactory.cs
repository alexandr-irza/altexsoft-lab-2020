using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using RecipeBook2.Infrastructure.Data;
using System.IO;

namespace RecipeBook2.Infrastructure.Extensions
{
    public class RecipeBookContextFactory : IDesignTimeDbContextFactory<RecipeBookContext>
    {
        public RecipeBookContext CreateDbContext(string[] args)
        {
            // Build config
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../RecipeBook2.Cmd"))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
                
            var optionsBuilder = new DbContextOptionsBuilder<RecipeBookContext>();

            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));

            return new RecipeBookContext(optionsBuilder.Options);
        }
    }
}

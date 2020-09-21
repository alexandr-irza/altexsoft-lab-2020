using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using RecipeBook2.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeBook2.Infrastructure.Extensions
{
    public class RecipeBookContextFactory : IDesignTimeDbContextFactory<RecipeBookContext>
    {
        public RecipeBookContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RecipeBookContext>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=RecipeBook;Trusted_Connection=True;");

            return new RecipeBookContext(optionsBuilder.Options);
        }
    }
}

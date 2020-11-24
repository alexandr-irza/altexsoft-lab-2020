using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RecipeBook2.Core.Controllers;
using RecipeBook2.Core.Interfaces;
using RecipeBook2.Infrastructure.Data;

namespace RecipeBook2.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<RecipeBookContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IIngredientRepository, IngredientRepository>();
            services.AddScoped<IRecipeRepository, RecipeRepository>();
            services.AddScoped<IRecipeIngredientRepository, RecipeIngredientRepository>();
            services.AddScoped<IRecipeStepRepository, RecipeStepRepository>();

            services.AddScoped<NavigationController>();
            services.AddScoped<RecipeController>();
            services.AddScoped<CategoryController>();
            services.AddScoped<IngredientController>();

            return services;
        }
    }
}

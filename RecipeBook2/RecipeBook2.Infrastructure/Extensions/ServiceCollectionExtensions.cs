using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RecipeBook2.Core.Interfaces;
using RecipeBook2.Infrastructure.Data;
using RecipeBook2.Infrastructure.Repositories;

namespace RecipeBook2.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
        public static IServiceCollection AddContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<RecipeBookContext>(options =>
                options.UseSqlServer(connectionString)
            ); 

            return services;
        }
    }
}

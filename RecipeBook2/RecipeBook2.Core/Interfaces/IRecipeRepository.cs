using RecipeBook2.Core.Entities;
using RecipeBook2.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeBook2.Core.Interfaces
{
    public interface IRecipeRepository : IRepository<Recipe>
    {
        Task<List<Recipe>> GetRecipesByCategoryIdAsync(int? categoryId);
    }
}

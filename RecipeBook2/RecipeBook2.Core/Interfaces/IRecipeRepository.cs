using RecipeBook2.Core.Entities;
using RecipeBook2.SharedKernel;
using System.Collections.Generic;

namespace RecipeBook2.Core.Interfaces
{
    public interface IRecipeRepository : IRepository<Recipe>
    {
        IEnumerable<Recipe> GetRecipesByCategoryId(int? categoryId);
    }
}

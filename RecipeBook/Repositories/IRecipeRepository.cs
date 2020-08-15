using RecipeBook.Models;
using System.Collections.Generic;

namespace RecipeBook.Repositories
{
    public interface IRecipeRepository : IRepository<Recipe>
    {
        IEnumerable<Recipe> GetRecipesByCategoryId(string categoryId);
    }
}

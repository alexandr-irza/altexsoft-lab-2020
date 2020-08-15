using RecipeBook.Models;
using System.Collections.Generic;

namespace RecipeBook.Repositories
{
    public interface IRecipeIngredientRepository : IRepository<RecipeIngredient>
    {
        IEnumerable<RecipeIngredient> GetRecipeIngredients(string recipeId);
    }
}

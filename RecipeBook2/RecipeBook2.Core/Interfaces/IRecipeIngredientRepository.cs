using RecipeBook2.Core.Entities;
using RecipeBook2.SharedKernel;
using System.Collections.Generic;

namespace RecipeBook2.Core.Interfaces
{
    public interface IRecipeIngredientRepository : IRepository<RecipeIngredient>
    {
        IEnumerable<RecipeIngredient> GetRecipeIngredients(int recipeId);
    }
}

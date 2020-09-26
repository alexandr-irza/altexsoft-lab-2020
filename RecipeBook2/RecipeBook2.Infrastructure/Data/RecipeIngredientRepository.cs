using RecipeBook2.Core.Entities;
using RecipeBook2.Core.Interfaces;
using System.Collections.Generic;

namespace RecipeBook2.Infrastructure.Data
{
    public class RecipeIngredientRepository : BaseRepository<RecipeIngredient>, IRecipeIngredientRepository
    {
        public RecipeIngredientRepository(RecipeBookContext context) : base(context)
        {
        }

        public IEnumerable<RecipeIngredient> GetRecipeIngredients(int recipeId)
        {
            return Find(x => x.RecipeId == recipeId);
        }
    }
}

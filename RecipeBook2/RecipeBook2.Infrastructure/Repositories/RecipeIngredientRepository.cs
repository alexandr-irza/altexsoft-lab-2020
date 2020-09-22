using RecipeBook2.Core.Entities;
using RecipeBook2.Core.Interfaces;
using RecipeBook2.Infrastructure.Data;
using System.Collections.Generic;

namespace RecipeBook2.Infrastructure.Repositories
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

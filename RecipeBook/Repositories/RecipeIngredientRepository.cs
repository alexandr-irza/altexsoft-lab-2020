using RecipeBook.Data;
using RecipeBook.Models;
using System.Collections.Generic;

namespace RecipeBook.Repositories
{
    public class RecipeIngredientRepository : Repository<RecipeIngredient>, IRecipeIngredientRepository
    {
        public RecipeIngredientRepository(IDataContext context) : base(context)
        {
        }

        public override RecipeIngredient Get(string id)
        {
            return null;
        }

        public IEnumerable<RecipeIngredient> GetRecipeIngredients(string recipeId)
        {
            return Find(x => x.RecipeId == recipeId);
        }
    }
}

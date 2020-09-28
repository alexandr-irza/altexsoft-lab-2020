using RecipeBook2.Core.Entities;
using RecipeBook2.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeBook2.Infrastructure.Data
{
    public class RecipeIngredientRepository : BaseRepository<RecipeIngredient>, IRecipeIngredientRepository
    {
        public RecipeIngredientRepository(RecipeBookContext context) : base(context)
        {
        }

        public Task<List<RecipeIngredient>> GetRecipeIngredients(int recipeId)
        {
            return FindAsync(x => x.RecipeId == recipeId);
        }
    }
}

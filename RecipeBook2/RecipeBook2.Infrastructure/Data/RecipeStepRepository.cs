using RecipeBook2.Core.Entities;
using RecipeBook2.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeBook2.Infrastructure.Data
{
    public class RecipeStepRepository: BaseRepository<RecipeStep>, IRecipeStepRepository
    {
        public RecipeStepRepository(RecipeBookContext context): base(context)
        {

        }

        public async Task<List<RecipeStep>> GetRecipeStepsAsync(int recipeId)
        {
            return await FindAsync(x => x.RecipeId == recipeId);
        }
    }
}

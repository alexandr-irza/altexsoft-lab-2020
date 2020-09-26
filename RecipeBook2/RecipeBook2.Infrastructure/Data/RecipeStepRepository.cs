using RecipeBook2.Core.Entities;
using RecipeBook2.Core.Interfaces;
using System.Collections.Generic;

namespace RecipeBook2.Infrastructure.Data
{
    public class RecipeStepRepository: BaseRepository<RecipeStep>, IRecipeStepRepository
    {
        public RecipeStepRepository(RecipeBookContext context): base(context)
        {

        }

        public IEnumerable<RecipeStep> GetRecipeSteps(int recipeId)
        {
            return Find(x => x.RecipeId == recipeId);
        }
    }
}

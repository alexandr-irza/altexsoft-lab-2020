using RecipeBook2.Core.Entities;
using RecipeBook2.SharedKernel;
using System.Collections.Generic;

namespace RecipeBook2.Core.Interfaces
{
    public interface IRecipeStepRepository: IRepository<RecipeStep>
    {
        IEnumerable<RecipeStep> GetRecipeSteps(int recipeId);
    }
}

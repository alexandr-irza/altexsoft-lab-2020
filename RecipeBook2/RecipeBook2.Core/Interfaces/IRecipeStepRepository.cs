using RecipeBook2.Core.Entities;
using RecipeBook2.SharedKernel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeBook2.Core.Interfaces
{
    public interface IRecipeStepRepository: IRepository<RecipeStep>
    {
        Task<List<RecipeStep>> GetRecipeStepsAsync(int recipeId);
    }
}

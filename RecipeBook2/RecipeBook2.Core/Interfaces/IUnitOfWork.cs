using System.Threading.Tasks;

namespace RecipeBook2.Core.Interfaces
{
    public interface IUnitOfWork
    {
        ICategoryRepository Categories { get; }
        IIngredientRepository Ingredients { get; }
        IRecipeRepository Recipes { get; }
        IRecipeIngredientRepository RecipeIngredients { get; }
        IRecipeStepRepository RecipeSteps { get; }
        Task SaveChangesAsync();
    }
}

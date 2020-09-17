
namespace RecipeBook2.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IRecipeRepository Recipes { get; }
        IIngredientRepository Ingredients { get; }
        IRecipeIngredientRepository RecipeIngredients { get; }
        ICategoryRepository Categories { get; }
        void Save();
    }
}

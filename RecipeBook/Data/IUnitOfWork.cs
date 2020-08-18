using RecipeBook.Repositories;

namespace RecipeBook.Data
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

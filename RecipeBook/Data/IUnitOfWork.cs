using RecipeBook.Repositories;

namespace RecipeBook.Data
{
    interface IUnitOfWork
    {
        public IRecipeRepository Recipes { get; }
        public IIngredientRepository Ingredients { get; }
        public IRecipeIngredientRepository RecipeIngredients { get; }
        public ICategoryRepository Categories { get; }

        void Save();
    }
}

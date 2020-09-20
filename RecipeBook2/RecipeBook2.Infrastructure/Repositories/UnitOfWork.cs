using RecipeBook2.Core.Interfaces;
using RecipeBook2.Infrastructure.Data;
using System.Linq;

namespace RecipeBook2.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository Categories { get; private set; }
        public IIngredientRepository Ingredients { get; private set; }
        public IRecipeRepository Recipes { get; private set; }
        public IRecipeIngredientRepository RecipeIngredients { get; private set; }
        public IRecipeStepRepository RecipeSteps { get; private set; }

        public UnitOfWork(RecipeBookContext context)
        {
            Categories = new CategoryRepository(context);
            Recipes = new RecipeRepository(context);
            Ingredients = new IngredientRepository(context);
            RecipeIngredients = new RecipeIngredientRepository(context);
            RecipeSteps = new RecipeStepRepository(context);
        }

        public void Save()
        {
            Categories.Save();
            Ingredients.Save();
            RecipeIngredients.Save();
            Recipes.Save();
        }
    }
}

using RecipeBook2.Core.Interfaces;
using RecipeBook2.Infrastructure.Data;
using System.Linq;

namespace RecipeBook2.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRecipeRepository Recipes { get; private set; }

        public IIngredientRepository Ingredients { get; private set; }

        public IRecipeIngredientRepository RecipeIngredients { get; private set; }
        public IRecipeStepRepository RecipeSteps { get; private set; }

        public ICategoryRepository Categories { get; private set; }

        public UnitOfWork(RecipeBookContext context)
        {
            Categories = new CategoryRepository(context);
            foreach (var category in Categories.GetAll())
                category.Parent = Categories.SingleOrDefault(y => y.Id == category.ParentId);

            Recipes = new RecipeRepository(context);
            Ingredients = new IngredientRepository(context);
            RecipeIngredients = new RecipeIngredientRepository(context);
            RecipeSteps = new RecipeStepRepository(context);

            foreach (var recipe in Recipes.GetAll())
            {
                recipe.Ingredients = RecipeIngredients.GetRecipeIngredients(recipe.Id).ToList();
                recipe.Ingredients.ForEach(x => x.Ingredient = Ingredients.SingleOrDefault(y => y.Id == x.IngredientId));
                recipe.Directions = RecipeSteps.GetRecipeSteps(recipe.Id).ToList();
            }
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

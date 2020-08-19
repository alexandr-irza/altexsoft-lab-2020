using RecipeBook.Repositories;
using System.Linq;

namespace RecipeBook.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRecipeRepository Recipes { get; private set; }

        public IIngredientRepository Ingredients { get; private set; }

        public IRecipeIngredientRepository RecipeIngredients { get; private set; }

        public ICategoryRepository Categories { get; private set; }

        public UnitOfWork()
        {
            var context = new JsonFileContext();
            Categories = new CategoryRepository(context);
            foreach (var category in Categories.GetAll())
                category.Parent = Categories.SingleOrDefault(y => y.Id == category.ParentId);

            Recipes = new RecipeRepository(context);
            Ingredients = new IngredientRepository(context);
            RecipeIngredients = new RecipeIngredientRepository(context);

            foreach (var recipe in Recipes.GetAll())
            {
                recipe.Ingredients = RecipeIngredients.GetRecipeIngredients(recipe.Id).ToList();
                recipe.Ingredients.ForEach(x => x.Ingredient = Ingredients.SingleOrDefault(y => y.Id == x.IngredientId));
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

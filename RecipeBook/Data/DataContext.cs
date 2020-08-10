using RecipeBook.Models;
using System.Collections.Generic;
using System.Linq;

namespace RecipeBook.Data
{
    public class DataContext
    {
        public List<Recipe> Recipes { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<RecipeIngredient> RecipeIngredients { get; set; }
        public List<Category> Categories { get; set; }
        private string CategoriesFileName { get => "categories.json"; }
        private string RecipesFileName { get => "recipes.json"; }
        private string IngredientsFileName { get => "ingredients.json"; }
        private string RecipeIngredientsFileName { get => "recipe_ingredients.json"; }
        public DataContext()
        {
            Categories = DataProcessor.LoadFromFile<Category>(CategoriesFileName);
            Categories.ForEach(x => x.Parent = Categories.SingleOrDefault(y => y.Id == x.ParentId));

            Recipes = DataProcessor.LoadFromFile<Recipe>(RecipesFileName);
            Ingredients = DataProcessor.LoadFromFile<Ingredient>(IngredientsFileName);
            RecipeIngredients = DataProcessor.LoadFromFile<RecipeIngredient>(RecipeIngredientsFileName);

            foreach (var recipe in Recipes)
            {
                recipe.Ingredients = RecipeIngredients.Where(y => y.RecipeId == recipe.Id).ToList();
                recipe.Ingredients.ForEach(x => x.Ingredient = Ingredients.Single(y => y.Id == x.IngredientId));
            }
        }

        public void SaveCategories()
        {
            DataProcessor.SaveToFile(Categories, CategoriesFileName);
        }

        public void SaveIngredients()
        {
            DataProcessor.SaveToFile(Ingredients, IngredientsFileName);
        }

        public void SaveRecipes()
        {
            DataProcessor.SaveToFile(Recipes, RecipesFileName);
            DataProcessor.SaveToFile(RecipeIngredients, RecipeIngredientsFileName);
        }

        private int NextId<T>(List<T> list) where T : BaseModel
        {
            if (list.Count == 0)
                return 1;
            else
                return list.Max(x => int.Parse(x.Id)) + 1;
        }

        public int NextCategoryId()
        {
            return NextId(Categories);
        }

        public int NextRecipeId()
        {
            return NextId(Recipes);
        }

        public int NextIngredientId()
        {
            return NextId(Ingredients);
        }
    }
}

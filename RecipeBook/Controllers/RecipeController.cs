using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RecipeBook.Data;
using RecipeBook.Models;

namespace RecipeBook.Controllers
{
    public class RecipeController : CommonController
    {
        public RecipeController(DataContext data) : base(data)
        {
        }

        public List<Recipe> GetRecipes(string categoryId = null)
        {
            return Data.Recipes.Where(x => x.CategoryId == categoryId).ToList();
        }

        public void CreateRecipe(Recipe recipe)
        {
            var item = Data.Recipes.ToList().Find(x => x.Name == recipe.Name);
            if (item != null)
                throw new Exception($"Recipe {item.Name} ({item.CategoryId}) already exists");

            Data.Recipes.Add(recipe);
        }

        public void RemoveRecipe(Recipe recipe)
        {
            var item = Data.Recipes.ToList().Find(x => x.Id == recipe.Id);
            if (item == null)
                throw new Exception($"Recipe {recipe.Name} has not been found");

            Data.Recipes.Remove(item);
        }
    }
}

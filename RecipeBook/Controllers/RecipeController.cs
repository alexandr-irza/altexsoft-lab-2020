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
        public Recipe Recipe { get; }
        public RecipeController(DataContext data) : base(data)
        {
        }

        public RecipeController(DataContext data, string recipeId) : base(data)
        {
            Recipe = Data.Recipes.SingleOrDefault(x => x.Id == recipeId);
            if (Recipe == null)
            {
                Recipe = new Recipe();
            }
        }

        public Recipe GetRecipe(string id)
        {
            var recipe = Data.Recipes.SingleOrDefault(x => x.Id == id);
            if (recipe == null)
                throw new EntryPointNotFoundException();

            return recipe;
        }
        public void AddIngredient(string ingredientName, double amount)
        {
            var product = Data.Ingredients.SingleOrDefault(x => x.Name == ingredientName);
            if (product == null)
            {
                product = new Ingredient { Id = Data.NextIngredientId().ToString(), Name = ingredientName };
                Data.Ingredients.Add(product);
                Data.SaveIngredients();
            }

            var recIngr = Data.RecipeIngredients.SingleOrDefault(x => x.IngredientId == product.Id && x.RecipeId == Recipe.Id);
            if (recIngr != null)
            {
                recIngr.Amount += amount;
            }
            else
            {
                recIngr = new RecipeIngredient { IngredientId = product.Id, RecipeId = Recipe.Id, Amount = amount };
                Recipe.Ingredients.Add(recIngr);
                Data.RecipeIngredients.Add(recIngr);
            }
            Data.SaveRecipes();
        }
    }
}

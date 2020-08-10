using RecipeBook.Data;
using RecipeBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipeBook.Controllers
{
    public class RecipeController : CommonController
    {
        public RecipeController(DataContext data) : base(data)
        {
        }

        public Recipe GetRecipe(string id)
        {
            var recipe = Data.Recipes.SingleOrDefault(x => x.Id == id);
            if (recipe == null)
                throw new EntryPointNotFoundException();

            return recipe;
        }
        public List<Recipe> GetRecipes(string categoryId = null)
        {
            return Data.Recipes.Where(x => x.CategoryId == categoryId).ToList();
        }

        public Recipe CreateRecipe(Recipe recipe)
        {
            var item = Data.Recipes.ToList().Find(x => string.Equals(x.Name, recipe.Name, StringComparison.OrdinalIgnoreCase));
            if (item != null)
                throw new Exception($"Recipe {item.Name} ({item.CategoryId}) already exists");
            if (string.IsNullOrEmpty(recipe.Id))
                recipe.Id = Data.NextRecipeId().ToString();

            Data.Recipes.Add(recipe);
            Data.SaveRecipes();
            return recipe;
        }
        public Recipe CreateRecipe(string recipeName, string categoryId)
        {
            return CreateRecipe(new Recipe { Name = recipeName, CategoryId = categoryId });
        }

        public void RemoveRecipe(string recipeId)
        {
            var item = Data.Recipes.ToList().Find(x => x.Id == recipeId);
            if (item == null)
                throw new Exception($"Recipe {recipeId} has not been found");

            Data.Recipes.Remove(item);
            Data.SaveRecipes();
        }
        public void AddIngredient(Recipe recipe, string ingredientName, double amount)
        {
            var product = Data.Ingredients.SingleOrDefault(x => string.Equals(x.Name, ingredientName, StringComparison.OrdinalIgnoreCase));
            if (product == null)
            {
                product = new Ingredient { Id = Data.NextIngredientId().ToString(), Name = ingredientName };
                Data.Ingredients.Add(product);
                Data.SaveIngredients();
            }

            var recIngr = Data.RecipeIngredients.SingleOrDefault(x => x.IngredientId == product.Id && x.RecipeId == recipe.Id);
            if (recIngr != null)
            {
                recIngr.Amount += amount;
            }
            else
            {
                recIngr = new RecipeIngredient { IngredientId = product.Id, RecipeId = recipe.Id, Amount = amount, Ingredient = product };
                recipe.Ingredients.Add(recIngr);
                Data.RecipeIngredients.Add(recIngr);
            }
            Data.SaveRecipes();
        }

        public void AddDirection(Recipe recipe, string stepDesc)
        {
            recipe.Directions.Add(new RecipeStep { StepNumber = recipe.Directions.Count + 1, StepInstruction = stepDesc });
            Data.SaveRecipes();
        }
    }
}

using RecipeBook.Data;
using RecipeBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipeBook.Controllers
{
    public class RecipeController : CommonController
    {
        public RecipeController(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Recipe GetRecipe(string id)
        {
            var recipe = UnitOfWork.Recipes.Get(id);
            if (recipe == null)
                throw new EntryPointNotFoundException();

            return recipe;
        }
        public List<Recipe> GetRecipes(string categoryId = null)
        {
            return UnitOfWork.Recipes.GetRecipesByCategoryId(categoryId).ToList();
        }

        public Recipe CreateRecipe(Recipe recipe)
        {
            var item = UnitOfWork.Recipes.SingleOrDefault(x => string.Equals(x.Name, recipe.Name, StringComparison.OrdinalIgnoreCase));
            if (item != null)
                throw new Exception($"Recipe {item.Name} ({item.CategoryId}) already exists");
            if (string.IsNullOrEmpty(recipe.Id))
                recipe.Id = Guid.NewGuid().ToString();

            UnitOfWork.Recipes.Add(recipe);
            UnitOfWork.Save();
            return recipe;
        }
        public Recipe CreateRecipe(string recipeName, string categoryId)
        {
            return CreateRecipe(new Recipe { Name = recipeName, CategoryId = categoryId });
        }

        public void RemoveRecipe(string recipeId)
        {
            var item = UnitOfWork.Recipes.Get(recipeId);
            if (item == null)
                throw new Exception($"Recipe {recipeId} has not been found");

            UnitOfWork.Recipes.Remove(item);
            UnitOfWork.Save();
        }
        public void AddIngredient(Recipe recipe, string ingredientName, double amount)
        {
            var product = UnitOfWork.Ingredients.SingleOrDefault(x => string.Equals(x.Name, ingredientName, StringComparison.OrdinalIgnoreCase));
            if (product == null)
            {
                product = new Ingredient { Id = Guid.NewGuid().ToString(), Name = ingredientName };
                UnitOfWork.Ingredients.Add(product);
            }

            var recIngr = UnitOfWork.RecipeIngredients.SingleOrDefault(x => x.IngredientId == product.Id && x.RecipeId == recipe.Id);
            if (recIngr != null)
            {
                recIngr.Amount += amount;
            }
            else
            {
                recIngr = new RecipeIngredient { IngredientId = product.Id, RecipeId = recipe.Id, Amount = amount, Ingredient = product };
                recipe.Ingredients.Add(recIngr);
                UnitOfWork.RecipeIngredients.Add(recIngr);
            }
            UnitOfWork.Save();
        }

        public void AddDirection(Recipe recipe, string stepDesc)
        {
            recipe.Directions.Add(new RecipeStep { StepNumber = recipe.Directions.Count + 1, StepInstruction = stepDesc });
            UnitOfWork.Save();
        }
    }
}

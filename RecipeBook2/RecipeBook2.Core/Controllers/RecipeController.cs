using RecipeBook2.Core.Entities;
using RecipeBook2.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipeBook2.Core.Controllers
{
    public class RecipeController : CommonController
    {
        public RecipeController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Recipe GetRecipe(int id)
        {
            var recipe = UnitOfWork.Recipes.Get(id);
            if (recipe == null)
                throw new EntryPointNotFoundException();

            return recipe;
        }
        public List<Recipe> GetRecipes(int? categoryId = null)
        {
            return UnitOfWork.Recipes.GetRecipesByCategoryId(categoryId).ToList();
        }

        public Recipe CreateRecipe(Recipe recipe)
        {
            var item = UnitOfWork.Recipes.SingleOrDefault(x => string.Equals(x.Name, recipe.Name, StringComparison.OrdinalIgnoreCase));
            if (item != null)
                throw new Exception($"Recipe {item.Name} ({item.CategoryId}) already exists");

            UnitOfWork.Recipes.Add(recipe);
            UnitOfWork.Save();
            return recipe;
        }
        public Recipe CreateRecipe(string recipeName, int? categoryId)
        {
            return CreateRecipe(new Recipe { Name = recipeName, CategoryId = categoryId });
        }

        public void RemoveRecipe(int recipeId)
        {
            var item = UnitOfWork.Recipes.Get(recipeId);
            if (item == null)
                throw new Exception($"Recipe {recipeId} has not been found");

            UnitOfWork.Recipes.Remove(item);
            UnitOfWork.Save();
        }

        public void UpdateRecipe(Recipe recipe)
        {
            var item = UnitOfWork.Recipes.Get(recipe.Id);
            if (item == null)
                throw new Exception($"Recipe {recipe.Id} has not been found");

            UnitOfWork.Recipes.Update(item);
            UnitOfWork.Save();
        }
        public void AddIngredient(Recipe recipe, string ingredientName, double amount)
        {
            var product = UnitOfWork.Ingredients.SingleOrDefault(x => string.Equals(x.Name, ingredientName, StringComparison.OrdinalIgnoreCase));
            if (product == null)
            {
                product = new Ingredient { Name = ingredientName };
                UnitOfWork.Ingredients.Add(product);
                UnitOfWork.Save();
            }

            var recIngr = UnitOfWork.RecipeIngredients.SingleOrDefault(x => x.IngredientId == product.Id && x.RecipeId == recipe.Id);
            if (recIngr != null)
            {
                recIngr.Amount += amount;
                UnitOfWork.RecipeIngredients.Update(recIngr);
            }
            else
            {
                recIngr = new RecipeIngredient { IngredientId = product.Id, RecipeId = recipe.Id, Amount = amount, Ingredient = product };
                UnitOfWork.RecipeIngredients.Add(recIngr);
            }
            UnitOfWork.Save();
        }

        public void AddDirection(Recipe recipe, string stepDesc)
        {
            UnitOfWork.RecipeSteps.Add(new RecipeStep { RecipeId = recipe.Id, StepNumber = recipe.Directions?.Count + 1 ?? 1, StepInstruction = stepDesc });
            UnitOfWork.Save();
        }
    }
}

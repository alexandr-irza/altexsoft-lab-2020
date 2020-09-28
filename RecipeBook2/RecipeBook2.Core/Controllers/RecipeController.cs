using RecipeBook2.Core.Entities;
using RecipeBook2.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBook2.Core.Controllers
{
    public class RecipeController : CommonController
    {
        public RecipeController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<Recipe> GetRecipeAsync(int id)
        {
            var recipe = await UnitOfWork.Recipes.GetAsync(id);
            if (recipe == null)
                throw new EntryPointNotFoundException();

            return recipe;
        }
        public async Task<List<Recipe>> GetRecipesAsync(int? categoryId = null)
        {
            return await UnitOfWork.Recipes.GetRecipesByCategoryId(categoryId);
        }

        public async Task<Recipe> CreateRecipeAsync(Recipe recipe)
        {
            var item = await UnitOfWork.Recipes.SingleOrDefaultAsync(x => x.Name == recipe.Name);
            if (item != null)
                throw new Exception($"Recipe {item.Name} ({item.CategoryId}) already exists");

            UnitOfWork.Recipes.Add(recipe);
            await UnitOfWork.SaveChangesAsync();
            return recipe;
        }
        public async Task<Recipe> CreateRecipeAsync(string recipeName, int? categoryId)
        {
            return await CreateRecipeAsync(new Recipe { Name = recipeName, CategoryId = categoryId });
        }

        public async Task RemoveRecipeAsync(int recipeId)
        {
            var item = await UnitOfWork.Recipes.GetAsync(recipeId);
            if (item == null)
                throw new Exception($"Recipe {recipeId} has not been found");

            UnitOfWork.Recipes.Remove(item);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task UpdateRecipeAsync(Recipe recipe)
        {
            var item = await UnitOfWork.Recipes.GetAsync(recipe.Id);
            if (item == null)
                throw new Exception($"Recipe {recipe.Id} has not been found");

            UnitOfWork.Recipes.Update(item);
            await UnitOfWork.SaveChangesAsync();
        }

        public async void AddIngredient(Recipe recipe, string ingredientName, double amount)
        {
            var product = await UnitOfWork.Ingredients.SingleOrDefaultAsync(x => x.Name == ingredientName);
            if (product == null)
            {
                product = new Ingredient { Name = ingredientName };
                UnitOfWork.Ingredients.Add(product);
                await UnitOfWork.SaveChangesAsync();
            }

            var recIngr = await UnitOfWork.RecipeIngredients.SingleOrDefaultAsync(x => x.IngredientId == product.Id && x.RecipeId == recipe.Id);
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
            await UnitOfWork.SaveChangesAsync();
        }

        public void AddDirection(Recipe recipe, string stepDesc)
        {
            UnitOfWork.RecipeSteps.Add(new RecipeStep { RecipeId = recipe.Id, StepNumber = recipe.Directions?.Count + 1 ?? 1, StepInstruction = stepDesc });
            UnitOfWork.SaveChangesAsync();
        }
    }
}

using RecipeBook2.Core.Entities;
using RecipeBook2.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeBook2.Core.Controllers
{
    public class IngredientController : CommonController
    {
        public IngredientController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public async Task<Ingredient> GetIngredientAsync(int id)
        {
            return await UnitOfWork.Ingredients.GetAsync(id);
        }

        public async Task<List<Ingredient>> GetIngredientsAsync()
        {
            return await UnitOfWork.Ingredients.GetAllAsync();
        }

        public async Task<Ingredient> CreateIngredientAsync(Ingredient ingredient)
        {
            if (ingredient == null)
                throw new ArgumentNullException();
            if (string.IsNullOrWhiteSpace(ingredient.Name))
                throw new Exception($"Ingredient name cannot be empty");
            var item = await UnitOfWork.Ingredients.SingleOrDefaultAsync(x => x.Name == ingredient.Name);
            if (item != null)
                throw new Exception($"Ingredient {item.Name} already exists");

            UnitOfWork.Ingredients.Add(ingredient);
            await UnitOfWork.SaveChangesAsync();
            return ingredient;
        }
        public async Task RemoveIngredientAsync(int ingredientId)
        {
            var item = await UnitOfWork.Ingredients.GetAsync(ingredientId);
            if (item == null)
                throw new Exception($"Ingredient {ingredientId} has not been found");

            UnitOfWork.Ingredients.Remove(item);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task UpdateIngredientAsync(Ingredient ingredient)
        {
            var item = await UnitOfWork.Ingredients.GetAsync(ingredient.Id);
            if (item == null)
                throw new Exception($"Ingredient {ingredient.Id} has not been found");

            item.Name = ingredient.Name;
            await UnitOfWork.SaveChangesAsync();
        }

    }
}

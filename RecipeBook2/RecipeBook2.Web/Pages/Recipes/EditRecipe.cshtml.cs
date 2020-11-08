using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeBook2.Core.Controllers;
using RecipeBook2.Core.Entities;

namespace RecipeBook2.Web.Pages.Recipes
{
    public class EditRecipeModel : PageModel
    {
        private readonly RecipeController recipeController;
        private readonly IngredientController ingredientController;
        private readonly CategoryController categoryController;

        [BindProperty(SupportsGet = true)]
        public Recipe Recipe { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<Category> Categories { get; set; }
        public EditRecipeModel(RecipeController recipeController, IngredientController ingredientController, CategoryController categoryController)
        {
            this.recipeController = recipeController;
            this.ingredientController = ingredientController;
            this.categoryController = categoryController;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Recipe = await recipeController.GetRecipeAsync(id);
            Ingredients = await ingredientController.GetIngredientsAsync();
            Categories = await categoryController.GetAllCategoriesAsync();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                await recipeController.UpdateRecipeAsync(Recipe);
                return RedirectToPage("Index");
            }
            
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteIngredientAsync(int ingredientId)
        {
            Ingredients = await ingredientController.GetIngredientsAsync();
            var item = Recipe.Ingredients.FirstOrDefault(x => x.RecipeId == Recipe.Id && x.IngredientId == ingredientId);
            if (item != null)
            {
                Recipe.Ingredients.Remove(item);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteDirectionAsync(int stepId)
        {
            Ingredients = await ingredientController.GetIngredientsAsync();

            var item = Recipe.Directions.FirstOrDefault(x => x.RecipeId == Recipe.Id && x.Id == stepId);
            if (item != null)
            {
                Recipe.Directions.Remove(item);
            }
            return Page();
        }

        public async Task OnPostAddDirectionAsync()
        {
            Ingredients = await ingredientController.GetIngredientsAsync();
            var item = new RecipeStep { RecipeId = Recipe.Id, StepNumber = Recipe.Directions.Count + 1 };
            if (item != null)
            {
                Recipe.Directions.Add(item);
            }
        }

        public async Task OnPostAddIngredientAsync()
        {
            Ingredients = await ingredientController.GetIngredientsAsync();
            var item = new RecipeIngredient { RecipeId = Recipe.Id };
            if (item != null)
            {
                Recipe.Ingredients.Add(item);
            }
        }
    }
}

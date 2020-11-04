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
    public class EditModel : PageModel
    {
        private readonly RecipeController recipeController;
        [BindProperty(SupportsGet = true)]
        public Recipe Recipe { get; set; }
        public EditModel(RecipeController recipeController)
        {
            this.recipeController = recipeController;
        }

        public async Task OnGetAsync(int id)
        {
            Recipe = await recipeController.GetRecipeAsync(id);
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

        public IActionResult OnPostDeleteIngredient(int ingredientId)
        {
            var item = Recipe.Ingredients.FirstOrDefault(x => x.RecipeId == Recipe.Id && x.IngredientId == ingredientId);
            if (item != null)
            {
                Recipe.Ingredients.Remove(item);
            }
            return Page();
        }

        public IActionResult OnPostDeleteDirection(int stepId)
        {
            var item = Recipe.Directions.FirstOrDefault(x => x.RecipeId == Recipe.Id && x.Id == stepId);
            if (item != null)
            {
                Recipe.Directions.Remove(item);
            }
            return Page();
        }

        public IActionResult OnPostAddDirection()
        {
            var item = new RecipeStep { RecipeId = Recipe.Id, StepNumber = 1, StepInstruction = "Test"};
            if (item != null)
            {
                Recipe.Directions.Add(item);
            }
            return Page();
        }

        public IActionResult OnPostAddIngredient()
        {
            var item = new RecipeIngredient { RecipeId = Recipe.Id, IngredientId = 1, Amount = 10 };
            item.Ingredient = new Ingredient { Id = 1, Name = "Muka" };
            if (item != null)
            {
                Recipe.Ingredients.Add(item);
            }
            return Page();
        }
    }
}

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
            else
            {
                return Page();
            }
        }

        public IActionResult OnPostDeleteIngredientAsync(int ingredientId)
        {
            ModelState.Clear();
            var item = Recipe.Ingredients.SingleOrDefault(x => x.RecipeId == Recipe.Id && x.IngredientId == ingredientId);
            if (item != null)
            {
                Recipe.Ingredients.Remove(item);
            }
            return Page();
        }

        public IActionResult OnPostDeleteDirectionAsync(int stepId)
        {
            ModelState.Clear();
            var item = Recipe.Directions.SingleOrDefault(x => x.RecipeId == Recipe.Id && x.Id == stepId);
            if (item != null)
            {
                Recipe.Directions.Remove(item);
            }
            return Page();
        }

    }
}

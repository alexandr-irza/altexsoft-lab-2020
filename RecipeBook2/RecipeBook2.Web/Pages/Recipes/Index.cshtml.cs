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
    public class IndexModel : PageModel
    {
        private readonly RecipeController recipeController;
        public List<Recipe> Recipes { get; set; }
        public IndexModel(RecipeController recipeController)
        {
            this.recipeController = recipeController;
        }

        public async Task OnGetAsync()
        {
            Recipes = await recipeController.GetAllRecipesAsync();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await recipeController.RemoveRecipeAsync(id);
            return RedirectToPage("Index");
        }
    }
}

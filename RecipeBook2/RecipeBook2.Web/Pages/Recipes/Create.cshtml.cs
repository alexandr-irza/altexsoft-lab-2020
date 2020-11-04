using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeBook2.Core.Controllers;
using RecipeBook2.Core.Entities;

namespace RecipeBook2.Web.Pages.Recipes
{
    public class CreateModel : PageModel
    {
        private readonly RecipeController recipeController;
        [BindProperty]
        public Recipe Recipe { get; set; }
        public CreateModel(RecipeController recipeController)
        {
            this.recipeController = recipeController;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                await recipeController.CreateRecipeAsync(Recipe);
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}

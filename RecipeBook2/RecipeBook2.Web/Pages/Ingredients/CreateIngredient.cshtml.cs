using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeBook2.Core.Controllers;
using RecipeBook2.Core.Entities;

namespace RecipeBook2.Web.Pages.Ingredients
{
    public class CreateIngredientModel : PageModel
    {
        private readonly IngredientController ingredientController;
        [BindProperty]
        public Ingredient Ingredient { get; set; }
        public CreateIngredientModel(IngredientController ingredientController)
        {
            this.ingredientController = ingredientController;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                await ingredientController.CreateIngredientAsync(Ingredient);
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}

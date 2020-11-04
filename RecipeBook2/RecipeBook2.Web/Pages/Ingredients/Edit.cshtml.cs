using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeBook2.Core.Controllers;
using RecipeBook2.Core.Entities;

namespace RecipeBook2.Web.Pages.Ingredients
{
    public class EditModel : PageModel
    {
        private readonly IngredientController ingredientController;
        [BindProperty]
        public Ingredient Ingredient { get; set; }
        public EditModel(IngredientController ingredientController)
        {
            this.ingredientController = ingredientController;
        }

        public async Task OnGetAsync(int id)
        {
            Ingredient = await ingredientController.GetIngredientAsync(id);
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                await ingredientController.UpdateIngredientAsync(Ingredient);
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}

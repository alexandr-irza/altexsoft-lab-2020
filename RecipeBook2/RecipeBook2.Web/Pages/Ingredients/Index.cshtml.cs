using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeBook2.Core.Controllers;
using RecipeBook2.Core.Entities;

namespace RecipeBook2.Web.Pages.Ingredients
{
    public class IndexModel : PageModel
    {
        private readonly IngredientController ingredientController;
        public List<Ingredient> Ingredients { get; set; }
        public IndexModel(IngredientController ingredientController)
        {
            this.ingredientController = ingredientController;
        }

        public async Task OnGetAsync()
        {
            Ingredients = await ingredientController.GetIngredientsAsync();
        }
        public async Task<IActionResult> OnPostDelete(int id)
        {
            await ingredientController.RemoveIngredientAsync(id);
            return RedirectToPage("Index");
        }
    }
}

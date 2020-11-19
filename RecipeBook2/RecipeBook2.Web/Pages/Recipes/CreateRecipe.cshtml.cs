using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeBook2.Core.Controllers;
using RecipeBook2.Core.Entities;

namespace RecipeBook2.Web.Pages.Recipes
{
    public class CreateRecipeModel : PageModel
    {
        private readonly RecipeController recipeController;
        private readonly IngredientController ingredientController;
        private readonly CategoryController categoryController;

        [BindProperty]
        public Recipe Recipe { get; set; }
        public List<Ingredient> Ingredients { get; private set; }
        public List<Category> Categories { get; set; }
        public CreateRecipeModel(RecipeController recipeController, IngredientController ingredientController, CategoryController categoryController)
        {
            this.recipeController = recipeController;
            this.ingredientController = ingredientController;
            this.categoryController = categoryController;
        }
        public async Task OnGetAsync()
        {
            Recipe = new Recipe
            {
                Ingredients = new List<RecipeIngredient>(),
                Directions = new List<RecipeStep>()
            };
            Ingredients = await ingredientController.GetIngredientsAsync();
            Categories = await categoryController.GetAllCategoriesAsync();
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using RecipeBook2.Core.Controllers;
using RecipeBook2.Core.Entities;
using RecipeBook2.Web.ViewModels;

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

        public async Task<PartialViewResult> OnGetAddIngredientModalPartialAsync(int recipeId)
        {
            var ingredients = await ingredientController.GetIngredientsAsync();
            var recipeIngredient = new RecipeIngredient 
            { 
                Amount = 1, 
                RecipeId = recipeId
            };
            var addIngredient = new AddIngredientViewModel
            {
                Ingredients = ingredients,
                RecipeIngredient = recipeIngredient
            };
            return new PartialViewResult
            {
                ViewName = "_AddIngredientModalPartial",
                ViewData = new ViewDataDictionary<AddIngredientViewModel>(ViewData, addIngredient)
            };
        }

        public async Task<PartialViewResult> OnPostAddIngredientModalPartialAsync(AddIngredientViewModel model)
        {
            ModelState.Remove("Name"); //AIrza I do not know why Name is here, I do not have it in my AddIngredientViewModel model
            if (ModelState.IsValid)
            {
                Recipe.Ingredients.Add(model.RecipeIngredient); //AIrza Here always null
            }
            var ingredients = await ingredientController.GetIngredientsAsync();
            model.Ingredients = ingredients;
            return new PartialViewResult
            {
                ViewName = "_AddIngredientModalPartial",
                ViewData = new ViewDataDictionary<AddIngredientViewModel>(ViewData, model)
            };
        }

        public PartialViewResult OnGetAddDirectionModalPartial(int recipeId, int stepsCount)
        {
            var recipeStep = new RecipeStep
            {
                StepNumber = stepsCount + 1,
                RecipeId = recipeId
            };
            return new PartialViewResult
            {
                ViewName = "_AddDirectionModalPartial",
                ViewData = new ViewDataDictionary<RecipeStep>(ViewData, recipeStep)
            };
        }

        public PartialViewResult OnPostAddDirectionModalPartial(RecipeStep model)
        {
            ModelState.Remove("Name"); //AIrza I do not know why Name is here, I do not have it in my AddIngredientViewModel model
            if (ModelState.IsValid)
            {
                Recipe.Directions.Add(model); //AIrza Here always null
            }

            return new PartialViewResult
            {
                ViewName = "_AddDirectionModalPartial",
                ViewData = new ViewDataDictionary<RecipeStep>(ViewData, model)
            };
        }

        public async Task<IActionResult> OnPostDeleteIngredientAsync(int ingredientId)
        {
            Ingredients = await ingredientController.GetIngredientsAsync();
            Categories = await categoryController.GetAllCategoriesAsync();
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
            Categories = await categoryController.GetAllCategoriesAsync();
            var item = Recipe.Directions.FirstOrDefault(x => x.RecipeId == Recipe.Id && x.Id == stepId);
            if (item != null)
            {
                Recipe.Directions.Remove(item);
            }
            return Page();
        }
    }
}

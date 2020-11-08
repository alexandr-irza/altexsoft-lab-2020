using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeBook2.Core.Controllers;
using RecipeBook2.Core.Entities;

namespace RecipeBook2.Web.Pages.Categories
{
    public class EditCategoryModel : PageModel
    {
        private readonly CategoryController categoryController;
        [BindProperty]
        public Category Category { get; set; }
        public EditCategoryModel(CategoryController categoryController)
        {
            this.categoryController = categoryController;
        }

        public async Task OnGetAsync(int id)
        {
            Category = await categoryController.GetCategoryAsync(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                await categoryController.UpdateCategoryAsync(Category);
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}

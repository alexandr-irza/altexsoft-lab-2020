using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeBook2.Core.Controllers;
using RecipeBook2.Core.Entities;
using RecipeBook2.Infrastructure.Data;

namespace RecipeBook2.Web.Pages.Categories
{
    public class CreateCategoryModel : PageModel
    {
        private readonly CategoryController categoryController;
        [BindProperty]
        public Category Category { get; set; }
        [BindProperty]
        public int? ParentId { get; set; }
        public CreateCategoryModel(CategoryController categoryController)
        {
            this.categoryController = categoryController;
        }

        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                Category.ParentId = ParentId;
                await categoryController.CreateCategoryAsync(Category);
                return RedirectToPage("Index", new { id = ParentId });
            }

            return Page();
        }
    }
}

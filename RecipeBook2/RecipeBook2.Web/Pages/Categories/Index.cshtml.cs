using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeBook2.Core.Controllers;
using RecipeBook2.Core.Entities;
using RecipeBook2.Core.Interfaces;

namespace RecipeBook2.Web.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly CategoryController categoryController;
        public List<Category> Categories { get; set; }
        [BindProperty]
        public int? CategoryId { get; set; }

        public IndexModel(CategoryController categoryController)
        {
            this.categoryController = categoryController;
        }
        public async Task OnGetAsync(int? id)
        {
            CategoryId = id;
            Categories = await categoryController.GetCategoriesAsync(id);
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await categoryController.RemoveCategoryAsync(id);
            return RedirectToPage("Index", new { id = id });
        }
    }
}

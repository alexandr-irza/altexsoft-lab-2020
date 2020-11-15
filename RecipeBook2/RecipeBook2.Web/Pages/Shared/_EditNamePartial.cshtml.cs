using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RecipeBook2.Web.Pages.Shared
{
    public class _EditNamePartialModel : PageModel
    {
        [BindProperty]
        public string Name { get; set; }
        public void OnGet(string name)
        {
            Name = name;
        }
    }
}
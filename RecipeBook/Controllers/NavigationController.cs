using RecipeBook.Data;
using RecipeBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecipeBook.Controllers
{
    public class NavigationController : CommonController
    {
        public NavigationController(DataContext data) : base(data)
        {
        }

        public List<BaseModel> GetItems(string rootCategoryId)
        {
            var res = new List<BaseModel>();

            Data.Categories.Where(x => x.ParentId == rootCategoryId).ToList().ForEach(x => res.Add(x));
            Data.Recipes.Where(x => x.CategoryId == rootCategoryId).ToList().ForEach(x => res.Add(x));

            return res;
        }
    }
}

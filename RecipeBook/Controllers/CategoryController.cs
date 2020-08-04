using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RecipeBook.Data;
using RecipeBook.Models;

namespace RecipeBook.Controllers
{
    public class CategoryController : CommonController
    {
        public CategoryController(DataContext data) : base(data)
        {
        }

        public List<Category> GetCategories(string parentId = null)
        {
             return Data.Categories.Where(x => x.ParentId == parentId).ToList();
        }

        public void CreateCategory(Category category)
        {
            if (Data.Categories.ToList().Find(x => x.Name == category.Name && x.ParentId == category.ParentId) != null)
                throw new Exception($"Category {category.Name} already exists");

            Data.Categories.Add(category);
        }

        public void RemoveCategory(Category category)
        {
            var item = Data.Categories.ToList().Find(x => x.Id == category.Id);
            if (item == null)
                throw new Exception($"Category {category.Name} has not been found");

            Data.Categories.Remove(item);
        }
    }
}

using RecipeBook.Data;
using RecipeBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipeBook.Controllers
{
    public class CategoryController : CommonController
    {
        public CategoryController(DataContext data) : base(data)
        {
        }

        public Category GetCategory(string id = null)
        {
            return Data.Categories.SingleOrDefault(x => x.Id == id);
        }

        public List<Category> GetCategories(string parentId = null)
        {
            return Data.Categories.Where(x => x.ParentId == parentId).ToList();
        }
        public Category CreateCategory(Category category)
        {
            if (Data.Categories.ToList().Find(x => string.Equals(x.Name, category.Name, StringComparison.OrdinalIgnoreCase) && x.ParentId == category.ParentId) != null)
                throw new Exception($"Category {category.Name} already exists");
            if (string.IsNullOrEmpty(category.Id))
                category.Id = Data.NextCategoryId().ToString();
            if (category.Parent == null)
                category.Parent = Data.Categories.SingleOrDefault(x => x.Id == category.ParentId);
            Data.Categories.Add(category);
            Data.SaveCategories();
            return category;
        }

        public Category CreateCategory(string categoryName, string parentId)
        {
            return CreateCategory(new Category { Name = categoryName, ParentId = parentId });
        }

        public void RemoveCategory(string categoryId)
        {
            var item = Data.Categories.ToList().Find(x => x.Id == categoryId);
            if (item == null)
                throw new Exception($"Category {categoryId} has not been found");

            Data.Categories.Remove(item);
            Data.SaveCategories();
        }

    }
}

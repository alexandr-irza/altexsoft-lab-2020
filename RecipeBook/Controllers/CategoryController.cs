using RecipeBook.Data;
using RecipeBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipeBook.Controllers
{
    public class CategoryController : CommonController
    {
        public CategoryController(UnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Category GetCategory(string id = null)
        {
            return UnitOfWork.Categories.Get(id);
        }

        public List<Category> GetCategories(string parentId = null)
        {
            return UnitOfWork.Categories.GetCategoriesByParentId(parentId).ToList();
        }
        public Category CreateCategory(Category category)
        {
            var item = UnitOfWork.Categories.SingleOrDefault(x => string.Equals(x.Name, category.Name, StringComparison.OrdinalIgnoreCase) && x.ParentId == category.ParentId);
            if (item != null)
                throw new Exception($"Category {category.Name} already exists");
            if (string.IsNullOrEmpty(category.Id))
                category.Id = Guid.NewGuid().ToString();
            if (category.Parent == null)
                category.Parent = UnitOfWork.Categories.SingleOrDefault(x => x.Id == category.ParentId);
            UnitOfWork.Categories.Add(category);
            UnitOfWork.Save();
            return category;
        }

        public Category CreateCategory(string categoryName, string parentId)
        {
            return CreateCategory(new Category { Name = categoryName, ParentId = parentId });
        }

        public void RemoveCategory(string categoryId)
        {
            var item = UnitOfWork.Categories.SingleOrDefault(x => x.Id == categoryId);
            if (item == null)
                throw new Exception($"Category {categoryId} has not been found");

            UnitOfWork.Categories.Remove(item);
            UnitOfWork.Save();
        }

    }
}

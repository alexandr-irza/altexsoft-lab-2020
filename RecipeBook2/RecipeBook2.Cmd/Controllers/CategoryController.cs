using RecipeBook2.Core.Entities;
using RecipeBook2.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipeBook2.Controllers
{
    public class CategoryController : CommonController
    {
        public CategoryController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Category GetCategory(int id)
        {
            return UnitOfWork.Categories.Get(id);
        }

        public List<Category> GetCategories(int parentId)
        {
            return UnitOfWork.Categories.GetCategoriesByParentId(parentId).ToList();
        }
        public Category CreateCategory(Category category)
        {
            var item = UnitOfWork.Categories.SingleOrDefault(x => string.Equals(x.Name, category.Name, StringComparison.OrdinalIgnoreCase) && x.ParentId == category.ParentId);
            if (item != null)
                throw new Exception($"Category {category.Name} already exists");
            if (category.Parent == null)
                category.Parent = UnitOfWork.Categories.SingleOrDefault(x => x.Id == category.ParentId);
            UnitOfWork.Categories.Add(category);
            UnitOfWork.Save();
            return category;
        }

        public Category CreateCategory(string categoryName, int parentId)
        {
            return CreateCategory(new Category { Name = categoryName, ParentId = parentId });
        }

        public void RemoveCategory(int categoryId)
        {
            var item = UnitOfWork.Categories.Get(categoryId);
            if (item == null)
                throw new Exception($"Category {categoryId} has not been found");

            UnitOfWork.Categories.Remove(item);
            UnitOfWork.Save();
        }

        public void UpdateCategory(Category category)
        {
            var item = UnitOfWork.Categories.Get(category.Id);
            if (item == null)
                throw new Exception($"Category {category.Id} has not been found");
            UnitOfWork.Categories.Update(category);
            UnitOfWork.Save();
        }

    }
}

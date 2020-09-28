using RecipeBook2.Core.Entities;
using RecipeBook2.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBook2.Core.Controllers
{
    public class CategoryController : CommonController
    {
        public CategoryController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<Category> GetCategoryAsync(int id)
        {
            return await UnitOfWork.Categories.GetAsync(id);
        }

        public async Task<List<Category>> GetCategoriesAsync(int parentId)
        {
            return await UnitOfWork.Categories.GetCategoriesByParentId(parentId);
        }
        public async Task<Category> CreateCategoryAsync(Category category)
        {
            var item = await UnitOfWork.Categories.SingleOrDefaultAsync(x => x.Name == category.Name && x.ParentId == category.ParentId);
            if (item != null)
                throw new Exception($"Category {category.Name} already exists");
            if (category.Parent == null)
                category.Parent = await UnitOfWork.Categories.GetAsync(category.ParentId);
            UnitOfWork.Categories.Add(category);
            await UnitOfWork.SaveChangesAsync();
            return category;
        }

        public async Task<Category> CreateCategoryAsync(string categoryName, int parentId)
        {
            return await CreateCategoryAsync(new Category { Name = categoryName, ParentId = parentId });
        }

        public async void RemoveCategory(int categoryId)
        {
            var item = await UnitOfWork.Categories.GetAsync(categoryId);
            if (item == null)
                throw new Exception($"Category {categoryId} has not been found");

            UnitOfWork.Categories.Remove(item);
            await UnitOfWork.SaveChangesAsync();
        }

        public async void UpdateCategory(Category category)
        {
            var item = await UnitOfWork.Categories.GetAsync(category.Id);
            if (item == null)
                throw new Exception($"Category {category.Id} has not been found");
            UnitOfWork.Categories.Update(category);
            await UnitOfWork.SaveChangesAsync();
        }

    }
}

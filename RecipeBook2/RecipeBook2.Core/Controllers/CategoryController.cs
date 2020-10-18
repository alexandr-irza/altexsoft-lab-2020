using RecipeBook2.Core.Entities;
using RecipeBook2.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RecipeBook2.Core.Exceptions;

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

        public async Task<List<Category>> GetCategoriesAsync(int? parentId)
        {
            return await UnitOfWork.Categories.GetCategoriesByParentIdAsync(parentId);
        }
        public async Task<Category> CreateCategoryAsync(Category category)
        {
            if (category == null)
                throw new ArgumentNullException();
            if (string.IsNullOrWhiteSpace(category.Name))
                throw new EmptyFieldException($"{ nameof(Category) } field { nameof(category.Name) } cannot be empty.");
            var item = await UnitOfWork.Categories.SingleOrDefaultAsync(x => x.Name == category.Name && x.ParentId == category.ParentId);
            if (item != null)
                throw new EntityAlreadyExistsException($"{ nameof(Category) } field { item.Name } already exists.");
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

        public async Task RemoveCategoryAsync(int categoryId)
        {
            var item = await UnitOfWork.Categories.GetAsync(categoryId);
            if (item == null)
                throw new NotFoundException($"{ nameof(Category) } ({ categoryId }) not found.");

            UnitOfWork.Categories.Remove(item);
            await UnitOfWork.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            var item = await UnitOfWork.Categories.GetAsync(category.Id);
            if (item == null)
                throw new NotFoundException($"{ nameof(Category) } ({ category.Id }) not found.");
            item.Name = category.Name;
            await UnitOfWork.SaveChangesAsync();
        }
    }
}

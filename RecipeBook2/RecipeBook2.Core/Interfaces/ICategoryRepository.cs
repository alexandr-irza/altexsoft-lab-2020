using RecipeBook2.Core.Entities;
using System.Collections.Generic;
using RecipeBook2.SharedKernel;

namespace RecipeBook2.Core.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<Category> GetCategoriesByParentId(int? parentId);
    }
}

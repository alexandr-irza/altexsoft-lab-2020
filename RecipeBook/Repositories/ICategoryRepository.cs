using RecipeBook.Models;
using System.Collections.Generic;

namespace RecipeBook.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<Category> GetCategoriesByParentId(string parentId);
    }
}

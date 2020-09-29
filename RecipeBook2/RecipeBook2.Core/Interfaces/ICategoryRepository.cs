using RecipeBook2.Core.Entities;
using System.Collections.Generic;
using RecipeBook2.SharedKernel;
using System.Threading.Tasks;

namespace RecipeBook2.Core.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<List<Category>> GetCategoriesByParentIdAsync(int? parentId);
    }
}

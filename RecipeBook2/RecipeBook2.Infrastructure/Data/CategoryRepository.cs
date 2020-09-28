using RecipeBook2.Core.Entities;
using RecipeBook2.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeBook2.Infrastructure.Data
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(RecipeBookContext context) : base(context)
        {

        }

        public Task<List<Category>> GetCategoriesByParentId(int? parentId)
        {
            return FindAsync(x => x.ParentId == parentId);
        }
    }
}

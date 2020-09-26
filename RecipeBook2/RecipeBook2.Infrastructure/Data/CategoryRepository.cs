using RecipeBook2.Core.Entities;
using RecipeBook2.Core.Interfaces;
using System.Collections.Generic;

namespace RecipeBook2.Infrastructure.Data
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(RecipeBookContext context) : base(context)
        {

        }

        public IEnumerable<Category> GetCategoriesByParentId(int? parentId)
        {
            return Find(x => x.ParentId == parentId);
        }
    }
}

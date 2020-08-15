using RecipeBook.Data;
using RecipeBook.Models;
using System.Collections.Generic;

namespace RecipeBook.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(IDataContext context) : base(context)
        {

        }

        public override Category Get(string id)
        {
            return SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<Category> GetCategoriesByParentId(string parentId)
        {
            return Find(x => x.ParentId == parentId);
        }
    }
}

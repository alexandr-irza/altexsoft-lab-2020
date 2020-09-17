using Microsoft.EntityFrameworkCore;
using RecipeBook2.Core.Entities;
using RecipeBook2.Core.Interfaces;
using RecipeBook2.Infrastructure.Data;
using System.Collections.Generic;

namespace RecipeBook2.Infrastructure.Repositories
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

using RecipeBook2.Core.Entities;
using RecipeBook2.Core.Interfaces;
using RecipeBook2.Infrastructure.Data;
using System.Collections.Generic;

namespace RecipeBook2.Infrastructure.Repositories
{
    public class RecipeRepository : BaseRepository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(RecipeBookContext context) : base(context)
        {
        }


        public IEnumerable<Recipe> GetRecipesByCategoryId(int? categoryId)
        {
            return Find(x => x.CategoryId == categoryId);
        }
    }
}

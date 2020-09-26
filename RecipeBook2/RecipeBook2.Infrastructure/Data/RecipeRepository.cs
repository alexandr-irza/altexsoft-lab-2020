using RecipeBook2.Core.Entities;
using RecipeBook2.Core.Interfaces;
using System.Collections.Generic;

namespace RecipeBook2.Infrastructure.Data
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

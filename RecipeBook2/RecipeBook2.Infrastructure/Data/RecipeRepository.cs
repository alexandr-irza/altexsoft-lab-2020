using RecipeBook2.Core.Entities;
using RecipeBook2.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeBook2.Infrastructure.Data
{
    public class RecipeRepository : BaseRepository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(RecipeBookContext context) : base(context)
        {
        }

        public Task<List<Recipe>> GetRecipesByCategoryId(int? categoryId)
        {
            return FindAsync(x => x.CategoryId == categoryId);
        }
    }
}

using RecipeBook.Data;
using RecipeBook.Models;
using System.Collections.Generic;

namespace RecipeBook.Repositories
{
    public class RecipeRepository : Repository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(IDataContext context) : base(context)
        {

        }

        public override Recipe Get(string id)
        {
            return SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<Recipe> GetRecipesByCategoryId(string categoryId)
        {
            return Find(x => x.CategoryId == categoryId);
        }
    }
}

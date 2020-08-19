using RecipeBook.Data;
using RecipeBook.Models;

namespace RecipeBook.Repositories
{
    public class IngredientRepository : Repository<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(IDataContext context) : base(context)
        {
        }

        public override Ingredient Get(string id)
        {
            return SingleOrDefault(x => x.Id == id);
        }
    }
}

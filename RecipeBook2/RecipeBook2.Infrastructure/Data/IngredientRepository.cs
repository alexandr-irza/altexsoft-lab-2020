using RecipeBook2.Core.Entities;
using RecipeBook2.Core.Interfaces;

namespace RecipeBook2.Infrastructure.Data
{
    public class IngredientRepository : BaseRepository<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(RecipeBookContext context) : base(context)
        {
        }

    }
}

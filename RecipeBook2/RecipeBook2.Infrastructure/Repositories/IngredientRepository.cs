using RecipeBook2.Core.Entities;
using RecipeBook2.Core.Interfaces;
using RecipeBook2.Infrastructure.Data;

namespace RecipeBook2.Infrastructure.Repositories
{
    public class IngredientRepository : BaseRepository<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(RecipeBookContext context) : base(context)
        {
        }

    }
}

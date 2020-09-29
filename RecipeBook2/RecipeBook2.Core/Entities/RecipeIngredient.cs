using RecipeBook2.SharedKernel;

namespace RecipeBook2.Core.Entities
{
    public class RecipeIngredient: BaseEntity
    {
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public virtual Ingredient Ingredient { get; set; }
        public double Amount { get; set; }
    }
}

using RecipeBook2.SharedKernel;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeBook2.Core.Entities
{
    public class RecipeIngredient: BaseEntity
    {
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        [NotMapped]
        public Ingredient Ingredient { get; set; } = new Ingredient();
        public double Amount { get; set; }
    }
}

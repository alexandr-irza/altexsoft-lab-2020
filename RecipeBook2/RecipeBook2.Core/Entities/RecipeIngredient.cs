using RecipeBook2.SharedKernel;
using System.ComponentModel.DataAnnotations;

namespace RecipeBook2.Core.Entities
{
    public class RecipeIngredient: BaseEntity
    {
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public virtual Ingredient Ingredient { get; set; }
        [Required]
        public double Amount { get; set; }
    }
}

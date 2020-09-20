using RecipeBook2.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeBook2.Core.Entities
{
    public class RecipeIngredient: BaseEntity
    {
        [Key, Column(Order = 0)]
        public int RecipeId { get; set; }
        [Key, Column(Order = 1)]
        public int IngredientId { get; set; }
        public virtual Ingredient Ingredient { get; set; }
        [Required]
        public double Amount { get; set; }
    }
}

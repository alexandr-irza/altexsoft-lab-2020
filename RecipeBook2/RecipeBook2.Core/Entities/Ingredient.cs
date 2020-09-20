using RecipeBook2.SharedKernel;
using System.ComponentModel.DataAnnotations;

namespace RecipeBook2.Core.Entities
{
    public class Ingredient: BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}

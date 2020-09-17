using RecipeBook2.SharedKernel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeBook2.Core.Entities
{
    public class Recipe: BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }
        [NotMapped]
        public List<RecipeIngredient> Ingredients { get; set; } = new List<RecipeIngredient>();
        public List<RecipeStep> Directions { get; set; } = new List<RecipeStep>();

        public override string ToString()
        {
            return $"Recipe {Name}";
        }
    }
}

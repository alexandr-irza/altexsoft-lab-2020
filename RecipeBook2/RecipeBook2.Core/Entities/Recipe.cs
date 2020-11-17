using RecipeBook2.SharedKernel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeBook2.Core.Entities
{
    public class Recipe: BaseEntity
    {
        [Display(Name = "Recipe Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<RecipeIngredient> Ingredients { get; set; }
        public virtual List<RecipeStep> Directions { get; set; }
        public override string ToString()
        {
            return $"Recipe {Name}";
        }
    }
}

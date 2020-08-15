using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RecipeBook.Models
{
    public class Recipe : BaseModel
    {
        public string Description { get; set; }
        public string CategoryId { get; set; }
        [JsonIgnore]
        public List<RecipeIngredient> Ingredients { get; set; } = new List<RecipeIngredient>();
        public List<RecipeStep> Directions { get; set; } = new List<RecipeStep>();

        public override string ToString()
        {
            return $"Recipe {Name}";
        }
    }
}

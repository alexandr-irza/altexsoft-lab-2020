using System.Text.Json.Serialization;

namespace RecipeBook.Models
{
    public class RecipeIngredient
    {
        public string RecipeId { get; set; }
        public string IngredientId { get; set; }
        [JsonIgnore]
        public Ingredient Ingredient { get; set; } = new Ingredient();
        public double Amount { get; set; }
    }
}

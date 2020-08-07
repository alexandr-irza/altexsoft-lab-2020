using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RecipeBook.Models
{
    public class RecipeIngredient
    {
        public string Id { get; set; }
        public string RecipeId { get; set; }
        public string IngredientId { get; set; }
        [JsonIgnore]
        public Ingredient Ingredient { get; set; } = new Ingredient();
        public double Amount { get; set; }
    }
}

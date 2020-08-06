using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RecipeBook.Models
{
    public class Recipe : BaseModel
    {
        public string Description { get; set; }
        public string CategoryId { get; set; }
        [JsonIgnore]
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public string Directions { get; set; }

    }
}

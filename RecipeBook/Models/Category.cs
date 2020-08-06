using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace RecipeBook.Models
{
    public class Category : BaseModel
    {
        public string ParentId { get; set; }
        [JsonIgnore]
        public List<Category> Categories { get; set; } = new List<Category>();
        [JsonIgnore]
        public List<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}

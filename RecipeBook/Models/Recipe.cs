using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeBook.Models
{
    public class Recipe
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public List<string> Steps { get; set; } = new List<string>();

        public string Test { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeBook.Models
{
    public class Category
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ParentId { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}

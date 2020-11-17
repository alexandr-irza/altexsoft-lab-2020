using RecipeBook2.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBook2.Web.ViewModels
{
    public class AddIngredientViewModel
    {
        public RecipeIngredient RecipeIngredient { get; set; }
        public List<Ingredient> Ingredients { get; set; }
    }
}

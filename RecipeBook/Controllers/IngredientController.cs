using System;
using System.Collections.Generic;
using System.Text;
using RecipeBook.Data;

namespace RecipeBook.Controllers
{
    public class IngredientController : CommonController
    {
        public IngredientController(DataContext data) : base(data)
        {
        }
    }
}

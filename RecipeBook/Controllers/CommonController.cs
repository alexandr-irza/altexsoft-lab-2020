using RecipeBook.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeBook.Controllers
{
    public class CommonController
    {
        public DataContext Data { get; set; }
        public CommonController(DataContext data)
        {
            Data = data;
        }
    }
}

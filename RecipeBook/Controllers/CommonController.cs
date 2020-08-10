using RecipeBook.Data;

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

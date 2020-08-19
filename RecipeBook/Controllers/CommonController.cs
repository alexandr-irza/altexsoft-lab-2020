using RecipeBook.Data;

namespace RecipeBook.Controllers
{
    public abstract class CommonController
    {
        protected UnitOfWork UnitOfWork { get; }
        protected CommonController(UnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}

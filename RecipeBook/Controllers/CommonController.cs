using RecipeBook.Data;

namespace RecipeBook.Controllers
{
    public abstract class CommonController
    {
        public UnitOfWork UnitOfWork { get; }
        public CommonController(UnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}

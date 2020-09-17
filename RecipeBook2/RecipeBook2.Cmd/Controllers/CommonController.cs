using RecipeBook2.Infrastructure.Repositories;

namespace RecipeBook2.Controllers
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

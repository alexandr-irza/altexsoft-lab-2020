using RecipeBook2.Core.Interfaces;

namespace RecipeBook2.Controllers
{
    public abstract class CommonController
    {
        protected IUnitOfWork UnitOfWork { get; }
        protected CommonController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}

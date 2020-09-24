using RecipeBook2.Core.Interfaces;

namespace RecipeBook2.Core.Controllers
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

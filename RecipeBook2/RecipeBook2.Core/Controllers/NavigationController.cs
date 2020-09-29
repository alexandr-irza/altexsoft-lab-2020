using RecipeBook2.Core.Entities;
using RecipeBook2.Core.Interfaces;
using RecipeBook2.SharedKernel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBook2.Core.Controllers
{
    public class NavigationController : CommonController
    {
        private int _treeIndex;
        public BaseEntity Current { get; private set; }
        public Category Root { get; private set; }
        public List<BaseEntity> Tree { get; } = new List<BaseEntity>();
        public NavigationController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task ReloadDataAsync(int? categoryId = null)
        {
            Tree.Clear();
            (await UnitOfWork.Categories.GetCategoriesByParentIdAsync(categoryId)).ForEach(x => Tree.Add(x));
            (await UnitOfWork.Recipes.GetRecipesByCategoryIdAsync(categoryId)).ForEach(x => Tree.Add(x));
            Root = await UnitOfWork.Categories.GetAsync(categoryId);
            Current = Tree.FirstOrDefault();
            _treeIndex = 0;
        }

        public bool Next()
        {
            if (Tree.Count == 0)
                return false;
            if (++_treeIndex >= Tree.Count)
                _treeIndex = Tree.Count - 1;

            Current = Tree[_treeIndex];
            return true;
        }
        public bool Prev()
        {
            if (Tree.Count == 0)
                return false;
            if (--_treeIndex < 0)
                _treeIndex = 0;
            Current = Tree[_treeIndex];
            return true;
        }

        public void Enter()
        {
            Root = Current as Category;
            _ = ReloadDataAsync(Root?.Id);
        }

        public void Exit()
        {
            Root = Root?.Parent;
            _ = ReloadDataAsync(Root?.Id);
        }
    }
}

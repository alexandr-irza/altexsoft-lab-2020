using RecipeBook2.Core.Entities;
using RecipeBook2.Core.Interfaces;
using RecipeBook2.SharedKernel;
using System.Collections.Generic;
using System.Linq;

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

        public void ReloadData(int? categoryId = null)
        {
            Tree.Clear();
            UnitOfWork.Categories.GetCategoriesByParentId(categoryId).ToList().ForEach(x => Tree.Add(x));
            UnitOfWork.Recipes.GetRecipesByCategoryId(categoryId).ToList().ForEach(x => Tree.Add(x));
            Root = UnitOfWork.Categories.Get(categoryId);
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
            ReloadData(Root?.Id);
        }

        public void Exit()
        {
            Root = Root?.Parent;
            ReloadData(Root?.Id);
        }
    }
}

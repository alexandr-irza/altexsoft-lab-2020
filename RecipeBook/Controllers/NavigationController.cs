using RecipeBook.Data;
using RecipeBook.Models;
using System.Collections.Generic;
using System.Linq;

namespace RecipeBook.Controllers
{
    public class NavigationController : CommonController
    {
        private int TreeIndex;
        public BaseModel Current { get; private set; }
        public Category Root { get; private set; }
        public List<BaseModel> Tree { get; } = new List<BaseModel>();
        public NavigationController(DataContext data) : base(data)
        {
        }

        public void ReloadData(string categoryId = null)
        {
            Tree.Clear();
            Data.Categories.Where(x => x.ParentId == categoryId).ToList().ForEach(x => Tree.Add(x));
            Data.Recipes.Where(x => x.CategoryId == categoryId).ToList().ForEach(x => Tree.Add(x));
            Root = Data.Categories.SingleOrDefault(x => x.Id == categoryId);
            Current = Tree.FirstOrDefault();
            TreeIndex = 0;
        }

        public bool Next()
        {
            if (Tree.Count == 0)
                return false;
            if (++TreeIndex >= Tree.Count)
                TreeIndex = Tree.Count - 1;

            Current = Tree[TreeIndex];
            return true;
        }
        public bool Prev()
        {
            if (Tree.Count == 0)
                return false;
            if (--TreeIndex < 0)
                TreeIndex = 0;
            Current = Tree[TreeIndex];
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

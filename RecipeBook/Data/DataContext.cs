using RecipeBook.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace RecipeBook.Data
{
    public class DataContext
    {
        public ObservableCollection<Recipe> Recipes { get; set; }
        public ObservableCollection<Ingredient> Ingredients { get; set; }
        public ObservableCollection<Category> Categories { get; set; }
        private string CategoriesFileName { get => "categories.json"; }
        private string RecipesFileName { get => "recipes.json"; }
        private string IngredientsFileName { get => "ingredients.json"; }
        public DataContext()
        {
            Recipes = new ObservableCollection<Recipe>();
            Ingredients = new ObservableCollection<Ingredient>();
            Categories = new ObservableCollection<Category>();
            AssignEvents();
        }

        private void AssignEvents()
        {
            Recipes.CollectionChanged += NotifyRecipesCollectionChanged;
            Ingredients.CollectionChanged += NotifyIngredientsCollectionChanged;
            Categories.CollectionChanged += NotifyCategoriesCollectionChanged;
        }

        private void NotifyCategoriesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            DataProcessor.SaveToFile(Categories, CategoriesFileName);
        }

        private void NotifyIngredientsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            DataProcessor.SaveToFile(Ingredients, IngredientsFileName);
        }

        private void NotifyRecipesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            DataProcessor.SaveToFile(Recipes, RecipesFileName);
        }

        public void LoadData()
        {
            Categories = DataProcessor.LoadFromFile<Category>(CategoriesFileName);
            Recipes = DataProcessor.LoadFromFile<Recipe>(RecipesFileName);
            Ingredients = DataProcessor.LoadFromFile<Ingredient>(IngredientsFileName);
            AssignEvents();
        }

    }
}

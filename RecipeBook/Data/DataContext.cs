using RecipeBook.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Text.Json;

namespace RecipeBook.Data
{
    public class DataContext
    {
        public ObservableCollection<Recipe> Recipes { get; set; }
        public ObservableCollection<Ingredient> Ingredients { get; set; }
        public ObservableCollection<Category> Categories { get; set; }

        public DataContext()
        {
            Recipes = new ObservableCollection<Recipe>();
            Recipes.CollectionChanged += NotifyRecipesCollectionChanged;

            Ingredients = new ObservableCollection<Ingredient>();
            Ingredients.CollectionChanged += NotifyIngredientsCollectionChanged;

            Categories = new ObservableCollection<Category>();
            Categories.CollectionChanged += NotifyCategoriesCollectionChanged;
        }

        private void NotifyCategoriesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var fileName = "categories.json";

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            File.WriteAllText(fileName, JsonSerializer.Serialize(Categories, options));
        }

        private void NotifyIngredientsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var fileName = "ingredients.json";

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            File.WriteAllText(fileName, JsonSerializer.Serialize(Ingredients, options));
        }

        private void NotifyRecipesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var fileName = "recipes.json";

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            File.WriteAllText(fileName, JsonSerializer.Serialize(Recipes, options));
        }

        public void LoadData()
        {
            var fileName = "categories.json";
            if (File.Exists(fileName))
            {
                Categories = JsonSerializer.Deserialize<ObservableCollection<Category>>(File.ReadAllText(fileName));
                Categories.CollectionChanged += NotifyCategoriesCollectionChanged;
            }
            fileName = "recipes.json";
            if (File.Exists(fileName))
            {
                Recipes = JsonSerializer.Deserialize<ObservableCollection<Recipe>>(File.ReadAllText(fileName));
                Recipes.CollectionChanged += NotifyRecipesCollectionChanged;
            }
            fileName = "ingredients.json";
            if (File.Exists(fileName))
            {
                Ingredients = JsonSerializer.Deserialize<ObservableCollection<Ingredient>>(File.ReadAllText(fileName));
                Ingredients.CollectionChanged += NotifyIngredientsCollectionChanged;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RecipeBook.Data;
using RecipeBook.Models;

namespace RecipeBook.Controllers
{
    public class CategoryController : CommonController
    {
        public CategoryController(DataContext data) : base(data)
        {
        }

        public List<Category> GetCategories(string parentId = null)
        {
             return Data.Categories.Where(x => x.ParentId == parentId).ToList();
        }

        public Category CreateCategory(string categoryName, string parentId)
        {
            if (Data.Categories.ToList().Find(x => x.Name == categoryName && x.ParentId == parentId) != null)
                throw new Exception($"Category {categoryName} already exists");

            Category category = new Category
            {
                Id = Data.NextCategoryId().ToString(),
                Name = categoryName,
                ParentId = parentId
            };

            Data.Categories.Add(category);
            Data.SaveCategories();
            return category;
        }

        public void RemoveCategory(string categoryId)
        {
            var item = Data.Categories.ToList().Find(x => x.Id == categoryId);
            if (item == null)
                throw new Exception($"Category {categoryId} has not been found");

            Data.Categories.Remove(item);
            Data.SaveCategories();
        }

        public List<Recipe> GetRecipes(string categoryId = null)
        {
            return Data.Recipes.Where(x => x.CategoryId == categoryId).ToList();
        }

        public Recipe CreateRecipe(string recipeName, string categoryId)
        {
            var item = Data.Recipes.ToList().Find(x => x.Name == recipeName);
            if (item != null)
                throw new Exception($"Recipe {item.Name} ({item.CategoryId}) already exists");

            Recipe recipe = new Recipe
            {
                Id = Data.NextRecipeId().ToString(),
                Name = recipeName,
                CategoryId = categoryId
            };

            Data.Recipes.Add(recipe);
            Data.SaveCategories();
            return recipe;
        }

        public void RemoveRecipe(string recipeId)
        {
            var item = Data.Recipes.ToList().Find(x => x.Id == recipeId);
            if (item == null)
                throw new Exception($"Recipe {recipeId} has not been found");

            Data.Recipes.Remove(item);
            Data.SaveCategories();
        }
    }
}

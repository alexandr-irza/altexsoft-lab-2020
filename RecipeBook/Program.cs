using RecipeBook.Controllers;
using RecipeBook.Data;
using RecipeBook.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace RecipeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            var d = new DataContext();
            d.LoadData();

            var cc = new CategoryController(d);
            var rc = new RecipeController(d);

            string categoryId = null;
            Stack<string> categoriesStack = new Stack<string>();
            categoriesStack.Push(categoryId);
            while (categoryId != "exit") {
                Console.WriteLine($"Category path: {string.Join("/", categoriesStack.ToArray().Reverse())}");
                var l = cc.GetCategories(categoryId);
                Console.WriteLine(string.Join(Environment.NewLine, l.Select(x => "Category-> Id: " +x.Id + ", Name: " + x.Name).ToList().ToArray()));

                var ll = rc.GetRecipes(categoryId);
                Console.WriteLine(string.Join(Environment.NewLine, ll.Select(x => "Recipe-> Id: " + x.Id + ", Name: " + x.Name).ToList().ToArray()));

                Console.WriteLine();
                Console.Write("Enter category id: ");
                categoryId = Console.ReadLine();
                if (categoryId.ToLower().Equals("-"))
                    categoryId = categoriesStack.Count > 0 ? categoriesStack.Pop() : null;
                else
                    categoriesStack.Push(categoryId);
            }
        }
    }
}

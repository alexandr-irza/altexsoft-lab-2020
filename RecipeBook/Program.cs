using RecipeBook.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace RecipeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileName = "categories.json";
            List<Category> book;
            if (File.Exists(fileName))
                book = JsonSerializer.Deserialize<List<Category>>(File.ReadAllText(fileName));
            else
            {
                book = new List<Category>();

                var c = new Category
                {
                    Id = "1",
                    Name = "Soups"
                };
                book.Add(c);
                c = new Category
                {
                    Id = "2",
                    Name = "Cocteils"
                };
                var r = new Recipe
                {
                    Id = "1",
                    Name = "Mohito"
                };
                c.Recipes.Add(r);
                book.Add(c);

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                File.WriteAllText(fileName, JsonSerializer.Serialize<List<Category>>(book, options));
            }

            Console.WriteLine(book.ToArray().ToString());
        }
    }
}

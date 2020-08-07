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

            var nc = new NavigationController(d);

            BaseModel current = new BaseModel();
            var index = 0;
            var list = nc.GetItems(current.Id);
            PrintCategory(list, list[index]);

            while (true) {
                try
                {
                    var key = Console.ReadKey();

                    switch (key.Key)
                    {
                        case ConsoleKey.DownArrow:
                            if (list.Count == 0)
                                continue;
                            if (++index >= list.Count)
                                index = list.Count - 1;
                            current = list[index];
                            PrintCategory(list, current);
                            break;
                        case ConsoleKey.UpArrow:
                            if (list.Count == 0)
                                continue;
                            if (--index < 0)
                                index = 0;
                            current = list[index];
                            PrintCategory(list, current);
                            break;
                        case ConsoleKey.Enter:
                            if (current is Recipe)
                                PrintRecipe(current as Recipe);
                            else
                            if (current is Category)
                            {
                                list = nc.GetItems(current.Id);
                                PrintCategory(list, current);
                            }
                            break;
                        case ConsoleKey.Escape:
                            string rootId = current.Id; 
                            if (current is Recipe)
                                rootId = (current as Recipe).CategoryId;
                            else if (current is Category)
                                rootId = (current as Category).ParentId;
                            list = nc.GetItems(rootId);
                            PrintCategory(list, current);
                            break;
                        case ConsoleKey.Q:
                            return;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        static void PrintRecipe(Recipe recipe)
        {
            Console.Clear();
            Console.WriteLine("*******************************");
            Console.WriteLine($"{recipe.Name}");
            Console.WriteLine("----------Description----------");
            Console.WriteLine($"{recipe.Description}");
            Console.WriteLine("--------Ingredients------------");
            recipe.Ingredients.ForEach(x => Console.WriteLine($"- {x.Ingredient.Name} ({x.Amount})"));
            Console.WriteLine("-----------Steps---------------");
            recipe.Directions.ForEach(x => Console.WriteLine($"{x.StepNumber}. {x.StepInstruction}"));
            Console.WriteLine("*******************************");
        }

        static void PrintCategory(List<BaseModel> list, BaseModel current)
        {
            Console.Clear();
            list.ForEach(x => Console.WriteLine((x.Id == current.Id && x.GetType() == current.GetType() ? "->" : "  ") + x.ToString()));
            Console.Write("Up/Down - navigation, Enter - Open, Exc - Back, Q - Exit");
        }
    }
}

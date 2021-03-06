﻿using RecipeBook.Controllers;
using RecipeBook.Data;
using RecipeBook.Models;
using System;
using System.Text;

namespace RecipeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            var d = new UnitOfWork();

            var nc = new NavigationController(d);
            var cc = new CategoryController(d);
            var rc = new RecipeController(d);

            nc.ReloadData();
            PrintTree(nc);

            while (true)
            {
                try
                {
                    var key = Console.ReadKey();

                    switch (key.Key)
                    {
                        case ConsoleKey.DownArrow:
                            if (nc.Next())
                                PrintTree(nc);
                            break;
                        case ConsoleKey.UpArrow:
                            if (nc.Prev())
                                PrintTree(nc);
                            break;
                        case ConsoleKey.Enter:
                            if (nc.Current is Recipe)
                            {
                                PrintRecipe(nc.Current as Recipe);
                                Console.ReadLine();
                                PrintTree(nc);
                            }
                            else
                            if (nc.Current is Category)
                            {
                                nc.Enter();
                                PrintTree(nc);
                            }
                            break;
                        case ConsoleKey.Backspace:
                            nc.Exit();
                            PrintTree(nc);
                            break;
                        case ConsoleKey.Q:
                            return;
                        case ConsoleKey.N:
                            Console.Clear();
                            OutputLine("Creating a new recipe", ConsoleColor.Blue);
                            var recipe = EnterRecipe(nc.Root?.Id);
                            recipe = rc.CreateRecipe(recipe);
                            while (true)
                            {
                                var res = EnterIngredient();
                                rc.AddIngredient(recipe, res.Ingredient, res.Amount);
                                OutputLine("Enter to continue adding, Backspace to exit", ConsoleColor.Cyan);
                                if (Console.ReadKey().Key != ConsoleKey.Enter)
                                    break;
                            }
                            while (true)
                            {
                                var res = EnterStep();
                                rc.AddDirection(recipe, res);
                                OutputLine("Enter to continue adding, Backspace to exit", ConsoleColor.Cyan);
                                if (Console.ReadKey().Key != ConsoleKey.Enter)
                                    break;
                            }
                            nc.ReloadData(nc.Root?.Id);
                            PrintTree(nc);
                            break;
                        case ConsoleKey.C:
                            Console.Clear();
                            OutputLine("Creating a new category", ConsoleColor.Blue);
                            var category = EnterCategory(nc.Root?.Id);
                            category = cc.CreateCategory(category);
                            nc.ReloadData(nc.Root?.Id);
                            PrintTree(nc);
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    OutputLine($"ERROR: { ex.Message }", ConsoleColor.Red);
                }
            }
        }

        static void PrintRecipe(Recipe recipe)
        {
            Console.Clear();
            OutputLine("Enter - Back", ConsoleColor.Cyan);
            OutputLine("*******************************", ConsoleColor.Red);
            Output("Name: ", ConsoleColor.Green);
            OutputLine($"{recipe.Name}");
            Output("Description: ", ConsoleColor.Green);
            OutputLine($"{recipe.Description}");
            OutputLine("Ingredients", ConsoleColor.Green);
            recipe.Ingredients.ForEach(x => OutputLine($"- {x.Ingredient.Name} ({x.Amount})"));
            OutputLine("Steps", ConsoleColor.Green);
            recipe.Directions.ForEach(x => OutputLine($"{x.StepNumber}. {x.StepInstruction}"));
            OutputLine("*******************************", ConsoleColor.Red);
        }

        static void OutputLine(string msg, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void Output(string msg, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.Write(msg);
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void PrintTree(NavigationController navigationController)
        {
            Console.Clear();
            OutputLine("Up/Down - Navigation, Enter - Open, Backspace - Back, Q - Exit, N - New recipe, C - New category", ConsoleColor.Cyan);
            navigationController.Tree.ForEach(x => OutputLine((x.Id == navigationController.Current?.Id && x.GetType() == navigationController.Current.GetType() ? "->" : "  ") + x.ToString()));
        }

        private static (string Ingredient, double Amount) EnterIngredient()
        {
            Output("Enter ingredient name: ", ConsoleColor.Yellow);
            return (Ingredient: Console.ReadLine(), Amount: ParseDouble("Amount"));
        }

        private static string EnterStep()
        {
            Output("Enter step description: ", ConsoleColor.Yellow);
            return Console.ReadLine();
        }

        private static Recipe EnterRecipe(string categoryId)
        {
            Output("Enter recipe name: ", ConsoleColor.Yellow);
            var name = Console.ReadLine();
            Output("Enter Desription name: ", ConsoleColor.Yellow);
            var desc = Console.ReadLine();

            return new Recipe { Name = name, CategoryId = categoryId, Description = desc };
        }
        private static Category EnterCategory(string categoryId)
        {
            Output("Enter category name: ", ConsoleColor.Yellow);
            var name = Console.ReadLine();

            return new Category { Name = name, ParentId = categoryId };
        }
        private static double ParseDouble(string name)
        {
            while (true)
            {
                Output($"Enter {name}: ", ConsoleColor.Yellow);
                if (double.TryParse(Console.ReadLine(), out double value))
                {
                    return value;
                }
                else
                {
                    OutputLine($"Incorrect format {name}", ConsoleColor.Red);
                }
            }
        }
    }
}

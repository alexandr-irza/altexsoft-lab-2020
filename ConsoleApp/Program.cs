using System;
using System.IO;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var param = "";
            if (args.Length == 0)
            {
                param = Console.ReadLine();
            }
            else
            {
                param = args[0];
            }

            param = AppDomain.CurrentDomain.BaseDirectory + param;

            if (File.Exists(param))
            {
                var text = File.ReadAllText(param);
                char[] delimiterChars = { ' ', ',', '.', ':', '\r', '\n', '?', '!', '(', ')' };

                var words = text.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);

                for (var i = 0; i < words.Length; i++)
                {
                    if (i % 10 == 0)
                    Console.Write(words[i] + ", ");
                }
            }
            else
            {
                Console.WriteLine("File {0} not found", param);
            }

        }
    }
}

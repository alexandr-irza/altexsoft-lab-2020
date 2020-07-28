using System;
using System.IO;

namespace ConsoleApp
{
    class Program
    {
        static void Output(string msg, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void OutputError(string msg)
        {
            Output(msg, ConsoleColor.Red);
        }

        static string ReverseString(string input)
        {
            var res = "";
            for (int i = input.Length - 1; i >= 0; i--)
            {
                res += input[i];
            }
            return res;
        }

        static void Main(string[] args)
        {
            var fileName = "";
            var param = "";
            if (args.Length == 0)
            {
                Output("Enter file name:", ConsoleColor.Yellow);
                fileName = Console.ReadLine();
                Output("Enter word to replace:", ConsoleColor.Yellow);
                param = Console.ReadLine();
            }
            else
            {
                fileName = args[0];
                param = args[1];
            }

            fileName = AppDomain.CurrentDomain.BaseDirectory + fileName;

            if (File.Exists(fileName))
            {
                var text = File.ReadAllText(fileName);

                #region 1 task replace word
                if (param.Length > 0 && text.Contains(param))
                {
                    //Store original file
                    File.Copy(fileName, fileName + ".origin", true);
                    text = text.Replace(param, "", true, null);
                    File.WriteAllText(fileName, text);
                    Output(string.Format("Text without the word \"{0}\"", param), ConsoleColor.Green);
                    Output(text);
                }
                else
                {
                    OutputError(string.Format("Text does not contain the word \"{0}\"", param));
                    Output(text);
                }
                #endregion
                #region 2 task count words and show every 10
                text = File.ReadAllText(fileName);
                var wordDelimiters = new string[] { " ", ",", ".", ":", "\r\n", Environment.NewLine, "?", "!", "(", ")", "\"" };

                var words = text.Split(wordDelimiters, StringSplitOptions.RemoveEmptyEntries);

                Output("Words count:", ConsoleColor.Green);
                Output(words.Length.ToString());

                var res = "";
                
                for (var i = 0; i < words.Length; i = i + 10)
                {
                    res += (res.Length != 0 ? ", " : "") + words[i];
                }

                Output("Every 10th word separated by comma:", ConsoleColor.Green);
                Output(res);
                #endregion

                #region 3 task show third sentence

                text = File.ReadAllText(fileName);
                var sentenceDelimiters = new string[] { ".", "!", "?" };

                var sentences = text.Split(sentenceDelimiters, StringSplitOptions.RemoveEmptyEntries);

                if (sentences.Length > 3)
                {
                    var sentence = sentences[2];
                    Output("Third sentence:", ConsoleColor.Green);
                    Output(sentence);

                    words = sentence.Split(" ");

                    for (int i = 0; i < words.Length; i++)
                    {
                        words[i] = ReverseString(words[i]);
                    }

                    Output("Third sentence with reversed words:", ConsoleColor.Green);

                    Output(string.Join(" ", words));
                }
                else
                    OutputError("Sentence #3 does not exist.");

                #endregion

            }
            else
            {
                OutputError(string.Format("File {0} not found", fileName));
            }

        }
    }
}

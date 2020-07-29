using System;
using System.Collections.Generic;
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

        static void PrintOperations()
        {
            Output("Available operations:");
            Output("-replaceText [FileName] [Word]");
            Output("-countWords [FileName]");
            Output("-reverseWords [FileName]");
            Output("-browseFolder [Path]");
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
            var command = "";
            var path = "";
            var param = "";
            if (args.Length == 0)
            {
                PrintOperations();
                Output("Enter operation name or number:", ConsoleColor.Yellow);
                command = Console.ReadLine();
            }
            else
            {
                command = args[0];
                if (args.Length > 1)
                    path = args[1];
                if (args.Length > 2)
                    param = args[2];
            }

            command = command.ToLower();
            if (path.Length == 0)
            {
                if (command.Contains("browseFolder"))
                    Output("Enter file name:", ConsoleColor.Yellow);
                else
                    Output("Enter folder path:", ConsoleColor.Yellow);

                path = Console.ReadLine();
            }

            if (command.Contains("replacetext"))
            { 
                if (param.Length == 0)
                {
                    Output("Enter word to replace:", ConsoleColor.Yellow);
                    param = Console.ReadLine();
                }
            }


            if (command.Contains("replacetext"))
            {
                path = AppDomain.CurrentDomain.BaseDirectory + path;
                if (!File.Exists(path))
                {
                    OutputError($"File {path} not found");
                    return;
                }

                var text = File.ReadAllText(path);

                #region 1 task replace word
                if (param.Length > 0 && text.Contains(param))
                {
                    //Store original file
                    File.Copy(path, path + ".origin", true);
                    text = text.Replace(param, "", true, null);
                    File.WriteAllText(path, text);
                    Output($"Text without the word \"{param}\"", ConsoleColor.Green);
                    Output(text);
                }
                else
                {
                    OutputError($"Text does not contain the word \"{param}\"");
                    Output(text);
                }
            }
            else
            #endregion
            #region 2 task count words and show every 10
            if (command.Contains("countwords"))
            {
                path = AppDomain.CurrentDomain.BaseDirectory + path;
                if (!File.Exists(path))
                {
                    OutputError($"File {path} not found");
                    return;
                }
                var text = File.ReadAllText(path);
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
            }
            else
            #endregion

            #region 3 task show third sentence
            if (command.Contains("reversewords"))
            {
                path = AppDomain.CurrentDomain.BaseDirectory + path;
                if (!File.Exists(path))
                {
                    OutputError($"File {path} not found");
                    return;
                }
                var text = File.ReadAllText(path);
                var sentenceDelimiters = new string[] { ".", "!", "?" };

                var sentences = text.Split(sentenceDelimiters, StringSplitOptions.RemoveEmptyEntries);

                if (sentences.Length > 3)
                {
                    var sentence = sentences[2];
                    Output("Third sentence:", ConsoleColor.Green);
                    Output(sentence);

                    var words = sentence.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                    for (int i = 0; i < words.Length; i++)
                    {
                        words[i] = ReverseString(words[i]);
                    }

                    Output("Third sentence with reversed words:", ConsoleColor.Green);

                    Output(string.Join(" ", words));
                }
                else
                    OutputError("Sentence #3 does not exist.");
            }
            else
            #endregion

            #region 4 task show folder content
            if (command.Contains("browsedir"))
            {
                Output("Browse Dir (EXIT for terminate):", ConsoleColor.Green);
                if (!Directory.Exists(path))
                {
                    OutputError($"Folder {path} not found");
                    return;
                }
                var folders = new string[] { };
                var files = new string[] { };
                while (path.ToLower() != "exit")
                {
                    var content = new List<string>();
                    var attr = File.GetAttributes(path);
                    if (!attr.HasFlag(FileAttributes.Directory))
                        path = Path.GetDirectoryName(path);
                    if (path == null)
                        return;
                    try
                    {
                        folders = Directory.GetDirectories(path.Trim('\\') + "\\");
                    }
                    catch
                    {
                        folders = new string[] { };
                    }
                    Array.Sort(folders);
                    content.AddRange(folders);
                    try
                    {
                        files = Directory.GetFiles(path.Trim('\\') + "\\");
                    }
                    catch
                    {
                        files = new string[] { };
                    }
                    Array.Sort(files);
                    content.AddRange(files);
                    if (content.Count > 0)
                        Output($"-, {path}");
                    for (int i = 0; i < content.Count; i++)
                    {
                        Output($"{i}, {content[i]}");
                    }
                    var val = Console.ReadLine();
                    int index;
                    if (int.TryParse(val, out index) && index >= 0 && index < content.Count)
                        path = content[index];
                    else
                        val = "-";
                    if (val == "-")
                        path = Path.GetDirectoryName(path);
                    if (path == null)
                        return;
                }
            }
            else
                #endregion
                OutputError("Incorrect operation");
        }
    }
}

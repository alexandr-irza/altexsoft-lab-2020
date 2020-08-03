using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp
{
    static class Utils
    {
        static public void Output(string msg, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
        }

        static public void OutputError(string msg)
        {
            Output(msg, ConsoleColor.Red);
        }

        static public void PrintAvailableOperations()
        {
            Output("Available operations:");
            Output($"-{ CmdCommands.ReplaceText } [FileName] [Word]");
            Output($"-{ CmdCommands.CountWords } [FileName]");
            Output($"-{ CmdCommands.ReverseWords } [FileName]");
            Output($"-{ CmdCommands.BrowseFolder } [Path]");
        }

        static public string RequestPath(string title, bool isFileName)
        {
            var res = "";
            Output(title, ConsoleColor.Yellow);
            while (res.Length == 0)
            {
                res = Console.ReadLine();
                if (string.Equals(res.ToUpper(), "EXIT"))
                    return "";
                if ((isFileName && !File.Exists(res)) || (!isFileName && !Directory.Exists(res)))
                {
                    if (isFileName)
                    {
                        res = AppDomain.CurrentDomain.BaseDirectory + res;
                        if (File.Exists(res))
                            return res;
                    }
                    OutputError($"Path {res} not found. Please retry enter or EXIT to terminate");
                    res = "";
                    continue;
                }
            }
            return res;
        }
    }
}

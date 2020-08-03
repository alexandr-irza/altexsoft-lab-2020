using ConsoleApp.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string command;
            var path = "";
            var param = "";
            if (args.Length == 0)
            {
                Utils.PrintAvailableOperations();
                Utils.Output("Enter operation name:", ConsoleColor.Yellow);
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

            command = command.Trim().Replace("/", "").Replace("-", "");

            if (!CmdCommands.Exists(command))
            {
                Utils.OutputError("Incorrect operation");
                Utils.PrintAvailableOperations();
                return;
            }

            if (path.Length == 0)
            {
                path = Utils.RequestPath(command.Equals(CmdCommands.BrowseFolder) ? "Enter folder path:" : "Enter file name:", !command.Equals(CmdCommands.BrowseFolder));
                if (path.Length == 0)
                    return;
            }

            CommonCommand cmd;

            switch (command)
            { 
                case CmdCommands.ReplaceText: 
                    if (param.Length == 0)
                    {
                        Utils.Output("Enter word to replace:", ConsoleColor.Yellow);
                        param = Console.ReadLine();
                    }
                    cmd = new ReplaceTextCommand(path, param);
                    break;
                case CmdCommands.CountWords:
                    cmd = new CountWordsCommand(path);
                    break;
                case CmdCommands.ReverseWords:
                    cmd = new ReverseWordsCommand(path);
                    break;
                case CmdCommands.BrowseFolder:
                    cmd = new BrowseFolderCommand(path);
                    break;
                default:
                    cmd = new IncorrectCommand(path);
                    break;
            }

            cmd.DoWork();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    static class CmdCommands
    {
        static public readonly string ReplaceText = "replacetext";
        static public readonly string CountWords = "countwords";
        static public readonly string ReverseWords = "reversewords";
        static public readonly string BrowseFolder = "browsefolder";

        static readonly string[] Commands = new string[] { ReplaceText, CountWords, ReverseWords, BrowseFolder };
        static public bool Exists(string command)
        {
            return Array.Exists(Commands, element => string.Compare(element, command, true) == 0);
        }
    }
}

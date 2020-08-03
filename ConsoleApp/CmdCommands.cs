using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    static class CmdCommands
    {
        public const string ReplaceText = "replacetext";
        public const string CountWords = "countwords";
        public const string ReverseWords = "reversewords";
        public const string BrowseFolder = "browsefolder";

        static readonly string[] Commands = new string[] { ReplaceText, CountWords, ReverseWords, BrowseFolder };
        static public bool Exists(string command)
        {
            return Array.Exists(Commands, element => string.Compare(element, command, true) == 0);
        }
    }
}

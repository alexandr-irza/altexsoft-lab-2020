using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApp.Commands
{
    public class ReverseWordsCommand : CommonCommand
    {
        public ReverseWordsCommand(string path) : base(path)
        {
        }

        public override void DoWork()
        {
            if (Path.GetDirectoryName(Location) == null)
                Location = AppDomain.CurrentDomain.BaseDirectory + Location;
            if (!File.Exists(Location))
            {
                Utils.OutputError($"File {Location} not found");
                return;
            }
            var text = File.ReadAllText(Location);

            var pattern = @"(\S.+?[.!?])";
            var sentences = Regex.Matches(text, pattern, RegexOptions.IgnoreCase);

            if (sentences.Count >= 3)
            {
                var sentence = sentences[2].Value;
                Utils.Output("Third sentence:", ConsoleColor.Green);
                Utils.Output(sentence);
                Utils.Output("Third sentence with reversed words:", ConsoleColor.Green);
                Utils.Output(string.Join(" ", sentence.Split(" ").Select(word => new string(word.Reverse().ToArray()))));
            }
            else
                Utils.OutputError("Sentence #3 does not exist.");
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp.Commands
{
    public class BrowseFolderCommand : CommonCommand
    {
        public BrowseFolderCommand(string path) : base(path)
        {
        }

        public override bool DoWork()
        {
            Utils.Output("Browse Dir (EXIT for terminate):", ConsoleColor.Green);
            string[] folders;
            string[] files;
            var path = Location;
            while (path.ToUpper() != "EXIT")
            {
                var attr = File.GetAttributes(path);
                if (!attr.HasFlag(FileAttributes.Directory))
                    path = Path.GetDirectoryName(path);
                if (path == null)
                    return false;
                if (!Directory.Exists(path))
                {
                    Utils.OutputError($"Folder {path} not found");
                    return false;
                }

                var content = new List<string>();
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
                    Utils.Output($"-, {path}");
                for (int i = 0; i < content.Count; i++)
                {
                    Utils.Output($"{i}, {content[i]}");
                }
                var val = Console.ReadLine();
                
                if (int.TryParse(val, out int index) && index >= 0 && index < content.Count)
                    path = content[index];
                else
                    val = "-";
                if (val == "-")
                    path = Path.GetDirectoryName(path);
                if (path == null)
                    return false;
            }
            return true;
        }
    }
}

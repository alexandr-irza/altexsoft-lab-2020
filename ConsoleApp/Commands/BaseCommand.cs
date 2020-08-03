using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp.Commands
{
    public class BaseCommand : CommonCommand
    {
        public BaseCommand(string path) : base(path)
        {
        }

        public override bool DoWork()
        {
            if (Path.GetDirectoryName(Location) == null)
                Location = AppDomain.CurrentDomain.BaseDirectory + Location;
            if (!File.Exists(Location))
            {
                Utils.OutputError($"File {Location} not found");
                return false;
            }
            return true;
        }
    }
}

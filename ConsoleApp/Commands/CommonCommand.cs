using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    public abstract class CommonCommand
    {
        public CommonCommand(string path)
        {
            Location = path;
        }

        public string Location { get; set; }

        public abstract bool DoWork();
    }
}

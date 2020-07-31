using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    public class CommonCommand
    {
        public CommonCommand(string path)
        {
            Location = path;
        }

        public string Location { get; set; }

        public virtual void DoWork()
        {
            Utils.OutputError("Incorrect operation");
        }
    }
}

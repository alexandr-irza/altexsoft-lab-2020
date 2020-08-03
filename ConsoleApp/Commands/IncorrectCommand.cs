using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Commands
{
    class IncorrectCommand : CommonCommand
    {
        public IncorrectCommand(string path) : base(path)
        {
        }

        public override bool DoWork()
        {
            Utils.OutputError("Incorrect operation");
            return false;
        }
    }
}

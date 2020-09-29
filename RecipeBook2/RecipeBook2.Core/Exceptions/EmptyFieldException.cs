using System;
using System.Collections.Generic;
using System.Text;

namespace RecipeBook2.Core.Exceptions
{
    public class EmptyFieldException : BaseException
    {
        public EmptyFieldException(string name, string field) 
            : base($"Field { name }.{ field } cannot be empty.")
        {

        }
    }
}

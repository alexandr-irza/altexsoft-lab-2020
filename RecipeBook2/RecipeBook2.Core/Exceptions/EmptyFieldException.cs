using System;

namespace RecipeBook2.Core.Exceptions
{
    public class EmptyFieldException : BaseException
    {
        public EmptyFieldException()
        {
        }
        public EmptyFieldException(string message) : base(message)
        {
        }
        public EmptyFieldException(string message, Exception inner) : base(message, inner)
        {
        }

    }
}

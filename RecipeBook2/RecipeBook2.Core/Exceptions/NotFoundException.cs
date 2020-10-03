using System;

namespace RecipeBook2.Core.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException()
        {
        }
        public NotFoundException(string message) : base(message)
        {
        }
        public NotFoundException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}

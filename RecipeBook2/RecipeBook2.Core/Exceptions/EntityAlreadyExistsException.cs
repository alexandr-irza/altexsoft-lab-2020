using System;

namespace RecipeBook2.Core.Exceptions
{
    public class EntityAlreadyExistsException : BaseException
    {
        public EntityAlreadyExistsException()
        {
        }
        public EntityAlreadyExistsException(string message) : base(message)
        {
        }
        public EntityAlreadyExistsException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}

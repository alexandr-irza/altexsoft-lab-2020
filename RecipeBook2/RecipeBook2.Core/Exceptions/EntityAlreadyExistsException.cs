namespace RecipeBook2.Core.Exceptions
{
    public class EntityAlreadyExistsException : BaseException
    {
        public EntityAlreadyExistsException(string name, object key)
            : base($"Entity { name } ({ key }) already exists.)")
        {

        }
    }
}

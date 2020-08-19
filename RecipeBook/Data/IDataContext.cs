using System.Collections.Generic;

namespace RecipeBook.Data
{
    public interface IDataContext
    {
        void SaveToFile<T>(IEnumerable<T> list, string fileName) where T : class;
        IEnumerable<T> LoadFromFile<T>(string fileName) where T : class, new();
    }
}

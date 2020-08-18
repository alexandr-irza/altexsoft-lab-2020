using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace RecipeBook.Data
{
    public class JsonFileContext : IDataContext
    {
        public void SaveToFile<T>(IEnumerable<T> list, string fileName) where T : class
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            File.WriteAllText(fileName, JsonSerializer.Serialize(list, options), Encoding.Unicode);
        }
        public IEnumerable<T> LoadFromFile<T>(string fileName) where T : class, new()
        {
            return File.Exists(fileName) ? JsonSerializer.Deserialize<List<T>>(File.ReadAllText(fileName, Encoding.Unicode)) : new List<T>();
        }
    }
}

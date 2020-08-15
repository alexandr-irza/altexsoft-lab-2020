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

        IEnumerable<T> IDataContext.LoadFromFile<T>(string fileName)
        {
            List<T> output;
            if (File.Exists(fileName))
                output = JsonSerializer.Deserialize<List<T>>(File.ReadAllText(fileName, Encoding.Unicode));
            else
                output = new List<T>();
            return output;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;

namespace RecipeBook.Data
{
    public class DataProcessor
    {
        public static void SaveToFile<T>(ObservableCollection<T> list, string fileName) where T: class
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            File.WriteAllText(fileName, JsonSerializer.Serialize(list, options));
        }

        public static ObservableCollection<T> LoadFromFile<T>(string fileName) where T : class, new()
        {
            ObservableCollection<T> output;
            if (File.Exists(fileName))
                output = JsonSerializer.Deserialize<ObservableCollection<T>>(File.ReadAllText(fileName));
            else
                output = new ObservableCollection<T>();
            return output;
        }
    }
}
